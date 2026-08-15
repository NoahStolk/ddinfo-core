namespace DevilDaggersInfo.Core.Wiki.Test;

internal sealed class DaggerTests
{
	[Test]
	public async Task TestLeviathanDagger()
	{
		const int seconds = 1000;

		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, seconds)).IsEqualTo(Daggers.Leviathan);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, seconds)).IsEqualTo(Daggers.Leviathan);
	}

	[Test]
	public async Task TestDevilDagger()
	{
		const double secondsLastV3Next = 999.9999;
		const int secondsLastV3 = 1000;
		const int secondsFirst = 500;

		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsLastV3Next)).IsEqualTo(Daggers.Devil);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsFirst)).IsEqualTo(Daggers.Devil);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsLastV3Next)).IsEqualTo(Daggers.Devil);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsFirst)).IsEqualTo(Daggers.Devil);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsLastV3)).IsEqualTo(Daggers.Devil);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsFirst)).IsEqualTo(Daggers.Devil);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsLastV3)).IsEqualTo(Daggers.Devil);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsFirst)).IsEqualTo(Daggers.Devil);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsLastV3)).IsEqualTo(Daggers.Devil);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsFirst)).IsEqualTo(Daggers.Devil);
	}

	[Test]
	public async Task TestGoldenDagger()
	{
		const double secondsLast = 499.9999;
		const int secondsFirst = 250;

		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsLast)).IsEqualTo(Daggers.Golden);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsFirst)).IsEqualTo(Daggers.Golden);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsLast)).IsEqualTo(Daggers.Golden);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsFirst)).IsEqualTo(Daggers.Golden);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsLast)).IsEqualTo(Daggers.Golden);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsFirst)).IsEqualTo(Daggers.Golden);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsLast)).IsEqualTo(Daggers.Golden);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsFirst)).IsEqualTo(Daggers.Golden);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsLast)).IsEqualTo(Daggers.Golden);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsFirst)).IsEqualTo(Daggers.Golden);
	}

	[Test]
	public async Task TestSilverDagger()
	{
		const double secondsLast = 249.9999;
		const int secondsFirst = 120;

		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsLast)).IsEqualTo(Daggers.Silver);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsFirst)).IsEqualTo(Daggers.Silver);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsLast)).IsEqualTo(Daggers.Silver);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsFirst)).IsEqualTo(Daggers.Silver);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsLast)).IsEqualTo(Daggers.Silver);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsFirst)).IsEqualTo(Daggers.Silver);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsLast)).IsEqualTo(Daggers.Silver);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsFirst)).IsEqualTo(Daggers.Silver);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsLast)).IsEqualTo(Daggers.Silver);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsFirst)).IsEqualTo(Daggers.Silver);
	}

	[Test]
	public async Task TestBronzeDagger()
	{
		const double secondsLast = 119.9999;
		const int secondsFirst = 60;

		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsLast)).IsEqualTo(Daggers.Bronze);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsFirst)).IsEqualTo(Daggers.Bronze);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsLast)).IsEqualTo(Daggers.Bronze);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsFirst)).IsEqualTo(Daggers.Bronze);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsLast)).IsEqualTo(Daggers.Bronze);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsFirst)).IsEqualTo(Daggers.Bronze);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsLast)).IsEqualTo(Daggers.Bronze);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsFirst)).IsEqualTo(Daggers.Bronze);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsLast)).IsEqualTo(Daggers.Bronze);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsFirst)).IsEqualTo(Daggers.Bronze);
	}

	[Test]
	public async Task TestDefaultDagger()
	{
		const double secondsLast = 59.9999;
		const int secondsFirst = 0;

		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsLast)).IsEqualTo(Daggers.Default);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_2, secondsFirst)).IsEqualTo(Daggers.Default);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsLast)).IsEqualTo(Daggers.Default);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_1, secondsFirst)).IsEqualTo(Daggers.Default);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsLast)).IsEqualTo(Daggers.Default);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V3_0, secondsFirst)).IsEqualTo(Daggers.Default);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsLast)).IsEqualTo(Daggers.Default);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V2_0, secondsFirst)).IsEqualTo(Daggers.Default);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsLast)).IsEqualTo(Daggers.Default);
		await Assert.That(Daggers.GetDaggerFromSeconds(GameVersion.V1_0, secondsFirst)).IsEqualTo(Daggers.Default);
	}

	[Test]
	public void TestOutOfRange()
	{
		Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => Daggers.GetDaggerFromSeconds(GameVersion.V1_0, -1));
		Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => Daggers.GetDaggerFromSeconds(GameVersion.V2_0, -1));
		Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => Daggers.GetDaggerFromSeconds(GameVersion.V3_0, -1));
		Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => Daggers.GetDaggerFromSeconds(GameVersion.V3_1, -1));
		Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => Daggers.GetDaggerFromSeconds(GameVersion.V3_2, -1));
	}
}
