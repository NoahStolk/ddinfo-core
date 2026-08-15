namespace DevilDaggersInfo.Core.Wiki.Test;

internal sealed class UpgradeTests
{
	[Test]
	public async Task TestGetUpgrades()
	{
		await Assert.That(Upgrades.GetUpgrades(GameVersion.V1_0).Count).IsEqualTo(3);
		await Assert.That(Upgrades.GetUpgrades(GameVersion.V2_0).Count).IsEqualTo(4);
		await Assert.That(Upgrades.GetUpgrades(GameVersion.V3_0).Count).IsEqualTo(4);
		await Assert.That(Upgrades.GetUpgrades(GameVersion.V3_1).Count).IsEqualTo(4);
		await Assert.That(Upgrades.GetUpgrades(GameVersion.V3_2).Count).IsEqualTo(4);
	}

	[Test]
	public async Task TestLevels()
	{
		await Assert.That(UpgradesV1_0.Level1.Level).IsEqualTo((byte)1);
		await Assert.That(UpgradesV1_0.Level2.Level).IsEqualTo((byte)2);
		await Assert.That(UpgradesV1_0.Level3.Level).IsEqualTo((byte)3);

		await Assert.That(UpgradesV2_0.Level1.Level).IsEqualTo((byte)1);
		await Assert.That(UpgradesV2_0.Level2.Level).IsEqualTo((byte)2);
		await Assert.That(UpgradesV2_0.Level3.Level).IsEqualTo((byte)3);
		await Assert.That(UpgradesV2_0.Level4.Level).IsEqualTo((byte)4);

		await Assert.That(UpgradesV3_0.Level1.Level).IsEqualTo((byte)1);
		await Assert.That(UpgradesV3_0.Level2.Level).IsEqualTo((byte)2);
		await Assert.That(UpgradesV3_0.Level3.Level).IsEqualTo((byte)3);
		await Assert.That(UpgradesV3_0.Level4.Level).IsEqualTo((byte)4);

		await Assert.That(UpgradesV3_1.Level1.Level).IsEqualTo((byte)1);
		await Assert.That(UpgradesV3_1.Level2.Level).IsEqualTo((byte)2);
		await Assert.That(UpgradesV3_1.Level3.Level).IsEqualTo((byte)3);
		await Assert.That(UpgradesV3_1.Level4.Level).IsEqualTo((byte)4);

		await Assert.That(UpgradesV3_2.Level1.Level).IsEqualTo((byte)1);
		await Assert.That(UpgradesV3_2.Level2.Level).IsEqualTo((byte)2);
		await Assert.That(UpgradesV3_2.Level3.Level).IsEqualTo((byte)3);
		await Assert.That(UpgradesV3_2.Level4.Level).IsEqualTo((byte)4);
	}

	[Test]
	public async Task TestDefaultDamage()
	{
		await Assert.That(UpgradesV1_0.Level1.DefaultDamage).IsEqualTo(new(10, 20f));
		await Assert.That(UpgradesV2_0.Level1.DefaultDamage).IsEqualTo(new(10, 20f));
		await Assert.That(UpgradesV3_0.Level1.DefaultDamage).IsEqualTo(new(10, 20f));
		await Assert.That(UpgradesV3_1.Level1.DefaultDamage).IsEqualTo(new(10, 20f));
		await Assert.That(UpgradesV3_2.Level1.DefaultDamage).IsEqualTo(new(10, 20f));

		await Assert.That(UpgradesV1_0.Level2.DefaultDamage).IsEqualTo(new(20, 40f));
		await Assert.That(UpgradesV2_0.Level2.DefaultDamage).IsEqualTo(new(20, 40f));
		await Assert.That(UpgradesV3_0.Level2.DefaultDamage).IsEqualTo(new(20, 40f));
		await Assert.That(UpgradesV3_1.Level2.DefaultDamage).IsEqualTo(new(20, 40f));
		await Assert.That(UpgradesV3_2.Level2.DefaultDamage).IsEqualTo(new(20, 40f));

		await Assert.That(UpgradesV1_0.Level3.DefaultDamage).IsEqualTo(new(40, 80f));
		await Assert.That(UpgradesV2_0.Level3.DefaultDamage).IsEqualTo(new(40, 80f));
		await Assert.That(UpgradesV3_0.Level3.DefaultDamage).IsEqualTo(new(40, 80f));
		await Assert.That(UpgradesV3_1.Level3.DefaultDamage).IsEqualTo(new(40, 80f));
		await Assert.That(UpgradesV3_2.Level3.DefaultDamage).IsEqualTo(new(40, 80f));

		await Assert.That(UpgradesV2_0.Level4.DefaultDamage).IsEqualTo(new(60, 106.666f));
		await Assert.That(UpgradesV3_0.Level4.DefaultDamage).IsEqualTo(new(60, 106.666f));
		await Assert.That(UpgradesV3_1.Level4.DefaultDamage).IsEqualTo(new(60, 106.666f));
		await Assert.That(UpgradesV3_2.Level4.DefaultDamage).IsEqualTo(new(60, 106.666f));
	}

