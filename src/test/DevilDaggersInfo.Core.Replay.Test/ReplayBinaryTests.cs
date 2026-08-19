using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace DevilDaggersInfo.Core.Replay.Test;

internal sealed class ReplayBinaryTests
{
	[Test]
	[Arguments("Forked-psy.ddreplay")]
	[Arguments("Forked-xvlv.ddreplay")]
	public async Task GetSpawnsetBuffer(string replayFileName)
	{
		string replayFilePath = Path.Combine("Resources", replayFileName);
		string spawnsetFilePath = Path.Combine("Resources", "Forked");

		byte[] replayBuffer = await File.ReadAllBytesAsync(replayFilePath);
		ReplayBinary<LocalReplayBinaryHeader> replayBinary = new(replayBuffer);

		await Assert.That(MD5.HashData(replayBinary.Header.SpawnsetBuffer)).IsEquivalentTo(replayBinary.Header.SpawnsetMd5, CollectionOrdering.Matching);
		await Assert.That(replayBinary.Header.SpawnsetBuffer).IsEquivalentTo(await File.ReadAllBytesAsync(spawnsetFilePath), CollectionOrdering.Matching);
	}

	// TODO: Move to ddinfo-tools.
	// [Test]
	// public async Task ParseAndCompileEvents()
	// {
	// 	ReplayBinary<LocalReplayBinaryHeader> replayBinary = new(LocalReplayBinaryHeader.CreateDefault(), Array.Empty<ReplayEvent>());
	// 	replayBinary.Events.AddEvent(new InitialInputsEventData(true, false, false, false, JumpType.None, ShootType.Hold, ShootType.None, 0, 0, 0.2f));
	//
	// 	replayBinary.Events.AddEvent(new SquidSpawnEventData(SquidType.Squid3, -1, Vector3.Zero, Vector3.Zero, 0));
	// 	for (int i = 0; i < 30; i++)
	// 	{
	// 		replayBinary.Events.AddEvent(new BoidSpawnEventData(1, BoidType.Skull4, default, Int16Mat3x3.Identity, default, 10));
	// 		replayBinary.Events.AddEvent(new InputsEventData(true, false, false, false, JumpType.None, ShootType.None, ShootType.None, 10, 0));
	// 	}
	//
	// 	replayBinary.Events.AddEvent(new EndEventData());
	//
	// 	byte[] replayBuffer = replayBinary.Compile();
	//
	// 	ReplayBinary<LocalReplayBinaryHeader> replayBinaryFromBuffer = new(replayBuffer);
	//
	// 	await Assert.That(replayBinaryFromBuffer.Events.Events.Count).IsEqualTo(replayBinary.Events.Events.Count);
	// 	for (int i = 0; i < replayBinary.Events.Events.Count; i++)
	// 		await Assert.That(replayBinaryFromBuffer.Events.Events[i]).IsEqualTo(replayBinary.Events.Events[i]);
	// }

	[Test]
	public async Task EditEventData()
	{
		string replayFilePath = Path.Combine("Resources", "SkullTest.ddreplay");
		byte[] replayBuffer = await File.ReadAllBytesAsync(replayFilePath);
		ReplayBinary<LocalReplayBinaryHeader> replayBinary = new(replayBuffer);

		int skullsAccessed = 0;
		foreach (ReplayEvent e in replayBinary.Events)
		{
			if (e.Data is not BoidSpawnEventData boid)
				continue;

			await Assert.That(boid.Position).IsEqualTo(new Int16Vec3(20, 20, 20));
			boid.Position = new Int16Vec3(10, 10, 10);
			skullsAccessed++;
		}

		await Assert.That(skullsAccessed).IsEqualTo(4);

		foreach (ReplayEvent e in replayBinary.Events)
		{
			if (e.Data is not BoidSpawnEventData boid)
				continue;

			await Assert.That(boid.Position).IsEqualTo(new Int16Vec3(10, 10, 10));
			skullsAccessed++;
		}

		await Assert.That(skullsAccessed).IsEqualTo(8);

		byte[] compiledReplayBuffer = replayBinary.Compile();

		ReplayBinary<LocalReplayBinaryHeader> replayBinaryFromBuffer = new(compiledReplayBuffer);

		await Assert.That(replayBinaryFromBuffer.Events.Count).IsEqualTo(replayBinary.Events.Count);
		for (int i = 0; i < replayBinary.Events.Count; i++)
			await Assert.That(replayBinaryFromBuffer.Events[i]).IsEqualTo(replayBinary.Events[i]);

		foreach (ReplayEvent e in replayBinary.Events)
		{
			if (e.Data is not BoidSpawnEventData boid)
				continue;

			await Assert.That(boid.Position).IsEqualTo(new Int16Vec3(10, 10, 10));
			skullsAccessed++;
		}

		await Assert.That(skullsAccessed).IsEqualTo(12);
	}

	[Test]
	[Arguments("ddrpl.", true)]
	[Arguments("ddrpl..", true)]
	[Arguments("ddrpl..abc", true)]
	[Arguments("ddRpl.", false)]
	[Arguments("ddrpl", false)]
	[Arguments("dd", false)]
	[Arguments("DF_RPL2", false)]
	[Arguments("", false)]
	[Arguments("dr1pl.", false)]
	public async Task TestValidateLocalReplayHeaderIdentifier(string identifier, bool isValid)
	{
		byte[] identifierBytes = Encoding.UTF8.GetBytes(identifier);
		await Assert.That(LocalReplayBinaryHeader.IdentifierIsValid(identifierBytes, out _)).IsEqualTo(isValid);
	}

	[Test]
	[Arguments("DF_RPL2", true)]
	[Arguments("DF_RPL22", true)]
	[Arguments("DF_RPL22abc", true)]
	[Arguments("Df_RPL2", false)]
	[Arguments("DF_RPL", false)]
	[Arguments("DF", false)]
	[Arguments("ddrpl.", false)]
	[Arguments("", false)]
	[Arguments("D_F1PL2", false)]
	public async Task TestValidateLeaderboardReplayHeaderIdentifier(string identifier, bool isValid)
	{
		byte[] identifierBytes = Encoding.UTF8.GetBytes(identifier);
		await Assert.That(LeaderboardReplayBinaryHeader.IdentifierIsValid(identifierBytes, out _)).IsEqualTo(isValid);
	}
}
