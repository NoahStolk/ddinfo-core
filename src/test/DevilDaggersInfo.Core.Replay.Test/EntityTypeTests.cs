// using DevilDaggersInfo.Core.Replay.Events.Enums;
//
// namespace DevilDaggersInfo.Core.Replay.Test;
//
// // TODO: Move tests to ddinfo-tools.
// internal sealed class EntityTypeTests
// {
// 	[Test]
// 	[Arguments(-2, null)]
// 	[Arguments(-1, null)]
// 	[Arguments(0, EntityType.Zero)]
// 	[Arguments(1, EntityType.Centipede)]
// 	[Arguments(2, EntityType.Thorn)]
// 	[Arguments(3, EntityType.Spider1)]
// 	[Arguments(4, EntityType.Squid1)]
// 	[Arguments(5, EntityType.Level4Dagger)]
// 	[Arguments(150, EntityType.Level4Dagger)]
// 	[Arguments(163, EntityType.Skull1)]
// 	[Arguments(173, EntityType.Skull2)]
// 	[Arguments(743, EntityType.Level4Dagger)]
// 	public async Task GetEntityType(int entityId, EntityType? expectedEntityType)
// 	{
// 		ReplayBinary<LocalReplayBinaryHeader> replayBinary = new(await File.ReadAllBytesAsync(Path.Combine("Resources", "SquidSpiderCentiThorn.ddreplay")));
// 		await Assert.That(replayBinary.EventsData.GetEntityType(entityId)).IsEqualTo(expectedEntityType);
// 	}
//
// 	[Test]
// 	[Arguments(-2, null)]
// 	[Arguments(-1, EntityType.Centipede)]
// 	[Arguments(0, EntityType.Zero)]
// 	[Arguments(1, EntityType.Centipede)]
// 	[Arguments(2, EntityType.Thorn)]
// 	[Arguments(3, EntityType.Spider1)]
// 	[Arguments(4, EntityType.Squid1)]
// 	[Arguments(5, EntityType.Level4Dagger)]
// 	[Arguments(150, EntityType.Level4Dagger)]
// 	[Arguments(163, EntityType.Skull1)]
// 	[Arguments(173, EntityType.Skull2)]
// 	[Arguments(743, EntityType.Level4Dagger)]
// 	public async Task GetEntityTypeIncludingNegated(int entityId, EntityType? expectedEntityType)
// 	{
// 		ReplayBinary<LocalReplayBinaryHeader> replayBinary = new(await File.ReadAllBytesAsync(Path.Combine("Resources", "SquidSpiderCentiThorn.ddreplay")));
// 		await Assert.That(replayBinary.EventsData.GetEntityTypeIncludingNegated(entityId)).IsEqualTo(expectedEntityType);
// 	}
// }
