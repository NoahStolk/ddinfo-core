using System.Numerics;

namespace DevilDaggersInfo.Core.Spawnset.Test;

// TODO: Refactor and add separate tests for calculating sections. Probably need to store all the expected spawnset data somewhere else.
internal sealed class SpawnsetParseTests
{
	[Test]
	public async Task Parse_V0()
	{
		SpawnsetBinary spawnset = await Parse(
			fileName: "V0",
			expectedSpawnVersion: 4,
			expectedWorldVersion: 8,
			expectedShrinkStart: 50,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 400,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 82,
			expectedHandLevel: HandLevel.Level1,
			expectedAdditionalGems: 0,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(57, 275),
			expectedLoopSection: new SpawnSectionInfo(18, 30));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 3));
		await Assert.That(spawnset.Spawns[1]).IsEqualTo(new Spawn(EnemyType.Empty, 6));
	}

	[Test]
	public async Task Parse_V1()
	{
		SpawnsetBinary spawnset = await Parse(
			fileName: "V1",
			expectedSpawnVersion: 4,
			expectedWorldVersion: 8,
			expectedShrinkStart: 50,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 400,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 130,
			expectedHandLevel: HandLevel.Level1,
			expectedAdditionalGems: 0,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(99, 421),
			expectedLoopSection: new SpawnSectionInfo(21, 54));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 3));
		await Assert.That(spawnset.Spawns[1]).IsEqualTo(new Spawn(EnemyType.Empty, 6));
	}

	[Test]
	public async Task Parse_V2()
	{
		SpawnsetBinary spawnset = await Parse(
			fileName: "V2",
			expectedSpawnVersion: 4,
			expectedWorldVersion: 9,
			expectedShrinkStart: 50,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 87,
			expectedHandLevel: HandLevel.Level1,
			expectedAdditionalGems: 0,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(71, 375),
			expectedLoopSection: new SpawnSectionInfo(7, 58));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 3));
		await Assert.That(spawnset.Spawns[1]).IsEqualTo(new Spawn(EnemyType.Empty, 6));
	}

	[Test]
	public async Task Parse_V3()
	{
		SpawnsetBinary spawnset = await Parse(
			fileName: "V3",
			expectedSpawnVersion: 4,
			expectedWorldVersion: 9,
			expectedShrinkStart: 50,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 118,
			expectedHandLevel: HandLevel.Level1,
			expectedAdditionalGems: 0,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(90, 451),
			expectedLoopSection: new SpawnSectionInfo(17, 56));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 3));
		await Assert.That(spawnset.Spawns[1]).IsEqualTo(new Spawn(EnemyType.Empty, 6));
	}

	[Test]
	public async Task Parse_V3_229()
	{
		SpawnsetBinary spawnset = await Parse(
			fileName: "V3_229",
			expectedSpawnVersion: 6,
			expectedWorldVersion: 9,
			expectedShrinkStart: 44.275f,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 75,
			expectedHandLevel: HandLevel.Level3,
			expectedAdditionalGems: 57,
			expectedTimerStart: 229,
			expectedPreLoopSection: new SpawnSectionInfo(52, 222),
			expectedLoopSection: new SpawnSectionInfo(17, 56));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 0));
		await Assert.That(spawnset.Spawns[6]).IsEqualTo(new Spawn(EnemyType.Squid2, 10));
	}

	[Test]
	public async Task Parse_V3_451()
	{
		SpawnsetBinary spawnset = await Parse(
			fileName: "V3_451",
			expectedSpawnVersion: 6,
			expectedWorldVersion: 9,
			expectedShrinkStart: 38.725f,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 18,
			expectedHandLevel: HandLevel.Level4,
			expectedAdditionalGems: 0,
			expectedTimerStart: 451,
			expectedPreLoopSection: new SpawnSectionInfo(0, null),
			expectedLoopSection: new SpawnSectionInfo(17, 56));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Empty, 5));
	}

	[Test]
	public async Task Parse_Empty()
	{
		await Parse(
			fileName: "Empty",
			expectedSpawnVersion: 6,
			expectedWorldVersion: 9,
			expectedShrinkStart: 50,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 0,
			expectedHandLevel: HandLevel.Level1,
			expectedAdditionalGems: 0,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(0, null),
			expectedLoopSection: new SpawnSectionInfo(0, null));
	}

	[Test]
	public async Task Parse_Scanner()
	{
		SpawnsetBinary spawnset = await Parse(
			fileName: "Scanner",
			expectedSpawnVersion: 6,
			expectedWorldVersion: 9,
			expectedShrinkStart: 26,
			expectedShrinkEnd: 15,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 125,
			expectedHandLevel: HandLevel.Level4,
			expectedAdditionalGems: 30,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(62, 16),
			expectedLoopSection: new SpawnSectionInfo(62, 21));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid2, 0));
		await Assert.That(spawnset.Spawns[30]).IsEqualTo(new Spawn(EnemyType.Spider1, 5));
	}

	[Test]
	public async Task Parse_EmptySpawn()
	{
		await Parse(
			fileName: "EmptySpawn",
			expectedSpawnVersion: 6,
			expectedWorldVersion: 9,
			expectedShrinkStart: 50,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 1,
			expectedHandLevel: HandLevel.Level1,
			expectedAdditionalGems: 0,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(0, null),
			expectedLoopSection: new SpawnSectionInfo(0, null));
	}

	[Test]
	public async Task Parse_NoEndLoop()
	{
		await Parse(
			fileName: "NoEndLoop",
			expectedSpawnVersion: 6,
			expectedWorldVersion: 9,
			expectedShrinkStart: 50,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 3,
			expectedHandLevel: HandLevel.Level1,
			expectedAdditionalGems: 0,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(2, 2),
			expectedLoopSection: new SpawnSectionInfo(0, null));
	}

	[Test]
	public async Task Parse_TimeAttack()
	{
		await Parse(
			fileName: "TimeAttack",
			expectedSpawnVersion: 6,
			expectedWorldVersion: 9,
			expectedShrinkStart: 50,
			expectedShrinkEnd: 20,
			expectedShrinkRate: 0.025f,
			expectedBrightness: 60,
			expectedGameMode: GameMode.TimeAttack,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 1,
			expectedHandLevel: HandLevel.Level1,
			expectedAdditionalGems: 0,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(1, 1),
			expectedLoopSection: new SpawnSectionInfo(0, null));
	}

	[Test]
	public async Task Parse_Metathrone()
	{
		await Parse(
			fileName: "Metathrone",
			expectedSpawnVersion: 5,
			expectedWorldVersion: 9,
			expectedShrinkStart: 6105.9f,
			expectedShrinkEnd: 27,
			expectedShrinkRate: 11.5f,
			expectedBrightness: 180,
			expectedGameMode: GameMode.Survival,
			expectedRaceDaggerPosition: default,
			expectedUnusedDevilTime: 500,
			expectedUnusedGoldenTime: 250,
			expectedUnusedSilverTime: 120,
			expectedUnusedBronzeTime: 60,
			expectedSpawnCount: 164,
			expectedHandLevel: HandLevel.Level1,
			expectedAdditionalGems: 0,
			expectedTimerStart: 0,
			expectedPreLoopSection: new SpawnSectionInfo(134, 691.6f),
			expectedLoopSection: new SpawnSectionInfo(9, 42.4f));
	}

	[AssertionMethod]
	private static async Task<SpawnsetBinary> Parse(
		string fileName,
		int expectedSpawnVersion,
		int expectedWorldVersion,
		float expectedShrinkStart,
		float expectedShrinkEnd,
		float expectedShrinkRate,
		float expectedBrightness,
		GameMode expectedGameMode,
		Vector2 expectedRaceDaggerPosition,
		int expectedUnusedDevilTime,
		int expectedUnusedGoldenTime,
		int expectedUnusedSilverTime,
		int expectedUnusedBronzeTime,
		int expectedSpawnCount,
		HandLevel expectedHandLevel,
		int expectedAdditionalGems,
		float expectedTimerStart,
		SpawnSectionInfo expectedPreLoopSection,
		SpawnSectionInfo expectedLoopSection)
	{
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(await File.ReadAllBytesAsync(Path.Combine("Resources", fileName)));

		await Assert.That(spawnset.SpawnVersion).IsEqualTo(expectedSpawnVersion);
		await Assert.That(spawnset.WorldVersion).IsEqualTo(expectedWorldVersion);
		await Assert.That(spawnset.ShrinkStart).IsEqualTo(expectedShrinkStart).Within(0.001f);
		await Assert.That(spawnset.ShrinkEnd).IsEqualTo(expectedShrinkEnd).Within(0.001f);
		await Assert.That(spawnset.ShrinkRate).IsEqualTo(expectedShrinkRate).Within(0.001f);
		await Assert.That(spawnset.Brightness).IsEqualTo(expectedBrightness).Within(0.001f);
		await Assert.That(spawnset.GameMode).IsEqualTo(expectedGameMode);

		await Assert.That(spawnset.RaceDaggerPosition).IsEqualTo(expectedRaceDaggerPosition);
		await Assert.That(spawnset.UnusedDevilTime).IsEqualTo(expectedUnusedDevilTime);
		await Assert.That(spawnset.UnusedGoldenTime).IsEqualTo(expectedUnusedGoldenTime);
		await Assert.That(spawnset.UnusedSilverTime).IsEqualTo(expectedUnusedSilverTime);
		await Assert.That(spawnset.UnusedBronzeTime).IsEqualTo(expectedUnusedBronzeTime);
		await Assert.That(spawnset.Spawns.Length).IsEqualTo(expectedSpawnCount);

		await Assert.That(spawnset.HandLevel).IsEqualTo(expectedHandLevel);
		await Assert.That(spawnset.AdditionalGems).IsEqualTo(expectedAdditionalGems);
		await Assert.That(spawnset.TimerStart).IsEqualTo(expectedTimerStart);

		(SpawnSectionInfo PreLoopSection, SpawnSectionInfo LoopSection) sections = spawnset.CalculateSections();
		await Assert.That(sections.PreLoopSection).IsEqualTo(expectedPreLoopSection);
		await Assert.That(sections.LoopSection).IsEqualTo(expectedLoopSection);

		return spawnset;
	}
}