	[Test]
	public async Task TestHomingDamage()
	{
		await Assert.That(UpgradesV1_0.Level1.HomingDamage).IsNull();
		await Assert.That(UpgradesV2_0.Level1.HomingDamage).IsNull();
		await Assert.That(UpgradesV3_0.Level1.HomingDamage).IsNull();
		await Assert.That(UpgradesV3_1.Level1.HomingDamage).IsNull();
		await Assert.That(UpgradesV3_2.Level1.HomingDamage).IsNull();

		await Assert.That(UpgradesV1_0.Level2.HomingDamage).IsNull();
		await Assert.That(UpgradesV2_0.Level2.HomingDamage).IsNull();
		await Assert.That(UpgradesV3_0.Level2.HomingDamage).IsNull();
		await Assert.That(UpgradesV3_1.Level2.HomingDamage).IsNull();
		await Assert.That(UpgradesV3_2.Level2.HomingDamage).IsNull();

		await Assert.That(UpgradesV1_0.Level3.HomingDamage).IsEqualTo(new(40, 40f));
		await Assert.That(UpgradesV2_0.Level3.HomingDamage).IsEqualTo(new(20, 40f));
		await Assert.That(UpgradesV3_0.Level3.HomingDamage).IsEqualTo(new(20, 40f));
		await Assert.That(UpgradesV3_1.Level3.HomingDamage).IsEqualTo(new(20, 40f));
		await Assert.That(UpgradesV3_2.Level3.HomingDamage).IsEqualTo(new(20, 40f));

		await Assert.That(UpgradesV2_0.Level4.HomingDamage).IsEqualTo(new(30, 40f));
		await Assert.That(UpgradesV3_0.Level4.HomingDamage).IsEqualTo(new(30, 40f));
		await Assert.That(UpgradesV3_1.Level4.HomingDamage).IsEqualTo(new(30, 40f));
		await Assert.That(UpgradesV3_2.Level4.HomingDamage).IsEqualTo(new(30, 40f));
	}

	[Test]
	public async Task TestUpgradeUnlock()
	{
		await Assert.That(UpgradesV1_0.Level1.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 0));
		await Assert.That(UpgradesV2_0.Level1.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 0));
		await Assert.That(UpgradesV3_0.Level1.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 0));
		await Assert.That(UpgradesV3_1.Level1.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 0));
		await Assert.That(UpgradesV3_2.Level1.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 0));

		await Assert.That(UpgradesV1_0.Level2.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 10));
		await Assert.That(UpgradesV2_0.Level2.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 10));
		await Assert.That(UpgradesV3_0.Level2.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 10));
		await Assert.That(UpgradesV3_1.Level2.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 10));
		await Assert.That(UpgradesV3_2.Level2.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 10));

		await Assert.That(UpgradesV1_0.Level3.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 70));
		await Assert.That(UpgradesV2_0.Level3.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 70));
		await Assert.That(UpgradesV3_0.Level3.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 70));
		await Assert.That(UpgradesV3_1.Level3.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 70));
		await Assert.That(UpgradesV3_2.Level3.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Gems, 70));

		await Assert.That(UpgradesV2_0.Level4.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Homing, 150));
		await Assert.That(UpgradesV3_0.Level4.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Homing, 150));
		await Assert.That(UpgradesV3_1.Level4.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Homing, 150));
		await Assert.That(UpgradesV3_2.Level4.UpgradeUnlock).IsEqualTo(new(UpgradeUnlockType.Homing, 150));
	}
}
