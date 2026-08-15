namespace DevilDaggersInfo.Core.Wiki.Test;

internal sealed class EnemyTests
{
	[Test]
	public async Task TestGetEnemies()
	{
		await Assert.That(Enemies.GetEnemies(GameVersion.V1_0).Count).IsEqualTo(14);
		await Assert.That(Enemies.GetEnemies(GameVersion.V2_0).Count).IsEqualTo(20);
		await Assert.That(Enemies.GetEnemies(GameVersion.V3_0).Count).IsEqualTo(22);
		await Assert.That(Enemies.GetEnemies(GameVersion.V3_1).Count).IsEqualTo(22);
		await Assert.That(Enemies.GetEnemies(GameVersion.V3_2).Count).IsEqualTo(22);
	}

	[Test]
	public async Task TestTransmutedSkull1HomingDamage()
	{
		foreach (GameVersion gameVersion in Enum.GetValues<GameVersion>())
		{
			if (gameVersion is GameVersion.V1_0 or GameVersion.V3_2)
				continue;

			Enemy? originalTransmutedSkull1 = Enemies.GetEnemyByName(gameVersion, "Transmuted Skull I");
			await Assert.That(originalTransmutedSkull1).IsNotNull();
			await Assert.That(originalTransmutedSkull1.HomingDamage.Level3HomingDaggers).IsNotNull();
			await Assert.That(originalTransmutedSkull1.HomingDamage.Level3HomingDaggers.Value).IsEqualTo(0.25f).Within(0.00001f);
			await Assert.That(originalTransmutedSkull1.HomingDamage.Level4HomingDaggers).IsEqualTo(10);
		}

		Enemy? fixedTransmutedSkull1 = Enemies.GetEnemyByName(GameVersion.V3_2, "Transmuted Skull I");
		await Assert.That(fixedTransmutedSkull1).IsNotNull();
		await Assert.That(fixedTransmutedSkull1.HomingDamage.Level3HomingDaggers).IsEqualTo(1);
		await Assert.That(fixedTransmutedSkull1.HomingDamage.Level4HomingDaggers).IsEqualTo(1);
	}
}
