namespace DevilDaggersInfo.Core.Spawnset.Test;

internal sealed class SpawnsetBinaryTests
{
	[Test]
	[Arguments("V0")]
	[Arguments("V1")]
	[Arguments("V2")]
	[Arguments("V3")]
	[Arguments("V3_229")]
	[Arguments("V3_451")]
	[Arguments("Empty")]
	[Arguments("EmptySpawn")]
	[Arguments("NoEndLoop")]
	[Arguments("TimeAttack")]
	[Arguments("Scanner")]
	public async Task CompareBinaryOutput(string fileName)
	{
		byte[] originalBytes = await File.ReadAllBytesAsync(Path.Combine("Resources", fileName));
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(originalBytes);
		byte[] bytes = spawnset.ToBytes();

		await Assert.That(bytes).IsEquivalentTo(originalBytes, CollectionOrdering.Matching);
	}

	[Test]
	public async Task TestEffectivePlayerSettings()
	{
		byte[] originalBytes = await File.ReadAllBytesAsync(Path.Combine("Resources", "V3"));
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(originalBytes) with
		{
			HandLevel = HandLevel.Level2,
			AdditionalGems = 40,
		};

		// The effective player setting should be default when using the default spawnset (or any spawnset with spawn version 4).
		EffectivePlayerSettings settings = spawnset.GetEffectivePlayerSettings();
		await Assert.That(settings.HandLevel).IsEqualTo(HandLevel.Level1);
		await Assert.That(settings.GemsOrHoming).IsEqualTo(0);
		await Assert.That(settings.HandMesh).IsEqualTo(HandLevel.Level1);

		spawnset = spawnset with
		{
			SpawnVersion = 5, // Specified player settings should be effective from version 5.
		};
		settings = spawnset.GetEffectivePlayerSettings();
		await Assert.That(settings.HandLevel).IsEqualTo(HandLevel.Level2);
		await Assert.That(settings.GemsOrHoming).IsEqualTo(50);
		await Assert.That(settings.HandMesh).IsEqualTo(HandLevel.Level2);
	}

	[Test]
	public async Task TestEffectiveTimerStart()
	{
		byte[] originalBytes = await File.ReadAllBytesAsync(Path.Combine("Resources", "V3"));
		SpawnsetBinary spawnset = SpawnsetBinary.Parse(originalBytes) with
		{
			TimerStart = 10,
		};

		// The effective timer start should be default when using the default spawnset (or any spawnset with spawn version 5 or lower).
		float timerStart = spawnset.GetEffectiveTimerStart();
		await Assert.That(timerStart).IsEqualTo(0);

		spawnset = spawnset with
		{
			SpawnVersion = 6, // Specified timer start should be effective from version 6.
		};
		timerStart = spawnset.GetEffectiveTimerStart();
		await Assert.That(timerStart).IsEqualTo(10);
	}
}
