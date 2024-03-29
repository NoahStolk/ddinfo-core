using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.Extensions;

public static class EntityTypeExtensions
{
	public static bool IsEnemy(this EntityType entityType)
	{
		return !entityType.IsDagger() && entityType != EntityType.Zero;
	}

	public static bool IsDagger(this EntityType entityType)
	{
		return entityType is EntityType.Level1Dagger or EntityType.Level2Dagger or EntityType.Level3Dagger or EntityType.Level3HomingDagger or EntityType.Level4Dagger or EntityType.Level4HomingDagger or EntityType.Level4HomingSplash;
	}

	public static DaggerType GetDaggerType(this EntityType entityType) => entityType switch
	{
		EntityType.Level1Dagger => DaggerType.Level1,
		EntityType.Level2Dagger => DaggerType.Level2,
		EntityType.Level3Dagger => DaggerType.Level3,
		EntityType.Level3HomingDagger => DaggerType.Level3Homing,
		EntityType.Level4Dagger => DaggerType.Level4,
		EntityType.Level4HomingDagger => DaggerType.Level4Homing,
		EntityType.Level4HomingSplash => DaggerType.Level4HomingSplash,
		_ => throw new InvalidOperationException($"{nameof(EntityType)} '{entityType}' is not a dagger."),
	};

	public static int GetInitialHp(this EntityType entityType) => entityType switch
	{
		EntityType.Skull1 => 1,
		EntityType.Skull2 => 5,
		EntityType.Skull3 => 10,
		EntityType.Skull4 => 100,
		EntityType.Squid1 => 10,
		EntityType.Squid2 => 20,
		EntityType.Squid3 => 90,
		EntityType.Centipede => 75,
		EntityType.Gigapede => 250,
		EntityType.Ghostpede => 500,
		EntityType.Leviathan => 1500,
		EntityType.Spider1 => 25,
		EntityType.Spider2 => 200,
		EntityType.SpiderEgg => 3,
		EntityType.Spiderling => 3,
		EntityType.Thorn => 120,
		_ => throw new InvalidOperationException($"{nameof(EntityType)} '{entityType}' is not an enemy."),
	};

	private static bool IsWeakPoint(this EntityType entityType, int userData) => entityType switch
	{
		EntityType.Squid2 => userData is >= 0 and < 2,
		EntityType.Squid3 => userData is >= 0 and < 3,
		EntityType.Leviathan => userData is >= 0 and < 6,
		EntityType.Squid1 or EntityType.Spider1 or EntityType.Spider2 => userData == 0,

		// Everything else is a hit by default, including pedes.
		// When damaging a dead pede segment, the ID of the pede is negated, which is ignored because a negative EntityType does not resolve to a pede.
		_ => true,
	};

	public static int GetInitialTransmuteHp(this EntityType entityType) => entityType switch
	{
		EntityType.Skull1 => 10,
		EntityType.Skull2 => 20,
		EntityType.Skull3 => 100,
		EntityType.Skull4 => 300,
		EntityType.Leviathan => 2400,
		_ => throw new InvalidOperationException($"{nameof(EntityType)} '{entityType}' cannot be transmuted."),
	};

	public static int GetDamage(this EntityType enemyType, EntityType daggerType, int userData)
	{
		if (!enemyType.IsEnemy())
			throw new InvalidOperationException($"Type '{enemyType}' must be an enemy.");

		if (!daggerType.IsDagger())
			throw new InvalidOperationException($"Type '{daggerType}' must be a dagger.");

		if (!enemyType.IsWeakPoint(userData))
			return 0;

		// Note that we don't use the GameData project here, since there is a discrepancy between the game's code and the wiki.
		// TODO: We probably need to know if a skull is transmuted at this point.
		return daggerType switch
		{
			EntityType.Level1Dagger or EntityType.Level2Dagger or EntityType.Level3Dagger or EntityType.Level4Dagger => 1,
			EntityType.Level3HomingDagger => enemyType switch
			{
				EntityType.Squid3 => 30, // Probably unintentional
				EntityType.Ghostpede => 0, // Homing phase through Ghostpede
				EntityType.Leviathan => 1, // Homing deals normal damage to Leviathan (and Orb, which is the same enemy in this context)
				_ => 10,
			},
			EntityType.Level4HomingDagger => enemyType switch
			{
				EntityType.Leviathan => 1, // Homing deals normal damage to Leviathan (and Orb, which is the same enemy in this context)
				EntityType.Centipede or EntityType.Gigapede or EntityType.Ghostpede => 0, // Only splash damages pedes (including Ghostpede)
				EntityType.Squid1 or EntityType.Squid2 or EntityType.Squid3 => 0,
				EntityType.Thorn => 1, // Both splash and homing deal damage to Thorns, so the total damage is 11.
				_ => 10, // TODO: Test if this is correct.
			},
			EntityType.Level4HomingSplash => enemyType switch
			{
				EntityType.Thorn => 10, // Thorns are an exception
				EntityType.Centipede or EntityType.Gigapede or EntityType.Ghostpede => 10, // Only splash damages pedes (including Ghostpede)
				EntityType.Squid1 or EntityType.Squid2 or EntityType.Squid3 => 10,
				_ => 0, // TODO: This is probably wrong. Skulls should be damaged by splash.
			},
			_ => 0,
		};
	}
}
