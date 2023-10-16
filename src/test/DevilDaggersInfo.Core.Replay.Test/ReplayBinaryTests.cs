using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Replay.Numerics;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace DevilDaggersInfo.Core.Replay.Test;

[TestClass]
public class ReplayBinaryTests
{
	[DataTestMethod]
	[DataRow("Forked-psy.ddreplay")]
	[DataRow("Forked-xvlv.ddreplay")]
	public void GetSpawnsetBuffer(string replayFileName)
	{
		string replayFilePath = Path.Combine("Resources", replayFileName);
		string spawnsetFilePath = Path.Combine("Resources", "Forked");

		byte[] replayBuffer = File.ReadAllBytes(replayFilePath);
		ReplayBinary<LocalReplayBinaryHeader> replayBinary = new(replayBuffer);

		CollectionAssert.AreEqual(replayBinary.Header.SpawnsetMd5, MD5.HashData(replayBinary.Header.SpawnsetBuffer));
		CollectionAssert.AreEqual(File.ReadAllBytes(spawnsetFilePath), replayBinary.Header.SpawnsetBuffer);
	}

	[TestMethod]
	public void ParseAndCompileEvents()
	{
		ReplayBinary<LocalReplayBinaryHeader> replayBinary = ReplayBinary<LocalReplayBinaryHeader>.CreateDefault();
		replayBinary.EventsData.Clear();
		replayBinary.EventsData.AddEvent(new InitialInputsEventData(true, false, false, false, JumpType.None, ShootType.Hold, ShootType.None, 0, 0, 0.2f));

		replayBinary.EventsData.AddEvent(new SquidSpawnEventData(SquidType.Squid3, -1, Vector3.Zero, Vector3.Zero, 0));
		for (int i = 0; i < 30; i++)
		{
			replayBinary.EventsData.AddEvent(new BoidSpawnEventData(1, BoidType.Skull4, default, Int16Mat3x3.Identity, default, 10));
			replayBinary.EventsData.AddEvent(new InputsEventData(true, false, false, false, JumpType.None, ShootType.None, ShootType.None, 10, 0));
		}

		replayBinary.EventsData.AddEvent(new EndEventData());

		byte[] replayBuffer = replayBinary.Compile();

		ReplayBinary<LocalReplayBinaryHeader> replayBinaryFromBuffer = new(replayBuffer);

		Assert.AreEqual(replayBinary.EventsData.Events.Count, replayBinaryFromBuffer.EventsData.Events.Count);
		for (int i = 0; i < replayBinary.EventsData.Events.Count; i++)
			Assert.AreEqual(replayBinary.EventsData.Events[i], replayBinaryFromBuffer.EventsData.Events[i]);
	}

	[TestMethod]
	public void EditEventData()
	{
		string replayFilePath = Path.Combine("Resources", "SkullTest.ddreplay");
		byte[] replayBuffer = File.ReadAllBytes(replayFilePath);
		ReplayBinary<LocalReplayBinaryHeader> replayBinary = new(replayBuffer);

		int skullsAccessed = 0;
		foreach (ReplayEvent e in replayBinary.EventsData.Events)
		{
			if (e.Data is not BoidSpawnEventData boid)
				continue;

			Assert.AreEqual(new(20, 20, 20), boid.Position);
			boid.Position = new(10, 10, 10);
			skullsAccessed++;
		}

		Assert.AreEqual(4, skullsAccessed);

		foreach (ReplayEvent e in replayBinary.EventsData.Events)
		{
			if (e.Data is not BoidSpawnEventData boid)
				continue;

			Assert.AreEqual(new(10, 10, 10), boid.Position);
			skullsAccessed++;
		}

		Assert.AreEqual(8, skullsAccessed);

		byte[] compiledReplayBuffer = replayBinary.Compile();

		ReplayBinary<LocalReplayBinaryHeader> replayBinaryFromBuffer = new(compiledReplayBuffer);

		Assert.AreEqual(replayBinary.EventsData.Events.Count, replayBinaryFromBuffer.EventsData.Events.Count);
		for (int i = 0; i < replayBinary.EventsData.Events.Count; i++)
			Assert.AreEqual(replayBinary.EventsData.Events[i], replayBinaryFromBuffer.EventsData.Events[i]);

		foreach (ReplayEvent e in replayBinary.EventsData.Events)
		{
			if (e.Data is not BoidSpawnEventData boid)
				continue;

			Assert.AreEqual(new(10, 10, 10), boid.Position);
			skullsAccessed++;
		}

		Assert.AreEqual(12, skullsAccessed);
	}

	[DataTestMethod]
	[DataRow("ddrpl.", true)]
	[DataRow("ddrpl..", true)]
	[DataRow("ddrpl..abc", true)]
	[DataRow("ddRpl.", false)]
	[DataRow("ddrpl", false)]
	[DataRow("dd", false)]
	[DataRow("DF_RPL2", false)]
	[DataRow("", false)]
	[DataRow("dr1pl.", false)]
	public void TestValidateLocalReplayHeaderIdentifier(string identifier, bool isValid)
	{
		byte[] identifierBytes = Encoding.UTF8.GetBytes(identifier);
		Assert.AreEqual(isValid, LocalReplayBinaryHeader.IdentifierIsValid(identifierBytes, out _));
	}

	[DataTestMethod]
	[DataRow("DF_RPL2", true)]
	[DataRow("DF_RPL22", true)]
	[DataRow("DF_RPL22abc", true)]
	[DataRow("Df_RPL2", false)]
	[DataRow("DF_RPL", false)]
	[DataRow("DF", false)]
	[DataRow("ddrpl.", false)]
	[DataRow("", false)]
	[DataRow("D_F1PL2", false)]
	public void TestValidateLeaderboardReplayHeaderIdentifier(string identifier, bool isValid)
	{
		byte[] identifierBytes = Encoding.UTF8.GetBytes(identifier);
		Assert.AreEqual(isValid, LeaderboardReplayBinaryHeader.IdentifierIsValid(identifierBytes, out _));
	}
}
