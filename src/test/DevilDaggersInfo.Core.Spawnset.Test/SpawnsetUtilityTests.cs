namespace DevilDaggersInfo.Core.Spawnset.Test;

internal sealed class SpawnsetUtilityTests
{
	[Test]
	[Arguments("V0", true)]
	[Arguments("V1", true)]
	[Arguments("V2", true)]
	[Arguments("V3", true)]
	[Arguments("V3_229", true)]
	[Arguments("V3_451", true)]
	[Arguments("Empty", false)]
	[Arguments("EmptySpawn", false)]
	[Arguments("NoEndLoop", false)]
	[Arguments("TimeAttack", false)]
	[Arguments("Scanner", true)]
	public async Task TestHasEndLoop(string fileName, bool hasEndLoop)
	{
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(await File.ReadAllBytesAsync(Path.Combine("Resources", fileName)));
		await Assert.That(spawnset.HasEndLoop()).IsEqualTo(hasEndLoop);
	}

	[Test]
	[Arguments("V0", true)]
	[Arguments("V1", true)]
	[Arguments("V2", true)]
	[Arguments("V3", true)]
	[Arguments("V3_229", true)]
	[Arguments("V3_451", true)]
	[Arguments("Empty", false)]
	[Arguments("EmptySpawn", false)]
	[Arguments("NoEndLoop", true)]
	[Arguments("TimeAttack", true)]
	[Arguments("Scanner", true)]
	public async Task TestHasSpawns(string fileName, bool hasSpawns)
	{
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(await File.ReadAllBytesAsync(Path.Combine("Resources", fileName)));
		await Assert.That(spawnset.HasSpawns()).IsEqualTo(hasSpawns);
	}

	[Test]
	[Arguments("V0", 63)]
	[Arguments("V1", 108)]
	[Arguments("V2", 79)]
	[Arguments("V3", 100)]
	[Arguments("V3_229", 57)]
	[Arguments("V3_451", 0)]
	[Arguments("Empty", 0)]
	[Arguments("EmptySpawn", 0)]
	[Arguments("NoEndLoop", 2)]
	[Arguments("TimeAttack", -1)]
	[Arguments("Scanner", 62)]
	public async Task TestLoopStartIndex(string fileName, int loopStartIndex)
	{
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(await File.ReadAllBytesAsync(Path.Combine("Resources", fileName)));
		if (loopStartIndex == -1)
			Assert.ThrowsExactly<InvalidOperationException>(() => spawnset.GetLoopStartIndex());
		else
			await Assert.That(spawnset.GetLoopStartIndex()).IsEqualTo(loopStartIndex);
	}

	[Test]
	[Arguments(50f, 20f, 0.025f, 1200f)]
	[Arguments(30f, 20f, 1f, 10f)]
	[Arguments(30f, 5f, 1f, 25f)]
	[Arguments(30f, 0f, 1f, 30f)]
	[Arguments(30f, -1f, 1f, 30f)]
	[Arguments(26f, 15f, 0.025f, 440f)]
	[Arguments(50f, 20f, 0f, 0f)]
	[Arguments(50f, 20f, -1f, 0f)]
	[Arguments(30f, 40f, 1f, 0f)]
	[Arguments(6105.9f, 27f, 11.5f, 528.6f)]
	[Arguments(0f, 29f, 3f, 0f)]
	public async Task TestShrinkEndTime(float start, float end, float rate, float expectedFinalShrinkSecond)
	{
		SpawnsetBinary spawnset = SpawnsetBinary.CreateDefault() with
		{
			ShrinkStart = start,
			ShrinkEnd = end,
			ShrinkRate = rate,
		};

		await Assert.That(spawnset.GetShrinkEndTime()).IsEqualTo(expectedFinalShrinkSecond).Within(0.0001f);
	}

