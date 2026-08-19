using System.Numerics;

namespace DevilDaggersInfo.Core.Spawnset.Test;

// TODO: Refactor and add separate tests for calculating sections. Probably need to store all the expected spawnset data somewhere else.
internal sealed class SpawnsetParseTests
{
	[Test]
	public async Task Parse_V0()
	{
		SpawnsetBinary spawnset = await Parse("V0", 4, 8, 50, 20, 0.025f, 60, GameMode.Survival, default, 400, 250, 120, 60, 82, HandLevel.Level1, 0, 0, new SpawnSectionInfo(57, 275), new SpawnSectionInfo(18, 30));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 3));
		await Assert.That(spawnset.Spawns[1]).IsEqualTo(new Spawn(EnemyType.Empty, 6));
	}

	[Test]
	public async Task Parse_V1()
	{
		SpawnsetBinary spawnset = await Parse("V1", 4, 8, 50, 20, 0.025f, 60, GameMode.Survival, default, 400, 250, 120, 60, 130, HandLevel.Level1, 0, 0, new SpawnSectionInfo(99, 421), new SpawnSectionInfo(21, 54));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 3));
		await Assert.That(spawnset.Spawns[1]).IsEqualTo(new Spawn(EnemyType.Empty, 6));
	}

	[Test]
	public async Task Parse_V2()
	{
		SpawnsetBinary spawnset = await Parse("V2", 4, 9, 50, 20, 0.025f, 60, GameMode.Survival, default, 500, 250, 120, 60, 87, HandLevel.Level1, 0, 0, new SpawnSectionInfo(71, 375), new SpawnSectionInfo(7, 58));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 3));
		await Assert.That(spawnset.Spawns[1]).IsEqualTo(new Spawn(EnemyType.Empty, 6));
	}

	[Test]
	public async Task Parse_V3()
	{
		SpawnsetBinary spawnset = await Parse("V3", 4, 9, 50, 20, 0.025f, 60, GameMode.Survival, default, 500, 250, 120, 60, 118, HandLevel.Level1, 0, 0, new SpawnSectionInfo(90, 451), new SpawnSectionInfo(17, 56));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 3));
		await Assert.That(spawnset.Spawns[1]).IsEqualTo(new Spawn(EnemyType.Empty, 6));
	}

	[Test]
	public async Task Parse_V3_229()
	{
		SpawnsetBinary spawnset = await Parse("V3_229", 6, 9, 44.275f, 20, 0.025f, 60, GameMode.Survival, default, 500, 250, 120, 60, 75, HandLevel.Level3, 57, 229, new SpawnSectionInfo(52, 222), new SpawnSectionInfo(17, 56));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid1, 0));
		await Assert.That(spawnset.Spawns[6]).IsEqualTo(new Spawn(EnemyType.Squid2, 10));
	}

	[Test]
	public async Task Parse_V3_451()
	{
		SpawnsetBinary spawnset = await Parse("V3_451", 6, 9, 38.725f, 20, 0.025f, 60, GameMode.Survival, default, 500, 250, 120, 60, 18, HandLevel.Level4, 0, 451, new SpawnSectionInfo(0, null), new SpawnSectionInfo(17, 56));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Empty, 5));
	}

	[Test]
	public async Task Parse_Empty()
	{
		await Parse("Empty", 6, 9, 50, 20, 0.025f, 60, GameMode.Survival, default, 500, 250, 120, 60, 0, HandLevel.Level1, 0, 0, new SpawnSectionInfo(0, null), new SpawnSectionInfo(0, null));
	}

	[Test]
	public async Task Parse_Scanner()
	{
		SpawnsetBinary spawnset = await Parse("Scanner", 6, 9, 26, 15, 0.025f, 60, GameMode.Survival, default, 500, 250, 120, 60, 125, HandLevel.Level4, 30, 0, new SpawnSectionInfo(62, 16), new SpawnSectionInfo(62, 21));
		await Assert.That(spawnset.Spawns[0]).IsEqualTo(new Spawn(EnemyType.Squid2, 0));
		await Assert.That(spawnset.Spawns[30]).IsEqualTo(new Spawn(EnemyType.Spider1, 5));
	}

	[Test]
	public async Task Parse_EmptySpawn()
	{
		await Parse("EmptySpawn", 6, 9, 50, 20, 0.025f, 60, GameMode.Survival, default, 500, 250, 120, 60, 1, HandLevel.Level1, 0, 0, new SpawnSectionInfo(0, null), new SpawnSectionInfo(0, null));
	}

	[Test]
	public async Task Parse_NoEndLoop()
	{
		await Parse("NoEndLoop", 6, 9, 50, 20, 0.025f, 60, GameMode.Survival, default, 500, 250, 120, 60, 3, HandLevel.Level1, 0, 0, new SpawnSectionInfo(2, 2), new SpawnSectionInfo(0, null));
	}

	[Test]
	public async Task Parse_TimeAttack()
	{
		await Parse("TimeAttack", 6, 9, 50, 20, 0.025f, 60, GameMode.TimeAttack, default, 500, 250, 120, 60, 1, HandLevel.Level1, 0, 0, new SpawnSectionInfo(1, 1), new SpawnSectionInfo(0, null));
	}

	[Test]
	public async Task Parse_Metathrone()
	{
		await Parse("Metathrone", 5, 9, 6105.9f, 27, 11.5f, 180, GameMode.Survival, default, 500, 250, 120, 60, 164, HandLevel.Level1, 0, 0, new SpawnSectionInfo(134, 691.6f), new SpawnSectionInfo(9, 42.4f));
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
