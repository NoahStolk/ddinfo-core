using DevilDaggersInfo.Core.GameData.Colors;

namespace DevilDaggersInfo.Core.GameData;

public sealed class V3_2 : IGameData
{
	private V3_2()
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
		Death discarnated = new(14, "DISCARNATED", EnemyColors.Orb);
		Death entangled = new(15, "ENTANGLED", EnemyColors.Thorn);
		Death haunted = new(16, "HAUNTED", EnemyColors.Ghostpede);

		Death[] deaths = [fallen, swarmed, impaled, gored, infested, opened, purged, desecrated, sacrificed, eviscerated, annihilated, intoxicated, envenomated, incarnated, discarnated, entangled, haunted];

		Squid1 = new("Squid I", 1, 1, 10, true, 3, EnemyColors.Squid1, purged);
		Squid2 = new("Squid II", 2, 2, 10, true, 39, EnemyColors.Squid2, desecrated);
		Squid3 = new("Squid III", 3, 3, 30, true, 244, EnemyColors.Squid3, sacrificed);
		Centipede = new("Centipede", 25, 25, 3, true, 114, EnemyColors.Centipede, eviscerated);
		Gigapede = new("Gigapede", 50, 50, 5, true, 259, EnemyColors.Gigapede, annihilated);
		Ghostpede = new("Ghostpede", 10, 10, 50, true, 442, EnemyColors.Ghostpede, haunted);
		Leviathan = new("Leviathan", 6, 6, 250, true, 350, EnemyColors.Leviathan, incarnated);
		Spider1 = new("Spider I", 1, 1, 25, true, 39, EnemyColors.Spider1, intoxicated);
		Spider2 = new("Spider II", 1, 1, 200, true, 274, EnemyColors.Spider2, envenomated);
		Thorn = new("Thorn", 0, 1, 120, true, 447, EnemyColors.Thorn, entangled);

		Orb = new("Orb", 0, 1, 2400, false, null, EnemyColors.Orb, discarnated);
		Skull1 = new("Skull I", 0, 1, 1, true, null, EnemyColors.Skull1, swarmed);
		Skull2 = new("Skull II", 1, 1, 5, true, null, EnemyColors.Skull2, impaled);
		Skull3 = new("Skull III", 1, 1, 10, true, null, EnemyColors.Skull3, gored);
		Skull4 = new("Skull IV", 0, 1, 100, true, null, EnemyColors.Skull4, opened);
		TransmutedSkull1 = new("Transmuted Skull I", 0, 1, 10, true, null, EnemyColors.TransmutedSkull1, swarmed);
		TransmutedSkull2 = new("Transmuted Skull II", 1, 1, 20, true, null, EnemyColors.TransmutedSkull2, impaled);
		TransmutedSkull3 = new("Transmuted Skull III", 1, 1, 100, true, null, EnemyColors.TransmutedSkull3, gored);
		TransmutedSkull4 = new("Transmuted Skull IV", 0, 1, 300, true, null, EnemyColors.TransmutedSkull4, opened);
		SpiderEgg1 = new("Spider Egg I", 0, 1, 3, false, null, EnemyColors.SpiderEgg1, intoxicated);
		SpiderEgg2 = new("Spider Egg II", 0, 1, 3, false, null, EnemyColors.SpiderEgg2, envenomated);
		Spiderling = new("Spiderling", 0, 1, 3, true, null, EnemyColors.Spiderling, infested);

		Skull1.SetTransmuteInto(TransmutedSkull1);
		Skull2.SetTransmuteInto(TransmutedSkull2);
		Skull3.SetTransmuteInto(TransmutedSkull3);
		Skull4.SetTransmuteInto(TransmutedSkull4);

		Orb.SetSpawnedBy(Leviathan);
		Skull1.SetSpawnedBy(Squid1, Squid2, Squid3);
		Skull2.SetSpawnedBy(Squid1);
		Skull3.SetSpawnedBy(Squid2);
		Skull4.SetSpawnedBy(Squid3);
		SpiderEgg1.SetSpawnedBy(Spider1);
		SpiderEgg2.SetSpawnedBy(Spider2);
		Spiderling.SetSpawnedBy(SpiderEgg1, SpiderEgg2);

		Enemy[] enemies = [Squid1, Squid2, Squid3, Centipede, Gigapede, Ghostpede, Leviathan, Spider1, Spider2, Thorn, Orb, Skull1, Skull2, Skull3, Skull4, TransmutedSkull1, TransmutedSkull2, TransmutedSkull3, TransmutedSkull4, SpiderEgg1, SpiderEgg2, Spiderling];

