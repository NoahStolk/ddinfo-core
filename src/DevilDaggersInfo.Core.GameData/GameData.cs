using DevilDaggersInfo.Core.GameData.Colors;
using System.Collections.Frozen;

namespace DevilDaggersInfo.Core.GameData;

public sealed class GameData
{
	private GameData()
	{
	}

	public required string Name { get; init; }

	public required DateOnly ReleaseDate { get; init; }

	public required FrozenSet<Enemy> Enemies { get; init; }

	public required FrozenSet<Dagger> Daggers { get; init; }

	public required FrozenSet<Upgrade> Upgrades { get; init; }

	public required FrozenSet<UnlockDagger> UnlockDaggers { get; init; }

	public required FrozenSet<Damage> Damages { get; init; }

	public required FrozenSet<Death> Deaths { get; init; }

	public static GameData V3_2 { get; } = CreateV3_2();

	private static GameData CreateV3_2()
	{
		Death fallen = new(0, "FALLEN", EnemyColors.Void);
		Death swarmed = new(1, "SWARMED", EnemyColors.Skull1);
		Death impaled = new(2, "IMPALED", EnemyColors.Skull2);
		Death gored = new(3, "GORED", EnemyColors.Skull3);
		Death infested = new(4, "INFESTED", EnemyColors.Spiderling);
		Death opened = new(5, "OPENED", EnemyColors.Skull4);
		Death purged = new(6, "PURGED", EnemyColors.Squid1);
		Death desecrated = new(7, "DESECRATED", EnemyColors.Squid2);
		Death sacrificed = new(8, "SACRIFICED", EnemyColors.Squid3);
		Death eviscerated = new(9, "EVISCERATED", EnemyColors.Centipede);
		Death annihilated = new(10, "ANNIHILATED", EnemyColors.Gigapede);
		Death intoxicated = new(11, "INTOXICATED", EnemyColors.SpiderEgg1);
		Death envenomated = new(12, "ENVENOMATED", EnemyColors.SpiderEgg2);
		Death incarnated = new(13, "INCARNATED", EnemyColors.Leviathan);
		Death discarnated = new(14, "DISCARNATED", EnemyColors.TheOrb);
		Death entangled = new(15, "ENTANGLED", EnemyColors.Thorn);
		Death haunted = new(16, "HAUNTED", EnemyColors.Ghostpede);

		Death[] deaths = [fallen, swarmed, impaled, gored, infested, opened, purged, desecrated, sacrificed, eviscerated, annihilated, intoxicated, envenomated, incarnated, discarnated, entangled, haunted];

		Enemy squid1 = new("Squid I", 1, 1, 10, true, 3, EnemyColors.Squid1, purged);
		Enemy squid2 = new("Squid II", 2, 2, 10, true, 39, EnemyColors.Squid2, desecrated);
		Enemy squid3 = new("Squid III", 3, 3, 30, true, 244, EnemyColors.Squid3, sacrificed);
		Enemy centipede = new("Centipede", 25, 25, 3, true, 114, EnemyColors.Centipede, eviscerated);
		Enemy gigapede = new("Gigapede", 50, 50, 5, true, 259, EnemyColors.Gigapede, annihilated);
		Enemy ghostpede = new("Ghostpede", 10, 10, 50, true, 442, EnemyColors.Ghostpede, haunted);
		Enemy leviathan = new("Leviathan", 6, 6, 250, true, 350, EnemyColors.Leviathan, incarnated);
		Enemy spider1 = new("Spider I", 1, 1, 25, true, 39, EnemyColors.Spider1, intoxicated);
		Enemy spider2 = new("Spider II", 1, 1, 200, true, 274, EnemyColors.Spider2, envenomated);
		Enemy thorn = new("Thorn", 0, 1, 120, true, 447, EnemyColors.Thorn, entangled);

		Enemy theOrb = new("The Orb", 0, 1, 2400, false, null, EnemyColors.TheOrb, discarnated);
		Enemy skull1 = new("Skull I", 0, 1, 1, true, null, EnemyColors.Skull1, swarmed);
		Enemy skull2 = new("Skull II", 1, 1, 5, true, null, EnemyColors.Skull2, impaled);
		Enemy skull3 = new("Skull III", 1, 1, 10, true, null, EnemyColors.Skull3, gored);
		Enemy skull4 = new("Skull IV", 0, 1, 100, true, null, EnemyColors.Skull4, opened);
		Enemy transmutedSkull1 = new("Transmuted Skull I", 0, 1, 10, true, null, EnemyColors.TransmutedSkull1, swarmed);
		Enemy transmutedSkull2 = new("Transmuted Skull II", 1, 1, 20, true, null, EnemyColors.TransmutedSkull2, impaled);
		Enemy transmutedSkull3 = new("Transmuted Skull III", 1, 1, 100, true, null, EnemyColors.TransmutedSkull3, gored);
		Enemy transmutedSkull4 = new("Transmuted Skull IV", 0, 1, 300, true, null, EnemyColors.TransmutedSkull4, opened);
		Enemy spiderEgg1 = new("Spider Egg I", 0, 1, 3, false, null, EnemyColors.SpiderEgg1, intoxicated);
		Enemy spiderEgg2 = new("Spider Egg II", 0, 1, 3, false, null, EnemyColors.SpiderEgg2, envenomated);
		Enemy spiderling = new("Spiderling", 0, 1, 3, true, null, EnemyColors.Spiderling, infested);

		skull1.SetTransmuteInto(transmutedSkull1);
		skull2.SetTransmuteInto(transmutedSkull2);
		skull3.SetTransmuteInto(transmutedSkull3);
		skull4.SetTransmuteInto(transmutedSkull4);

		theOrb.SetSpawnedBy(leviathan);
		skull1.SetSpawnedBy(squid1, squid2, squid3);
		skull2.SetSpawnedBy(squid1);
		skull3.SetSpawnedBy(squid2);
		skull4.SetSpawnedBy(squid3);
		spiderEgg1.SetSpawnedBy(spider1);
		spiderEgg2.SetSpawnedBy(spider2);
		spiderling.SetSpawnedBy(spiderEgg1, spiderEgg2);

		Enemy[] enemies = [squid1, squid2, squid3, centipede, gigapede, ghostpede, leviathan, spider1, spider2, thorn, theOrb, skull1, skull2, skull3, skull4, transmutedSkull1, transmutedSkull2, transmutedSkull3, transmutedSkull4, spiderEgg1, spiderEgg2, spiderling];

		Dagger level1 = new("Level 1", DaggerColors.Level1);
		Dagger level2 = new("Level 2", DaggerColors.Level2);
		Dagger level3 = new("Level 3", DaggerColors.Level3);
		Dagger level4 = new("Level 4", DaggerColors.Level4);
		Dagger level3Homing = new("Level 3 Homing", DaggerColors.Level3Homing);
		Dagger level4Homing = new("Level 4 Homing", DaggerColors.Level4Homing);
		Dagger level4Splash = new("Level 4 Splash", DaggerColors.Level4Splash);
		Dagger[] daggers = [level1, level2, level3, level4, level3Homing, level4Homing, level4Splash];

		Upgrade[] upgrades =
		[
			new("Level 1", DaggerColors.Level1, UpgradeUnlockType.Gems, 0, new(10, 20f), null),
			new("Level 2", DaggerColors.Level2, UpgradeUnlockType.Gems, 10, new(20, 40f), null),
			new("Level 3", DaggerColors.Level3, UpgradeUnlockType.Gems, 70, new(40, 80f), new(20, 40f)),
			new("Level 4", DaggerColors.Level4, UpgradeUnlockType.Homing, 150, new(60, 106.666f), new(30, 40f)),
		];

		UnlockDagger[] unlockDaggers =
		[
			new("Default", DaggerColors.Default, 0),
			new("Bronze", DaggerColors.Bronze, 60),
			new("Silver", DaggerColors.Silver, 120),
			new("Golden", DaggerColors.Golden, 250),
			new("Devil", DaggerColors.Devil, 500),
			new("Leviathan", DaggerColors.Leviathan, 1000),
		];

		// All normal daggers deal 1 damage and get destroyed on impact.
		List<Damage> damages = [];
		foreach (Enemy enemy in enemies)
		{
			damages.Add(new(level1, enemy, 1, 1));
			damages.Add(new(level2, enemy, 1, 1));
			damages.Add(new(level3, enemy, 1, 1));
			damages.Add(new(level4, enemy, 1, 1));
		}

		// Level 3 homing daggers deal 10 damage and get destroyed on impact. Exceptions:
		// - They deal 30 damage to Squid 3.
		// - They can take out 4 Skull Is before getting destroyed.
		// - They phase through Ghostpede.
		damages.Add(new(level3Homing, squid1, 1, 10));
		damages.Add(new(level3Homing, squid2, 1, 10));
		damages.Add(new(level3Homing, squid3, 1, 30));
		damages.Add(new(level3Homing, centipede, 1, 10));
		damages.Add(new(level3Homing, gigapede, 1, 10));
		damages.Add(new(level3Homing, leviathan, 1, 1));
		damages.Add(new(level3Homing, theOrb, 1, 1));
		damages.Add(new(level3Homing, spider1, 1, 10));
		damages.Add(new(level3Homing, spider2, 1, 10));
		damages.Add(new(level3Homing, thorn, 1, 10));
		damages.Add(new(level3Homing, skull1, 0.25f, 10));
		damages.Add(new(level3Homing, skull2, 1, 10));
		damages.Add(new(level3Homing, skull3, 1, 10));
		damages.Add(new(level3Homing, skull4, 1, 10));
		damages.Add(new(level3Homing, transmutedSkull1, 1, 10));
		damages.Add(new(level3Homing, transmutedSkull2, 1, 10));
		damages.Add(new(level3Homing, transmutedSkull3, 1, 10));
		damages.Add(new(level3Homing, transmutedSkull4, 1, 10));
		damages.Add(new(level3Homing, spiderEgg1, 1, 10));
		damages.Add(new(level3Homing, spiderEgg2, 1, 10));
		damages.Add(new(level3Homing, spiderling, 1, 10));

		// TODO: Level 4 splash depletion doesn't really make sense. Needs more research. For now the values are all set to 1.
		// TODO: Needs confirmation. Splash definitely damages squids but not sure about normal level 4 homing.
		damages.Add(new(level4Homing, squid1, 1, 0));
		damages.Add(new(level4Homing, squid2, 1, 0));
		damages.Add(new(level4Homing, squid3, 1, 0));
		damages.Add(new(level4Splash, squid1, 1, 10));
		damages.Add(new(level4Splash, squid2, 1, 10));
		damages.Add(new(level4Splash, squid3, 1, 10));

		// Only splash damages pedes (including Ghostpede). Level 4 homing phases through Ghostpede.
		damages.Add(new(level4Homing, centipede, 1, 0));
		damages.Add(new(level4Homing, gigapede, 1, 0));
		damages.Add(new(level4Splash, centipede, 1, 10));
		damages.Add(new(level4Splash, gigapede, 1, 10));
		damages.Add(new(level4Splash, ghostpede, 1, 10));

		// TODO: Needs confirmation. Not sure if splash deals the damage instead.
		damages.Add(new(level4Homing, leviathan, 1, 1));
		damages.Add(new(level4Splash, leviathan, 1, 0));
		damages.Add(new(level4Homing, theOrb, 1, 1));
		damages.Add(new(level4Splash, theOrb, 1, 0));

		// TODO: Needs confirmation. Not sure if splash deals the damage instead.
		damages.Add(new(level4Homing, spider1, 1, 10));
		damages.Add(new(level4Homing, spider2, 1, 10));
		damages.Add(new(level4Splash, spider1, 1, 0));
		damages.Add(new(level4Splash, spider2, 1, 0));

		// TODO: Needs confirmation. Not sure which dagger deals 10 and which deals 1.
		damages.Add(new(level4Homing, thorn, 1, 1));
		damages.Add(new(level4Splash, thorn, 1, 10));

		damages.Add(new(level4Homing, skull1, 0.25f, 10));
		damages.Add(new(level4Homing, skull2, 1, 10));
		damages.Add(new(level4Homing, skull3, 1, 10));
		damages.Add(new(level4Homing, skull4, 1, 10));
		damages.Add(new(level4Splash, skull1, 1, 10));
		damages.Add(new(level4Splash, skull2, 1, 10));
		damages.Add(new(level4Splash, skull3, 1, 10));
		damages.Add(new(level4Splash, skull4, 1, 0)); // TODO: Test.

		damages.Add(new(level4Homing, transmutedSkull1, 1, 10));
		damages.Add(new(level4Homing, transmutedSkull2, 1, 10));
		damages.Add(new(level4Homing, transmutedSkull3, 1, 10));
		damages.Add(new(level4Homing, transmutedSkull4, 1, 10));
		damages.Add(new(level4Splash, transmutedSkull1, 1, 10));
		damages.Add(new(level4Splash, transmutedSkull2, 1, 10));
		damages.Add(new(level4Splash, transmutedSkull3, 1, 10));
		damages.Add(new(level4Splash, transmutedSkull4, 1, 10)); // TODO: Test.

		// TODO: Needs confirmation. Not sure if splash deals the damage instead.
		damages.Add(new(level4Homing, spiderEgg1, 1, 10));
		damages.Add(new(level4Homing, spiderEgg2, 1, 10));
		damages.Add(new(level4Homing, spiderling, 1, 10));
		damages.Add(new(level4Splash, spiderEgg1, 1, 0));
		damages.Add(new(level4Splash, spiderEgg2, 1, 0));
		damages.Add(new(level4Splash, spiderling, 1, 0));

		return new()
		{
			Name = "V3.2",
			ReleaseDate = new(2021, 10, 27),
			Deaths = deaths.ToFrozenSet(),
			Enemies = enemies.ToFrozenSet(),
			Daggers = daggers.ToFrozenSet(),
			Upgrades = upgrades.ToFrozenSet(),
			UnlockDaggers = unlockDaggers.ToFrozenSet(),
			Damages = damages.ToFrozenSet(),
		};
	}
}
