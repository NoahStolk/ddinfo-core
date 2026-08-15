namespace DevilDaggersInfo.Core.Wiki.Test;

internal sealed class DeathTests
{
	[Test]
	public async Task TestFallen()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 0)).IsEqualTo(DeathsV1_0.Fallen);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 0)).IsEqualTo(DeathsV2_0.Fallen);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 0)).IsEqualTo(DeathsV3_0.Fallen);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 0)).IsEqualTo(DeathsV3_1.Fallen);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 0)).IsEqualTo(DeathsV3_2.Fallen);

		await Assert.That(EnemyColors.Void).IsEqualTo(DeathsV1_0.Fallen.Color);
		await Assert.That(EnemyColors.Void).IsEqualTo(DeathsV2_0.Fallen.Color);
		await Assert.That(EnemyColors.Void).IsEqualTo(DeathsV3_0.Fallen.Color);
		await Assert.That(EnemyColors.Void).IsEqualTo(DeathsV3_1.Fallen.Color);
		await Assert.That(EnemyColors.Void).IsEqualTo(DeathsV3_2.Fallen.Color);
	}

	[Test]
	public async Task TestSwarmed()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 1)).IsEqualTo(DeathsV1_0.Swarmed);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 1)).IsEqualTo(DeathsV2_0.Swarmed);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 1)).IsEqualTo(DeathsV3_0.Swarmed);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 1)).IsEqualTo(DeathsV3_1.Swarmed);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 1)).IsEqualTo(DeathsV3_2.Swarmed);
		await Assert.That(EnemiesV1_0.Skull1.Death).IsEqualTo(DeathsV1_0.Swarmed);
		await Assert.That(EnemiesV2_0.Skull1.Death).IsEqualTo(DeathsV2_0.Swarmed);
		await Assert.That(EnemiesV3_0.Skull1.Death).IsEqualTo(DeathsV3_0.Swarmed);
		await Assert.That(EnemiesV3_1.Skull1.Death).IsEqualTo(DeathsV3_1.Swarmed);
		await Assert.That(EnemiesV3_2.Skull1.Death).IsEqualTo(DeathsV3_2.Swarmed);
		await Assert.That(EnemiesV2_0.TransmutedSkull1.Death).IsEqualTo(DeathsV2_0.Swarmed);
		await Assert.That(EnemiesV3_0.TransmutedSkull1.Death).IsEqualTo(DeathsV3_0.Swarmed);
		await Assert.That(EnemiesV3_1.TransmutedSkull1.Death).IsEqualTo(DeathsV3_1.Swarmed);
		await Assert.That(EnemiesV3_2.TransmutedSkull1.Death).IsEqualTo(DeathsV3_2.Swarmed);

		await Assert.That(EnemyColors.Skull1).IsEqualTo(DeathsV1_0.Swarmed.Color);
		await Assert.That(EnemyColors.Skull1).IsEqualTo(DeathsV2_0.Swarmed.Color);
		await Assert.That(EnemyColors.Skull1).IsEqualTo(DeathsV3_0.Swarmed.Color);
		await Assert.That(EnemyColors.Skull1).IsEqualTo(DeathsV3_1.Swarmed.Color);
		await Assert.That(EnemyColors.Skull1).IsEqualTo(DeathsV3_2.Swarmed.Color);
	}

	[Test]
	public async Task TestImpaled()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 2)).IsEqualTo(DeathsV1_0.Impaled);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 2)).IsEqualTo(DeathsV2_0.Impaled);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 2)).IsEqualTo(DeathsV3_0.Impaled);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 2)).IsEqualTo(DeathsV3_1.Impaled);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 2)).IsEqualTo(DeathsV3_2.Impaled);
		await Assert.That(EnemiesV1_0.Skull2.Death).IsEqualTo(DeathsV1_0.Impaled);
		await Assert.That(EnemiesV2_0.Skull2.Death).IsEqualTo(DeathsV2_0.Impaled);
		await Assert.That(EnemiesV3_0.Skull2.Death).IsEqualTo(DeathsV3_0.Impaled);
		await Assert.That(EnemiesV3_1.Skull2.Death).IsEqualTo(DeathsV3_1.Impaled);
		await Assert.That(EnemiesV3_2.Skull2.Death).IsEqualTo(DeathsV3_2.Impaled);
		await Assert.That(EnemiesV1_0.TransmutedSkull2.Death).IsEqualTo(DeathsV1_0.Impaled);
		await Assert.That(EnemiesV2_0.TransmutedSkull2.Death).IsEqualTo(DeathsV2_0.Impaled);
		await Assert.That(EnemiesV3_0.TransmutedSkull2.Death).IsEqualTo(DeathsV3_0.Impaled);
		await Assert.That(EnemiesV3_1.TransmutedSkull2.Death).IsEqualTo(DeathsV3_1.Impaled);
		await Assert.That(EnemiesV3_2.TransmutedSkull2.Death).IsEqualTo(DeathsV3_2.Impaled);

		await Assert.That(EnemyColors.Skull2).IsEqualTo(DeathsV1_0.Impaled.Color);
		await Assert.That(EnemyColors.Skull2).IsEqualTo(DeathsV2_0.Impaled.Color);
		await Assert.That(EnemyColors.Skull2).IsEqualTo(DeathsV3_0.Impaled.Color);
		await Assert.That(EnemyColors.Skull2).IsEqualTo(DeathsV3_1.Impaled.Color);
		await Assert.That(EnemyColors.Skull2).IsEqualTo(DeathsV3_2.Impaled.Color);
	}

	[Test]
	public async Task TestGored()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 3)).IsEqualTo(DeathsV2_0.Gored);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 3)).IsEqualTo(DeathsV3_0.Gored);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 3)).IsEqualTo(DeathsV3_1.Gored);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 3)).IsEqualTo(DeathsV3_2.Gored);
		await Assert.That(EnemiesV2_0.Skull3.Death).IsEqualTo(DeathsV2_0.Gored);
		await Assert.That(EnemiesV3_0.Skull3.Death).IsEqualTo(DeathsV3_0.Gored);
		await Assert.That(EnemiesV3_1.Skull3.Death).IsEqualTo(DeathsV3_1.Gored);
		await Assert.That(EnemiesV3_2.Skull3.Death).IsEqualTo(DeathsV3_2.Gored);
		await Assert.That(EnemiesV2_0.TransmutedSkull3.Death).IsEqualTo(DeathsV2_0.Gored);
		await Assert.That(EnemiesV3_0.TransmutedSkull3.Death).IsEqualTo(DeathsV3_0.Gored);
		await Assert.That(EnemiesV3_1.TransmutedSkull3.Death).IsEqualTo(DeathsV3_1.Gored);
		await Assert.That(EnemiesV3_2.TransmutedSkull3.Death).IsEqualTo(DeathsV3_2.Gored);

		await Assert.That(EnemyColors.Skull3).IsEqualTo(DeathsV2_0.Gored.Color);
		await Assert.That(EnemyColors.Skull3).IsEqualTo(DeathsV3_0.Gored.Color);
		await Assert.That(EnemyColors.Skull3).IsEqualTo(DeathsV3_1.Gored.Color);
		await Assert.That(EnemyColors.Skull3).IsEqualTo(DeathsV3_2.Gored.Color);
	}

	[Test]
	public async Task TestInfested()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 4)).IsEqualTo(DeathsV1_0.Infested);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 4)).IsEqualTo(DeathsV2_0.Infested);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 4)).IsEqualTo(DeathsV3_0.Infested);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 4)).IsEqualTo(DeathsV3_1.Infested);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 4)).IsEqualTo(DeathsV3_2.Infested);
		await Assert.That(EnemiesV1_0.SpiderEgg1.Death).IsEqualTo(DeathsV1_0.Infested);
		await Assert.That(EnemiesV2_0.SpiderEgg1.Death).IsEqualTo(DeathsV2_0.Infested);
		await Assert.That(EnemiesV3_0.Spiderling.Death).IsEqualTo(DeathsV3_0.Infested);
		await Assert.That(EnemiesV3_1.Spiderling.Death).IsEqualTo(DeathsV3_1.Infested);
		await Assert.That(EnemiesV3_2.Spiderling.Death).IsEqualTo(DeathsV3_2.Infested);

		await Assert.That(EnemyColors.SpiderEgg1).IsEqualTo(DeathsV1_0.Infested.Color);
		await Assert.That(EnemyColors.SpiderEgg1).IsEqualTo(DeathsV2_0.Infested.Color);
		await Assert.That(EnemyColors.Spiderling).IsEqualTo(DeathsV3_0.Infested.Color);
		await Assert.That(EnemyColors.Spiderling).IsEqualTo(DeathsV3_1.Infested.Color);
		await Assert.That(EnemyColors.Spiderling).IsEqualTo(DeathsV3_2.Infested.Color);
	}

	[Test]
	public async Task TestOpened()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 5)).IsEqualTo(DeathsV2_0.Opened);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 5)).IsEqualTo(DeathsV3_0.Opened);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 5)).IsEqualTo(DeathsV3_1.Opened);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 5)).IsEqualTo(DeathsV3_2.Opened);
		await Assert.That(EnemiesV2_0.Skull4.Death).IsEqualTo(DeathsV2_0.Opened);
		await Assert.That(EnemiesV3_0.Skull4.Death).IsEqualTo(DeathsV3_0.Opened);
		await Assert.That(EnemiesV3_1.Skull4.Death).IsEqualTo(DeathsV3_1.Opened);
		await Assert.That(EnemiesV3_2.Skull4.Death).IsEqualTo(DeathsV3_2.Opened);
		await Assert.That(EnemiesV2_0.TransmutedSkull4.Death).IsEqualTo(DeathsV2_0.Opened);
		await Assert.That(EnemiesV3_0.TransmutedSkull4.Death).IsEqualTo(DeathsV3_0.Opened);
		await Assert.That(EnemiesV3_1.TransmutedSkull4.Death).IsEqualTo(DeathsV3_1.Opened);
		await Assert.That(EnemiesV3_2.TransmutedSkull4.Death).IsEqualTo(DeathsV3_2.Opened);

		await Assert.That(EnemyColors.Skull4).IsEqualTo(DeathsV2_0.Opened.Color);
		await Assert.That(EnemyColors.Skull4).IsEqualTo(DeathsV3_0.Opened.Color);
		await Assert.That(EnemyColors.Skull4).IsEqualTo(DeathsV3_1.Opened.Color);
		await Assert.That(EnemyColors.Skull4).IsEqualTo(DeathsV3_2.Opened.Color);
	}

	[Test]
	public async Task TestPurged()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 6)).IsEqualTo(DeathsV1_0.Purged);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 6)).IsEqualTo(DeathsV2_0.Purged);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 6)).IsEqualTo(DeathsV3_0.Purged);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 6)).IsEqualTo(DeathsV3_1.Purged);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 6)).IsEqualTo(DeathsV3_2.Purged);
		await Assert.That(EnemiesV1_0.Squid1.Death).IsEqualTo(DeathsV1_0.Purged);
		await Assert.That(EnemiesV2_0.Squid1.Death).IsEqualTo(DeathsV2_0.Purged);
		await Assert.That(EnemiesV3_0.Squid1.Death).IsEqualTo(DeathsV3_0.Purged);
		await Assert.That(EnemiesV3_1.Squid1.Death).IsEqualTo(DeathsV3_1.Purged);
		await Assert.That(EnemiesV3_2.Squid1.Death).IsEqualTo(DeathsV3_2.Purged);

		await Assert.That(EnemyColors.Squid1).IsEqualTo(DeathsV1_0.Purged.Color);
		await Assert.That(EnemyColors.Squid1).IsEqualTo(DeathsV2_0.Purged.Color);
		await Assert.That(EnemyColors.Squid1).IsEqualTo(DeathsV3_0.Purged.Color);
		await Assert.That(EnemyColors.Squid1).IsEqualTo(DeathsV3_1.Purged.Color);
		await Assert.That(EnemyColors.Squid1).IsEqualTo(DeathsV3_2.Purged.Color);
	}

	[Test]
	public async Task TestDesecrated()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 7)).IsEqualTo(DeathsV2_0.Desecrated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 7)).IsEqualTo(DeathsV3_0.Desecrated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 7)).IsEqualTo(DeathsV3_1.Desecrated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 7)).IsEqualTo(DeathsV3_2.Desecrated);
		await Assert.That(EnemiesV2_0.Squid2.Death).IsEqualTo(DeathsV2_0.Desecrated);
		await Assert.That(EnemiesV3_0.Squid2.Death).IsEqualTo(DeathsV3_0.Desecrated);
		await Assert.That(EnemiesV3_1.Squid2.Death).IsEqualTo(DeathsV3_1.Desecrated);
		await Assert.That(EnemiesV3_2.Squid2.Death).IsEqualTo(DeathsV3_2.Desecrated);

		await Assert.That(EnemyColors.Squid2).IsEqualTo(DeathsV2_0.Desecrated.Color);
		await Assert.That(EnemyColors.Squid2).IsEqualTo(DeathsV3_0.Desecrated.Color);
		await Assert.That(EnemyColors.Squid2).IsEqualTo(DeathsV3_1.Desecrated.Color);
		await Assert.That(EnemyColors.Squid2).IsEqualTo(DeathsV3_2.Desecrated.Color);
	}

	[Test]
	public async Task TestSacrificed()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 8)).IsEqualTo(DeathsV1_0.Sacrificed);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 8)).IsEqualTo(DeathsV2_0.Sacrificed);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 8)).IsEqualTo(DeathsV3_0.Sacrificed);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 8)).IsEqualTo(DeathsV3_1.Sacrificed);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 8)).IsEqualTo(DeathsV3_2.Sacrificed);
		await Assert.That(EnemiesV1_0.Squid2.Death).IsEqualTo(DeathsV1_0.Sacrificed);
		await Assert.That(EnemiesV2_0.Squid3.Death).IsEqualTo(DeathsV2_0.Sacrificed);
		await Assert.That(EnemiesV3_0.Squid3.Death).IsEqualTo(DeathsV3_0.Sacrificed);
		await Assert.That(EnemiesV3_1.Squid3.Death).IsEqualTo(DeathsV3_1.Sacrificed);
		await Assert.That(EnemiesV3_2.Squid3.Death).IsEqualTo(DeathsV3_2.Sacrificed);

		await Assert.That(EnemyColors.Squid2).IsEqualTo(DeathsV1_0.Sacrificed.Color);
		await Assert.That(EnemyColors.Squid3).IsEqualTo(DeathsV2_0.Sacrificed.Color);
		await Assert.That(EnemyColors.Squid3).IsEqualTo(DeathsV3_0.Sacrificed.Color);
		await Assert.That(EnemyColors.Squid3).IsEqualTo(DeathsV3_1.Sacrificed.Color);
		await Assert.That(EnemyColors.Squid3).IsEqualTo(DeathsV3_2.Sacrificed.Color);
	}

	[Test]
	public async Task TestEviscerated()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 9)).IsEqualTo(DeathsV1_0.Eviscerated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 9)).IsEqualTo(DeathsV2_0.Eviscerated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 9)).IsEqualTo(DeathsV3_0.Eviscerated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 9)).IsEqualTo(DeathsV3_1.Eviscerated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 9)).IsEqualTo(DeathsV3_2.Eviscerated);
		await Assert.That(EnemiesV1_0.Gigapede.Death).IsEqualTo(DeathsV1_0.Eviscerated);
		await Assert.That(EnemiesV1_0.Centipede.Death).IsEqualTo(DeathsV1_0.Eviscerated);
		await Assert.That(EnemiesV2_0.Centipede.Death).IsEqualTo(DeathsV2_0.Eviscerated);
		await Assert.That(EnemiesV3_0.Centipede.Death).IsEqualTo(DeathsV3_0.Eviscerated);
		await Assert.That(EnemiesV3_1.Centipede.Death).IsEqualTo(DeathsV3_1.Eviscerated);
		await Assert.That(EnemiesV3_2.Centipede.Death).IsEqualTo(DeathsV3_2.Eviscerated);

		await Assert.That(EnemyColors.Centipede).IsEqualTo(DeathsV1_0.Eviscerated.Color);
		await Assert.That(EnemyColors.Centipede).IsEqualTo(DeathsV2_0.Eviscerated.Color);
		await Assert.That(EnemyColors.Centipede).IsEqualTo(DeathsV3_0.Eviscerated.Color);
		await Assert.That(EnemyColors.Centipede).IsEqualTo(DeathsV3_1.Eviscerated.Color);
		await Assert.That(EnemyColors.Centipede).IsEqualTo(DeathsV3_2.Eviscerated.Color);
	}

	[Test]
	public async Task TestAnnihilated()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 10)).IsEqualTo(DeathsV1_0.Annihilated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 10)).IsEqualTo(DeathsV2_0.Annihilated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 10)).IsEqualTo(DeathsV3_0.Annihilated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 10)).IsEqualTo(DeathsV3_1.Annihilated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 10)).IsEqualTo(DeathsV3_2.Annihilated);
		await Assert.That(EnemiesV1_0.TransmutedSkull4.Death).IsEqualTo(DeathsV1_0.Annihilated);
		await Assert.That(EnemiesV2_0.Gigapede.Death).IsEqualTo(DeathsV2_0.Annihilated);
		await Assert.That(EnemiesV3_0.Gigapede.Death).IsEqualTo(DeathsV3_0.Annihilated);
		await Assert.That(EnemiesV3_1.Gigapede.Death).IsEqualTo(DeathsV3_1.Annihilated);
		await Assert.That(EnemiesV3_2.Gigapede.Death).IsEqualTo(DeathsV3_2.Annihilated);

		await Assert.That(EnemyColors.TransmutedSkull4).IsEqualTo(DeathsV1_0.Annihilated.Color);
		await Assert.That(EnemyColors.GigapedeRed).IsEqualTo(DeathsV2_0.Annihilated.Color);
		await Assert.That(EnemyColors.Gigapede).IsEqualTo(DeathsV3_0.Annihilated.Color);
		await Assert.That(EnemyColors.Gigapede).IsEqualTo(DeathsV3_1.Annihilated.Color);
		await Assert.That(EnemyColors.Gigapede).IsEqualTo(DeathsV3_2.Annihilated.Color);
	}

	[Test]
	public async Task TestIntoxicated()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 11)).IsEqualTo(DeathsV3_0.Intoxicated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 11)).IsEqualTo(DeathsV3_1.Intoxicated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 11)).IsEqualTo(DeathsV3_2.Intoxicated);
		await Assert.That(EnemiesV3_0.SpiderEgg1.Death).IsEqualTo(DeathsV3_0.Intoxicated);
		await Assert.That(EnemiesV3_0.Spider1.Death).IsEqualTo(DeathsV3_0.Intoxicated);
		await Assert.That(EnemiesV3_0.Ghostpede.Death).IsEqualTo(DeathsV3_0.Intoxicated);
		await Assert.That(EnemiesV3_1.SpiderEgg1.Death).IsEqualTo(DeathsV3_1.Intoxicated);
		await Assert.That(EnemiesV3_1.Spider1.Death).IsEqualTo(DeathsV3_1.Intoxicated);
		await Assert.That(EnemiesV3_2.SpiderEgg1.Death).IsEqualTo(DeathsV3_2.Intoxicated);
		await Assert.That(EnemiesV3_2.Spider1.Death).IsEqualTo(DeathsV3_2.Intoxicated);

		await Assert.That(EnemyColors.SpiderEgg1).IsEqualTo(DeathsV3_0.Intoxicated.Color);
		await Assert.That(EnemyColors.SpiderEgg1).IsEqualTo(DeathsV3_1.Intoxicated.Color);
		await Assert.That(EnemyColors.SpiderEgg1).IsEqualTo(DeathsV3_2.Intoxicated.Color);
	}

	[Test]
	public async Task TestEnvenomated()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 12)).IsEqualTo(DeathsV2_0.Envenomated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 12)).IsEqualTo(DeathsV3_0.Envenomated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 12)).IsEqualTo(DeathsV3_1.Envenomated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 12)).IsEqualTo(DeathsV3_2.Envenomated);
		await Assert.That(EnemiesV2_0.SpiderEgg2.Death).IsEqualTo(DeathsV2_0.Envenomated);
		await Assert.That(EnemiesV2_0.Spider2.Death).IsEqualTo(DeathsV2_0.Envenomated);
		await Assert.That(EnemiesV3_0.SpiderEgg2.Death).IsEqualTo(DeathsV3_0.Envenomated);
		await Assert.That(EnemiesV3_0.Spider2.Death).IsEqualTo(DeathsV3_0.Envenomated);
		await Assert.That(EnemiesV3_1.SpiderEgg2.Death).IsEqualTo(DeathsV3_1.Envenomated);
		await Assert.That(EnemiesV3_1.Spider2.Death).IsEqualTo(DeathsV3_1.Envenomated);
		await Assert.That(EnemiesV3_2.SpiderEgg2.Death).IsEqualTo(DeathsV3_2.Envenomated);
		await Assert.That(EnemiesV3_2.Spider2.Death).IsEqualTo(DeathsV3_2.Envenomated);

		await Assert.That(EnemyColors.SpiderEgg2).IsEqualTo(DeathsV2_0.Envenomated.Color);
		await Assert.That(EnemyColors.SpiderEgg2).IsEqualTo(DeathsV3_0.Envenomated.Color);
		await Assert.That(EnemyColors.SpiderEgg2).IsEqualTo(DeathsV3_1.Envenomated.Color);
		await Assert.That(EnemyColors.SpiderEgg2).IsEqualTo(DeathsV3_2.Envenomated.Color);
	}

	[Test]
	public async Task TestIncarnated()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 13)).IsEqualTo(DeathsV3_0.Incarnated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 13)).IsEqualTo(DeathsV3_1.Incarnated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 13)).IsEqualTo(DeathsV3_2.Incarnated);
		await Assert.That(EnemiesV3_0.Leviathan.Death).IsEqualTo(DeathsV3_0.Incarnated);
		await Assert.That(EnemiesV3_1.Leviathan.Death).IsEqualTo(DeathsV3_1.Incarnated);
		await Assert.That(EnemiesV3_2.Leviathan.Death).IsEqualTo(DeathsV3_2.Incarnated);

		await Assert.That(EnemyColors.Leviathan).IsEqualTo(DeathsV3_0.Incarnated.Color);
		await Assert.That(EnemyColors.Leviathan).IsEqualTo(DeathsV3_1.Incarnated.Color);
		await Assert.That(EnemyColors.Leviathan).IsEqualTo(DeathsV3_2.Incarnated.Color);
	}

	[Test]
	public async Task TestDiscarnated()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 14)).IsEqualTo(DeathsV3_0.Discarnated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 14)).IsEqualTo(DeathsV3_1.Discarnated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 14)).IsEqualTo(DeathsV3_2.Discarnated);
		await Assert.That(EnemiesV3_0.TheOrb.Death).IsEqualTo(DeathsV3_0.Discarnated);
		await Assert.That(EnemiesV3_1.TheOrb.Death).IsEqualTo(DeathsV3_1.Discarnated);
		await Assert.That(EnemiesV3_2.TheOrb.Death).IsEqualTo(DeathsV3_2.Discarnated);

		await Assert.That(EnemyColors.TheOrb).IsEqualTo(DeathsV3_0.Discarnated.Color);
		await Assert.That(EnemyColors.TheOrb).IsEqualTo(DeathsV3_1.Discarnated.Color);
		await Assert.That(EnemyColors.TheOrb).IsEqualTo(DeathsV3_2.Discarnated.Color);
	}

	[Test]
	public async Task TestBarbed()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 15)).IsEqualTo(DeathsV3_0.Barbed);
		await Assert.That(EnemiesV3_0.Thorn.Death).IsEqualTo(DeathsV3_0.Barbed);

		await Assert.That(EnemyColors.Thorn).IsEqualTo(DeathsV3_0.Barbed.Color);
	}

	[Test]
	public async Task TestEntangled()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 15)).IsEqualTo(DeathsV3_1.Entangled);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 15)).IsEqualTo(DeathsV3_2.Entangled);
		await Assert.That(EnemiesV3_1.Thorn.Death).IsEqualTo(DeathsV3_1.Entangled);
		await Assert.That(EnemiesV3_2.Thorn.Death).IsEqualTo(DeathsV3_2.Entangled);

		await Assert.That(EnemyColors.Thorn).IsEqualTo(DeathsV3_1.Entangled.Color);
		await Assert.That(EnemyColors.Thorn).IsEqualTo(DeathsV3_2.Entangled.Color);
	}

	[Test]
	public async Task TestHaunted()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 16)).IsEqualTo(DeathsV3_1.Haunted);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 16)).IsEqualTo(DeathsV3_2.Haunted);
		await Assert.That(EnemiesV3_1.Ghostpede.Death).IsEqualTo(DeathsV3_1.Haunted);
		await Assert.That(EnemiesV3_2.Ghostpede.Death).IsEqualTo(DeathsV3_2.Haunted);

		await Assert.That(EnemyColors.Ghostpede).IsEqualTo(DeathsV3_1.Haunted.Color);
		await Assert.That(EnemyColors.Ghostpede).IsEqualTo(DeathsV3_2.Haunted.Color);
	}

	[Test]
	public async Task TestStricken()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 16)).IsEqualTo(DeathsV1_0.Stricken);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 16)).IsEqualTo(DeathsV2_0.Stricken);
		await Assert.That(EnemiesV1_0.Spiderling.Death).IsEqualTo(DeathsV1_0.Stricken);
		await Assert.That(EnemiesV2_0.Spiderling.Death).IsEqualTo(DeathsV2_0.Stricken);

		await Assert.That(EnemyColors.Spiderling).IsEqualTo(DeathsV1_0.Stricken.Color);
		await Assert.That(EnemyColors.Spiderling).IsEqualTo(DeathsV2_0.Stricken.Color);
	}

	[Test]
	public async Task TestDevastated()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 17)).IsEqualTo(DeathsV1_0.Devastated);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 17)).IsEqualTo(DeathsV2_0.Devastated);
		await Assert.That(EnemiesV1_0.Leviathan.Death).IsEqualTo(DeathsV1_0.Devastated);
		await Assert.That(EnemiesV2_0.Leviathan.Death).IsEqualTo(DeathsV2_0.Devastated);

		await Assert.That(EnemyColors.Leviathan).IsEqualTo(DeathsV1_0.Devastated.Color);
		await Assert.That(EnemyColors.Leviathan).IsEqualTo(DeathsV2_0.Devastated.Color);
	}

	[Test]
	public async Task TestDismembered()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 18)).IsEqualTo(DeathsV1_0.Dismembered);
		await Assert.That(EnemiesV1_0.Skull3.Death).IsEqualTo(DeathsV1_0.Dismembered);
		await Assert.That(EnemiesV1_0.TransmutedSkull3.Death).IsEqualTo(DeathsV1_0.Dismembered);

		await Assert.That(EnemyColors.Skull3).IsEqualTo(DeathsV1_0.Dismembered.Color);
	}

	[Test]
	public async Task TestUnknown()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V1_0, 255)).IsEqualTo(DeathsV1_0.Unknown);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 255)).IsEqualTo(DeathsV2_0.Unknown);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_0, 255)).IsEqualTo(DeathsV3_0.Unknown);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_1, 255)).IsEqualTo(DeathsV3_1.Unknown);
		await Assert.That(Deaths.GetDeathByType(GameVersion.V3_2, 255)).IsEqualTo(DeathsV3_2.Unknown);

		await Assert.That(EnemyColors.Unknown).IsEqualTo(DeathsV1_0.Unknown.Color);
		await Assert.That(EnemyColors.Unknown).IsEqualTo(DeathsV2_0.Unknown.Color);
		await Assert.That(EnemyColors.Unknown).IsEqualTo(DeathsV3_0.Unknown.Color);
		await Assert.That(EnemyColors.Unknown).IsEqualTo(DeathsV3_1.Unknown.Color);
		await Assert.That(EnemyColors.Unknown).IsEqualTo(DeathsV3_2.Unknown.Color);
	}

	[Test]
	public async Task TestNone()
	{
		await Assert.That(Deaths.GetDeathByType(GameVersion.V2_0, 200)).IsEqualTo(DeathsV2_0.None);

		await Assert.That(EnemyColors.Andras).IsEqualTo(DeathsV2_0.None.Color);
	}
}