		Level1Dagger = new("Level 1", DaggerColors.Level1);
		Level2Dagger = new("Level 2", DaggerColors.Level2);
		Level3Dagger = new("Level 3", DaggerColors.Level3);
		Level4Dagger = new("Level 4", DaggerColors.Level4);
		Level3HomingDagger = new("Level 3 Homing", DaggerColors.Level3Homing);
		Level4HomingDagger = new("Level 4 Homing", DaggerColors.Level4Homing);
		Level4Splash = new("Level 4 Splash", DaggerColors.Level4Splash);
		Dagger[] daggers = [Level1Dagger, Level2Dagger, Level3Dagger, Level4Dagger, Level3HomingDagger, Level4HomingDagger, Level4Splash];

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
			damages.Add(new(Level1Dagger, enemy, 1, 1));
			damages.Add(new(Level2Dagger, enemy, 1, 1));
			damages.Add(new(Level3Dagger, enemy, 1, 1));
			damages.Add(new(Level4Dagger, enemy, 1, 1));
		}

		// Level 3 homing daggers deal 10 damage and get destroyed on impact. Exceptions:
		// - They deal 30 damage to Squid 3.
		// - They can take out 4 Skull Is before getting destroyed.
		// - They phase through Ghostpede.
		damages.Add(new(Level3HomingDagger, Squid1, 1, 10));
		damages.Add(new(Level3HomingDagger, Squid2, 1, 10));
		damages.Add(new(Level3HomingDagger, Squid3, 1, 30));
		damages.Add(new(Level3HomingDagger, Centipede, 1, 10));
		damages.Add(new(Level3HomingDagger, Gigapede, 1, 10));
		damages.Add(new(Level3HomingDagger, Leviathan, 1, 1));
		damages.Add(new(Level3HomingDagger, Orb, 1, 1));
		damages.Add(new(Level3HomingDagger, Spider1, 1, 10));
		damages.Add(new(Level3HomingDagger, Spider2, 1, 10));
		damages.Add(new(Level3HomingDagger, Thorn, 1, 10));
		damages.Add(new(Level3HomingDagger, Skull1, 0.25f, 10));
		damages.Add(new(Level3HomingDagger, Skull2, 1, 10));
		damages.Add(new(Level3HomingDagger, Skull3, 1, 10));
		damages.Add(new(Level3HomingDagger, Skull4, 1, 10));
		damages.Add(new(Level3HomingDagger, TransmutedSkull1, 1, 10));
		damages.Add(new(Level3HomingDagger, TransmutedSkull2, 1, 10));
		damages.Add(new(Level3HomingDagger, TransmutedSkull3, 1, 10));
		damages.Add(new(Level3HomingDagger, TransmutedSkull4, 1, 10));
		damages.Add(new(Level3HomingDagger, SpiderEgg1, 1, 10));
		damages.Add(new(Level3HomingDagger, SpiderEgg2, 1, 10));
		damages.Add(new(Level3HomingDagger, Spiderling, 1, 10));

		// TODO: Level 4 splash depletion doesn't really make sense. Needs more research. For now the values are all set to 1.
		// TODO: Needs confirmation. Splash definitely damages squids but not sure about normal level 4 homing.
		damages.Add(new(Level4HomingDagger, Squid1, 1, 0));
		damages.Add(new(Level4HomingDagger, Squid2, 1, 0));
		damages.Add(new(Level4HomingDagger, Squid3, 1, 0));
		damages.Add(new(Level4Splash, Squid1, 1, 10));
		damages.Add(new(Level4Splash, Squid2, 1, 10));
		damages.Add(new(Level4Splash, Squid3, 1, 10));

		// Only splash damages pedes (including Ghostpede). Level 4 homing phases through Ghostpede.
		damages.Add(new(Level4HomingDagger, Centipede, 1, 0));
		damages.Add(new(Level4HomingDagger, Gigapede, 1, 0));
		damages.Add(new(Level4Splash, Centipede, 1, 10));
		damages.Add(new(Level4Splash, Gigapede, 1, 10));
		damages.Add(new(Level4Splash, Ghostpede, 1, 10));

		// TODO: Needs confirmation. Not sure if splash deals the damage instead.
		damages.Add(new(Level4HomingDagger, Leviathan, 1, 1));
		damages.Add(new(Level4Splash, Leviathan, 1, 0));
		damages.Add(new(Level4HomingDagger, Orb, 1, 1));
		damages.Add(new(Level4Splash, Orb, 1, 0));

		// TODO: Needs confirmation. Not sure if splash deals the damage instead.
		damages.Add(new(Level4HomingDagger, Spider1, 1, 10));
		damages.Add(new(Level4HomingDagger, Spider2, 1, 10));
		damages.Add(new(Level4Splash, Spider1, 1, 0));
		damages.Add(new(Level4Splash, Spider2, 1, 0));

		// TODO: Needs confirmation. Not sure which dagger deals 10 and which deals 1.
		damages.Add(new(Level4HomingDagger, Thorn, 1, 1));
		damages.Add(new(Level4Splash, Thorn, 1, 10));

		damages.Add(new(Level4HomingDagger, Skull1, 0.25f, 10));
		damages.Add(new(Level4HomingDagger, Skull2, 1, 10));
		damages.Add(new(Level4HomingDagger, Skull3, 1, 10));
		damages.Add(new(Level4HomingDagger, Skull4, 1, 10));
		damages.Add(new(Level4Splash, Skull1, 1, 10));
		damages.Add(new(Level4Splash, Skull2, 1, 10));
		damages.Add(new(Level4Splash, Skull3, 1, 10));
		damages.Add(new(Level4Splash, Skull4, 1, 0)); // TODO: Test.

		damages.Add(new(Level4HomingDagger, TransmutedSkull1, 1, 10));
		damages.Add(new(Level4HomingDagger, TransmutedSkull2, 1, 10));
		damages.Add(new(Level4HomingDagger, TransmutedSkull3, 1, 10));
		damages.Add(new(Level4HomingDagger, TransmutedSkull4, 1, 10));
		damages.Add(new(Level4Splash, TransmutedSkull1, 1, 10));
		damages.Add(new(Level4Splash, TransmutedSkull2, 1, 10));
		damages.Add(new(Level4Splash, TransmutedSkull3, 1, 10));
		damages.Add(new(Level4Splash, TransmutedSkull4, 1, 10)); // TODO: Test.

		// TODO: Needs confirmation. Not sure if splash deals the damage instead.
		damages.Add(new(Level4HomingDagger, SpiderEgg1, 1, 10));
		damages.Add(new(Level4HomingDagger, SpiderEgg2, 1, 10));
		damages.Add(new(Level4HomingDagger, Spiderling, 1, 10));
		damages.Add(new(Level4Splash, SpiderEgg1, 1, 0));
		damages.Add(new(Level4Splash, SpiderEgg2, 1, 0));
		damages.Add(new(Level4Splash, Spiderling, 1, 0));

		Name = "V3.2";
		ReleaseDate = new(2021, 10, 27);
		Deaths = deaths;
		Enemies = enemies;
		Daggers = daggers;
		Upgrades = upgrades;
		UnlockDaggers = unlockDaggers;
		Damages = damages;
	}

	public string Name { get; }
	public DateOnly ReleaseDate { get; }
	public IReadOnlyList<Enemy> Enemies { get; }
	public IReadOnlyList<Dagger> Daggers { get; }
	public IReadOnlyList<Upgrade> Upgrades { get; }
	public IReadOnlyList<UnlockDagger> UnlockDaggers { get; }
	public IReadOnlyList<Damage> Damages { get; }
	public IReadOnlyList<Death> Deaths { get; }

	public Enemy Squid1 { get; }
	public Enemy Squid2 { get; }
	public Enemy Squid3 { get; }
	public Enemy Centipede { get; }
	public Enemy Gigapede { get; }
	public Enemy Ghostpede { get; }
	public Enemy Leviathan { get; }
	public Enemy Spider1 { get; }
	public Enemy Spider2 { get; }
	public Enemy Thorn { get; }
	public Enemy Orb { get; }
	public Enemy Skull1 { get; }
	public Enemy Skull2 { get; }
	public Enemy Skull3 { get; }
	public Enemy Skull4 { get; }
	public Enemy TransmutedSkull1 { get; }
	public Enemy TransmutedSkull2 { get; }
	public Enemy TransmutedSkull3 { get; }
	public Enemy TransmutedSkull4 { get; }
	public Enemy SpiderEgg1 { get; }
	public Enemy SpiderEgg2 { get; }
	public Enemy Spiderling { get; }

	public Dagger Level1Dagger { get; }
	public Dagger Level2Dagger { get; }
	public Dagger Level3Dagger { get; }
	public Dagger Level4Dagger { get; }
	public Dagger Level3HomingDagger { get; }
	public Dagger Level4HomingDagger { get; }
	public Dagger Level4Splash { get; }

	public static V3_2 Instance { get; } = new();
}
