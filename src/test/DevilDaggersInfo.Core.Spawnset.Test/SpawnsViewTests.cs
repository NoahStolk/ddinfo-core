using DevilDaggersInfo.Core.Wiki;

namespace DevilDaggersInfo.Core.Spawnset.Test;

internal sealed class SpawnsViewTests
{
	[Test]
	public async Task TestSpawnsView_V0()
	{
		SpawnsView spawnsView = await Parse("V0", GameVersion.V1_0, 3, 57, 18);

		await AreEqual(new(EnemyType.Squid1, 3, 2, new(HandLevel.Level1, 2, 2)), spawnsView.PreLoop[0]);
		await AreEqual(new(EnemyType.Squid1, 14, 2, new(HandLevel.Level1, 4, 4)), spawnsView.PreLoop[1]);

		await AreEqual(new(EnemyType.Squid2, 300, 3, new(HandLevel.Level3, 111, 181)), spawnsView.Waves[0][0]);
		await AreEqual(new(EnemyType.Squid2, 300, 3, new(HandLevel.Level3, 114, 184)), spawnsView.Waves[0][1]);
	}

	[Test]
	public async Task TestSpawnsView_V2()
	{
		SpawnsView spawnsView = await Parse("V2", GameVersion.V2_0, 3, 71, 7);
		await AreEqual(new(EnemyType.Squid1, 3, 2, new(HandLevel.Level1, 2, 2)), spawnsView.PreLoop[0]);
		await AreEqual(new(EnemyType.Squid1, 14, 2, new(HandLevel.Level1, 4, 4)), spawnsView.PreLoop[1]);

		await AreEqual(new(EnemyType.Squid3, 397, 3, new(HandLevel.Level4, 132, 352)), spawnsView.Waves[0][0]);
		await AreEqual(new(EnemyType.Squid2, 403, 3, new(HandLevel.Level4, 135, 355)), spawnsView.Waves[0][1]);

		await AreEqual(new(EnemyType.Gigapede, 415, 50, new(HandLevel.Level4, 187, 407)), spawnsView.Waves[0][3]);
		await AreEqual(new(EnemyType.Gigapede, 468.5667f, 50, new(HandLevel.Level4, 272, 492)), spawnsView.Waves[1][3]);
		await AreEqual(new(EnemyType.Gigapede, 516.5667f, 50, new(HandLevel.Level4, 357, 577)), spawnsView.Waves[2][3]);
	}

	[Test]
	public async Task TestSpawnsView_V2_In_V3()
	{
		SpawnsView spawnsView = await Parse("V2", GameVersion.V3_0, 3, 71, 7);
		await AreEqual(new(EnemyType.Squid1, 3, 2, new(HandLevel.Level1, 2, 2)), spawnsView.PreLoop[0]);
		await AreEqual(new(EnemyType.Squid1, 14, 2, new(HandLevel.Level1, 4, 4)), spawnsView.PreLoop[1]);

		await AreEqual(new(EnemyType.Squid3, 397, 3, new(HandLevel.Level4, 132, 352)), spawnsView.Waves[0][0]);
		await AreEqual(new(EnemyType.Squid2, 403, 3, new(HandLevel.Level4, 135, 355)), spawnsView.Waves[0][1]);

		await AreEqual(new(EnemyType.Gigapede, 415, 50, new(HandLevel.Level4, 187, 407)), spawnsView.Waves[0][3]);
		await AreEqual(new(EnemyType.Gigapede, 468.5667f, 50, new(HandLevel.Level4, 272, 492)), spawnsView.Waves[1][3]);
		await AreEqual(new(EnemyType.Ghostpede, 516.5667f, 10, new(HandLevel.Level4, 317, 537)), spawnsView.Waves[2][3]);
	}

