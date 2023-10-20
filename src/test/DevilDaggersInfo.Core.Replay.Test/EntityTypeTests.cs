using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.Test;

[TestClass]
public class EntityTypeTests
{
	[DataTestMethod]
	[DataRow(-2, null)]
	[DataRow(-1, null)]
	[DataRow(0, EntityType.Zero)]
	[DataRow(1, EntityType.Centipede)]
	[DataRow(2, EntityType.Thorn)]
	[DataRow(3, EntityType.Spider1)]
	[DataRow(4, EntityType.Squid1)]
	[DataRow(5, EntityType.Level4Dagger)]
	[DataRow(150, EntityType.Level4Dagger)]
	[DataRow(163, EntityType.Skull1)]
	[DataRow(173, EntityType.Skull2)]
	[DataRow(743, EntityType.Level4Dagger)]
	public void GetEntityType(int entityId, EntityType? expectedEntityType)
	{
		ReplayBinary<LocalReplayBinaryHeader> replayBinary = new(File.ReadAllBytes(Path.Combine("Resources", "SquidSpiderCentiThorn.ddreplay")));
		Assert.AreEqual(expectedEntityType, replayBinary.EventsData.GetEntityType(entityId));
	}

	[DataTestMethod]
	[DataRow(-2, null)]
	[DataRow(-1, EntityType.Centipede)]
	[DataRow(0, EntityType.Zero)]
	[DataRow(1, EntityType.Centipede)]
	[DataRow(2, EntityType.Thorn)]
	[DataRow(3, EntityType.Spider1)]
	[DataRow(4, EntityType.Squid1)]
	[DataRow(5, EntityType.Level4Dagger)]
	[DataRow(150, EntityType.Level4Dagger)]
	[DataRow(163, EntityType.Skull1)]
	[DataRow(173, EntityType.Skull2)]
	[DataRow(743, EntityType.Level4Dagger)]
	public void GetEntityTypeIncludingNegated(int entityId, EntityType? expectedEntityType)
	{
		ReplayBinary<LocalReplayBinaryHeader> replayBinary = new(File.ReadAllBytes(Path.Combine("Resources", "SquidSpiderCentiThorn.ddreplay")));
		Assert.AreEqual(expectedEntityType, replayBinary.EventsData.GetEntityTypeIncludingNegated(entityId));
	}
}