	[Test]
	[Arguments(30f, 0f, 1f, 25, 17, 0f)]
	[Arguments(30f, 0f, 1f, 25, 18, 2f)]
	[Arguments(30f, 0f, 1f, 25, 19, 6f)]
	[Arguments(30f, 0f, 1f, 25, 20, 10f)]
	[Arguments(30f, 0f, 1f, 25, 21, 14f)]
	[Arguments(30f, 0f, 1f, 25, 22, 18f)]
	[Arguments(30f, 0f, 1f, 25, 23, 22f)]
	[Arguments(30f, 0f, 1f, 25, 24, 26f)]
	[Arguments(30f, 0f, 1f, 25, 25, float.MaxValue)]

	[Arguments(30f, 0f, 2f, 25, 17, 0f)]
	[Arguments(30f, 0f, 2f, 25, 18, 1f)]
	[Arguments(30f, 0f, 2f, 25, 19, 3f)]
	[Arguments(30f, 0f, 2f, 25, 20, 5f)]
	[Arguments(30f, 0f, 2f, 25, 21, 7f)]
	[Arguments(30f, 0f, 2f, 25, 22, 9f)]
	[Arguments(30f, 0f, 2f, 25, 23, 11f)]
	[Arguments(30f, 0f, 2f, 25, 24, 13f)]
	[Arguments(30f, 0f, 2f, 25, 25, float.MaxValue)]

	[Arguments(30f, 10f, 2f, 25, 17, 0f)]
	[Arguments(30f, 10f, 2f, 25, 18, 1f)]
	[Arguments(30f, 10f, 2f, 25, 19, 3f)]
	[Arguments(30f, 10f, 2f, 25, 20, 5f)]
	[Arguments(30f, 10f, 2f, 25, 21, 7f)]
	[Arguments(30f, 10f, 2f, 25, 22, 9f)]
	[Arguments(30f, 10f, 2f, 25, 23, float.MaxValue)]
	[Arguments(30f, 10f, 2f, 25, 24, float.MaxValue)]
	[Arguments(30f, 10f, 2f, 25, 25, float.MaxValue)]

	[Arguments(30f, 40f, 1f, 25, 25, float.MaxValue)]
	[Arguments(30f, 40f, 1f, 25, 5, 0f)]

	[Arguments(30f, 30f, 1f, 25, 25, float.MaxValue)]
	[Arguments(30f, 30f, 1f, 25, 5, 0f)]

	[Arguments(30f, 10f, 0f, 25, 25, float.MaxValue)]
	[Arguments(30f, 10f, 0f, 25, 5, 0f)]

	[Arguments(30f, 10f, -1f, 25, 25, float.MaxValue)]
	[Arguments(30f, 10f, -1f, 25, 5, 0f)]

	[Arguments(0f, 29f, 3f, 25, 25, float.MaxValue)]
	[Arguments(0f, 29f, 3f, 27, 21, float.MaxValue)]
	[Arguments(0f, 29f, 3f, 25, 15, 0f)]
	public async Task TestShrinkTimeForTile(float start, float end, float rate, int x, int y, float expectedTime)
	{
		SpawnsetBinary spawnset = SpawnsetBinary.CreateDefault() with
		{
			ShrinkStart = start,
			ShrinkEnd = end,
			ShrinkRate = rate,
		};

		await Assert.That(spawnset.GetShrinkTimeForTile(x, y)).IsEqualTo(expectedTime).Within(0.0001f);
	}

	[Test]
	[Arguments(48, 0, 37, 25)]
	[Arguments(-48, 0, 13, 25)]
	[Arguments(0, 48, 25, 37)]
	[Arguments(0, -48, 25, 13)]

	[Arguments(47, 0, 37, 25)]
	[Arguments(46, 0, 37, 25)]
	[Arguments(45, 0, 36, 25)]
	public async Task TestRaceDaggerGridPosition(float raceDaggerX, float raceDaggerZ, int expectedTileX, int expectedTileZ)
	{
		SpawnsetBinary defaultSpawnset = SpawnsetBinary.CreateDefault();
		int x = defaultSpawnset.WorldToTileCoordinate(raceDaggerX);
		int z = defaultSpawnset.WorldToTileCoordinate(raceDaggerZ);
		await Assert.That(x).IsEqualTo(expectedTileX);
		await Assert.That(z).IsEqualTo(expectedTileZ);
	}
}