	[Test]
	public async Task TestSpawns_PracticeSettings()
	{
		const string fileName = "V3";
		const GameVersion gameVersion = GameVersion.V3_0;
		const int waveCount = 1;
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(await File.ReadAllBytesAsync(Path.Combine("Resources", fileName)));
		SpawnsView spawnsView = new(spawnset, gameVersion, waveCount);
		await Assert.That(spawnsView.PreLoop[0].Seconds).IsEqualTo(3);
		await Assert.That(spawnsView.PreLoop[0].GemState).IsEqualTo(new(HandLevel.Level1, 2, 2));

		spawnset = spawnset with
		{
			TimerStart = 10,
			HandLevel = HandLevel.Level2,
			AdditionalGems = 5,
		};
		spawnsView = new(spawnset, gameVersion, waveCount);

		// Settings should not be effective for spawn version 4.
		await Assert.That(spawnsView.PreLoop[0].Seconds).IsEqualTo(3);
		await Assert.That(spawnsView.PreLoop[0].GemState).IsEqualTo(new(HandLevel.Level1, 2, 2));

		spawnset = spawnset with { SpawnVersion = 5 };
		spawnsView = new(spawnset, gameVersion, waveCount);

		// Only hand and gem settings should be effective for spawn version 5.
		await Assert.That(spawnsView.PreLoop[0].Seconds).IsEqualTo(3);
		await Assert.That(spawnsView.PreLoop[0].GemState).IsEqualTo(new(HandLevel.Level2, 17, 2));

		spawnset = spawnset with { SpawnVersion = 6 };
		spawnsView = new(spawnset, gameVersion, waveCount);

		// All settings should be effective for spawn version 6.
		await Assert.That(spawnsView.PreLoop[0].Seconds).IsEqualTo(13);
		await Assert.That(spawnsView.PreLoop[0].GemState).IsEqualTo(new(HandLevel.Level2, 17, 2));
	}

	[AssertionMethod]
	private static async Task<SpawnsView> Parse(string fileName, GameVersion gameVersion, int waveCount, int expectedPreLoopSpawnCount, int expectedWaveSpawnCount)
	{
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(await File.ReadAllBytesAsync(Path.Combine("Resources", fileName)));
		SpawnsView spawnsView = new(spawnset, gameVersion, waveCount);
		await Assert.That(spawnsView.Waves.Length).IsEqualTo(waveCount);
		await Assert.That(spawnsView.PreLoop.Count).IsEqualTo(expectedPreLoopSpawnCount);
		await Assert.That(Array.TrueForAll(spawnsView.Waves, l => l.Count == expectedWaveSpawnCount)).IsTrue();
		return spawnsView;
	}

	[AssertionMethod]
	private static async Task AreEqual(SpawnView a, SpawnView b)
	{
		await Assert.That(b.EnemyType).IsEqualTo(a.EnemyType);
		await Assert.That(b.Seconds).IsEqualTo(a.Seconds).Within(0.0166f); // Allow 1 frame difference.
		await Assert.That(b.NoFarmGems).IsEqualTo(a.NoFarmGems);
		await Assert.That(b.GemState).IsEqualTo(a.GemState);
	}

	[Test]
	[Arguments("Empty", false, false)]
	[Arguments("EmptySpawn", false, false)]
	[Arguments("NoEndLoop", true, false)]
	[Arguments("LoopOnly", false, true)]
	[Arguments("Scanner", true, true)]
	[Arguments("V3", true, true)]
	[Arguments("TimeAttack", true, false)]
	[Arguments("RacePede", true, false)]
	[Arguments("Race", false, false)]
	public async Task TestHasSpawns(string fileName, bool expectedHasPreLoopSpawns, bool expectedHasLoopSpawns)
	{
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(await File.ReadAllBytesAsync(Path.Combine("Resources", fileName)));
		SpawnsView spawnsView = new(spawnset, GameVersion.V3_2);
		await Assert.That(spawnsView.HasPreLoopSpawns).IsEqualTo(expectedHasPreLoopSpawns);
		await Assert.That(spawnsView.HasLoopSpawns).IsEqualTo(expectedHasLoopSpawns);
	}
}
