// ReSharper disable StringLiteralTypo
using System.Diagnostics;

namespace DevilDaggersInfo.Core.CriteriaExpression.Extensions;

public static class CustomLeaderboardCriteriaTypeExtensions
{
	extension(CustomLeaderboardCriteriaType criteriaType)
	{
		public string Display()
		{
			return criteriaType switch
			{
				CustomLeaderboardCriteriaType.GemsCollected => "Gems collected",
				CustomLeaderboardCriteriaType.GemsDespawned => "Gems despawned",
				CustomLeaderboardCriteriaType.GemsEaten => "Gems eaten",
				CustomLeaderboardCriteriaType.EnemiesKilled => "Total kills",
				CustomLeaderboardCriteriaType.DaggersFired => "Daggers fired",
				CustomLeaderboardCriteriaType.DaggersHit => "Daggers hit",
				CustomLeaderboardCriteriaType.HomingStored => "Homing stored",
				CustomLeaderboardCriteriaType.HomingEaten => "Homing eaten",
				CustomLeaderboardCriteriaType.Skull1KillCount => "Skull I kills",
				CustomLeaderboardCriteriaType.Skull2KillCount => "Skull II kills",
				CustomLeaderboardCriteriaType.Skull3KillCount => "Skull III kills",
				CustomLeaderboardCriteriaType.Skull4KillCount => "Skull IV kills",
				CustomLeaderboardCriteriaType.SpiderlingKillCount => "Spiderling kills",
				CustomLeaderboardCriteriaType.SpiderEggKillCount => "Spider Egg kills",
				CustomLeaderboardCriteriaType.Squid1KillCount => "Squid I kills",
				CustomLeaderboardCriteriaType.Squid2KillCount => "Squid II kills",
				CustomLeaderboardCriteriaType.Squid3KillCount => "Squid III kills",
				CustomLeaderboardCriteriaType.CentipedeKillCount => "Centipede kills",
				CustomLeaderboardCriteriaType.GigapedeKillCount => "Gigapede kills",
				CustomLeaderboardCriteriaType.GhostpedeKillCount => "Ghostpede kills",
				CustomLeaderboardCriteriaType.Spider1KillCount => "Spider I kills",
				CustomLeaderboardCriteriaType.Spider2KillCount => "Spider II kills",
				CustomLeaderboardCriteriaType.LeviathanKillCount => "Leviathan kills",
				CustomLeaderboardCriteriaType.OrbKillCount => "Orb kills",
				CustomLeaderboardCriteriaType.ThornKillCount => "Thorn kills",
				CustomLeaderboardCriteriaType.Skull1AliveCount => "Skull Is alive",
				CustomLeaderboardCriteriaType.Skull2AliveCount => "Skull IIs alive",
				CustomLeaderboardCriteriaType.Skull3AliveCount => "Skull IIIs alive",
				CustomLeaderboardCriteriaType.Skull4AliveCount => "Skull IVs alive",
				CustomLeaderboardCriteriaType.SpiderlingAliveCount => "Spiderlings alive",
				CustomLeaderboardCriteriaType.SpiderEggAliveCount => "Spider Eggs alive",
				CustomLeaderboardCriteriaType.Squid1AliveCount => "Squid Is alive",
				CustomLeaderboardCriteriaType.Squid2AliveCount => "Squid IIs alive",
				CustomLeaderboardCriteriaType.Squid3AliveCount => "Squid IIIs alive",
				CustomLeaderboardCriteriaType.CentipedeAliveCount => "Centipedes alive",
				CustomLeaderboardCriteriaType.GigapedeAliveCount => "Gigapedes alive",
				CustomLeaderboardCriteriaType.GhostpedeAliveCount => "Ghostpedes alive",
				CustomLeaderboardCriteriaType.Spider1AliveCount => "Spider Is alive",
				CustomLeaderboardCriteriaType.Spider2AliveCount => "Spider IIs alive",
				CustomLeaderboardCriteriaType.LeviathanAliveCount => "Leviathans alive",
				CustomLeaderboardCriteriaType.OrbAliveCount => "Orbs alive",
				CustomLeaderboardCriteriaType.ThornAliveCount => "Thorns alive",
				CustomLeaderboardCriteriaType.DeathType => "Death type",
				CustomLeaderboardCriteriaType.Time => "Time",
				CustomLeaderboardCriteriaType.LevelUpTime2 => "Level 2 hand",
				CustomLeaderboardCriteriaType.LevelUpTime3 => "Level 3 hand",
				CustomLeaderboardCriteriaType.LevelUpTime4 => "Level 4 hand",
				CustomLeaderboardCriteriaType.EnemiesAlive => "Enemies alive",
				_ => throw new UnreachableException(),
			};
		}

		public string GetIdentifier()
		{
			return criteriaType switch
			{
				CustomLeaderboardCriteriaType.GemsCollected => "gems",
				CustomLeaderboardCriteriaType.GemsDespawned => "gemsdespawned",
				CustomLeaderboardCriteriaType.GemsEaten => "gemseaten",
				CustomLeaderboardCriteriaType.EnemiesKilled => "kills",
				CustomLeaderboardCriteriaType.DaggersFired => "daggers",
				CustomLeaderboardCriteriaType.DaggersHit => "hits",
				CustomLeaderboardCriteriaType.HomingStored => "homing",
				CustomLeaderboardCriteriaType.HomingEaten => "homingeaten",
				CustomLeaderboardCriteriaType.Skull1KillCount => "skull1kills",
				CustomLeaderboardCriteriaType.Skull2KillCount => "skull2kills",
				CustomLeaderboardCriteriaType.Skull3KillCount => "skull3kills",
				CustomLeaderboardCriteriaType.Skull4KillCount => "skull4kills",
				CustomLeaderboardCriteriaType.SpiderlingKillCount => "spiderlingkills",
				CustomLeaderboardCriteriaType.SpiderEggKillCount => "eggkills",
				CustomLeaderboardCriteriaType.Squid1KillCount => "squid1kills",
				CustomLeaderboardCriteriaType.Squid2KillCount => "squid2kills",
				CustomLeaderboardCriteriaType.Squid3KillCount => "squid3kills",
				CustomLeaderboardCriteriaType.CentipedeKillCount => "centikills",
				CustomLeaderboardCriteriaType.GigapedeKillCount => "gigakills",
				CustomLeaderboardCriteriaType.GhostpedeKillCount => "ghostkills",
				CustomLeaderboardCriteriaType.Spider1KillCount => "spider1kills",
				CustomLeaderboardCriteriaType.Spider2KillCount => "spider2kills",
				CustomLeaderboardCriteriaType.LeviathanKillCount => "levikills",
				CustomLeaderboardCriteriaType.OrbKillCount => "orbkills",
				CustomLeaderboardCriteriaType.ThornKillCount => "thornkills",
				CustomLeaderboardCriteriaType.Skull1AliveCount => "skull1salive",
				CustomLeaderboardCriteriaType.Skull2AliveCount => "skull2salive",
				CustomLeaderboardCriteriaType.Skull3AliveCount => "skull3salive",
				CustomLeaderboardCriteriaType.Skull4AliveCount => "skull4salive",
				CustomLeaderboardCriteriaType.SpiderlingAliveCount => "spiderlingsalive",
				CustomLeaderboardCriteriaType.SpiderEggAliveCount => "eggsalive",
				CustomLeaderboardCriteriaType.Squid1AliveCount => "squid1salive",
				CustomLeaderboardCriteriaType.Squid2AliveCount => "squid2salive",
				CustomLeaderboardCriteriaType.Squid3AliveCount => "squid3salive",
				CustomLeaderboardCriteriaType.CentipedeAliveCount => "centisalive",
				CustomLeaderboardCriteriaType.GigapedeAliveCount => "gigasalive",
				CustomLeaderboardCriteriaType.GhostpedeAliveCount => "ghostsalive",
				CustomLeaderboardCriteriaType.Spider1AliveCount => "spider1salive",
				CustomLeaderboardCriteriaType.Spider2AliveCount => "spider2salive",
				CustomLeaderboardCriteriaType.LeviathanAliveCount => "levisalive",
				CustomLeaderboardCriteriaType.OrbAliveCount => "orbsalive",
				CustomLeaderboardCriteriaType.ThornAliveCount => "thornsalive",
				CustomLeaderboardCriteriaType.DeathType => "death",
				CustomLeaderboardCriteriaType.Time => "time",
				CustomLeaderboardCriteriaType.LevelUpTime2 => "level2",
				CustomLeaderboardCriteriaType.LevelUpTime3 => "level3",
				CustomLeaderboardCriteriaType.LevelUpTime4 => "level4",
				CustomLeaderboardCriteriaType.EnemiesAlive => "enemiesalive",
				_ => throw new UnreachableException(),
			};
		}

		public string ToStringFast()
		{
			return criteriaType switch
			{
				CustomLeaderboardCriteriaType.GemsCollected => nameof(CustomLeaderboardCriteriaType.GemsCollected),
				CustomLeaderboardCriteriaType.GemsDespawned => nameof(CustomLeaderboardCriteriaType.GemsDespawned),
				CustomLeaderboardCriteriaType.GemsEaten => nameof(CustomLeaderboardCriteriaType.GemsEaten),
				CustomLeaderboardCriteriaType.EnemiesKilled => nameof(CustomLeaderboardCriteriaType.EnemiesKilled),
				CustomLeaderboardCriteriaType.DaggersFired => nameof(CustomLeaderboardCriteriaType.DaggersFired),
				CustomLeaderboardCriteriaType.DaggersHit => nameof(CustomLeaderboardCriteriaType.DaggersHit),
				CustomLeaderboardCriteriaType.HomingStored => nameof(CustomLeaderboardCriteriaType.HomingStored),
				CustomLeaderboardCriteriaType.HomingEaten => nameof(CustomLeaderboardCriteriaType.HomingEaten),
				CustomLeaderboardCriteriaType.Skull1KillCount => nameof(CustomLeaderboardCriteriaType.Skull1KillCount),
				CustomLeaderboardCriteriaType.Skull2KillCount => nameof(CustomLeaderboardCriteriaType.Skull2KillCount),
				CustomLeaderboardCriteriaType.Skull3KillCount => nameof(CustomLeaderboardCriteriaType.Skull3KillCount),
				CustomLeaderboardCriteriaType.Skull4KillCount => nameof(CustomLeaderboardCriteriaType.Skull4KillCount),
				CustomLeaderboardCriteriaType.SpiderlingKillCount => nameof(CustomLeaderboardCriteriaType.SpiderlingKillCount),
				CustomLeaderboardCriteriaType.SpiderEggKillCount => nameof(CustomLeaderboardCriteriaType.SpiderEggKillCount),
				CustomLeaderboardCriteriaType.Squid1KillCount => nameof(CustomLeaderboardCriteriaType.Squid1KillCount),
				CustomLeaderboardCriteriaType.Squid2KillCount => nameof(CustomLeaderboardCriteriaType.Squid2KillCount),
				CustomLeaderboardCriteriaType.Squid3KillCount => nameof(CustomLeaderboardCriteriaType.Squid3KillCount),
				CustomLeaderboardCriteriaType.CentipedeKillCount => nameof(CustomLeaderboardCriteriaType.CentipedeKillCount),
				CustomLeaderboardCriteriaType.GigapedeKillCount => nameof(CustomLeaderboardCriteriaType.GigapedeKillCount),
				CustomLeaderboardCriteriaType.GhostpedeKillCount => nameof(CustomLeaderboardCriteriaType.GhostpedeKillCount),
				CustomLeaderboardCriteriaType.Spider1KillCount => nameof(CustomLeaderboardCriteriaType.Spider1KillCount),
				CustomLeaderboardCriteriaType.Spider2KillCount => nameof(CustomLeaderboardCriteriaType.Spider2KillCount),
				CustomLeaderboardCriteriaType.LeviathanKillCount => nameof(CustomLeaderboardCriteriaType.LeviathanKillCount),
				CustomLeaderboardCriteriaType.OrbKillCount => nameof(CustomLeaderboardCriteriaType.OrbKillCount),
				CustomLeaderboardCriteriaType.ThornKillCount => nameof(CustomLeaderboardCriteriaType.ThornKillCount),
				CustomLeaderboardCriteriaType.Skull1AliveCount => nameof(CustomLeaderboardCriteriaType.Skull1AliveCount),
				CustomLeaderboardCriteriaType.Skull2AliveCount => nameof(CustomLeaderboardCriteriaType.Skull2AliveCount),
				CustomLeaderboardCriteriaType.Skull3AliveCount => nameof(CustomLeaderboardCriteriaType.Skull3AliveCount),
				CustomLeaderboardCriteriaType.Skull4AliveCount => nameof(CustomLeaderboardCriteriaType.Skull4AliveCount),
				CustomLeaderboardCriteriaType.SpiderlingAliveCount => nameof(CustomLeaderboardCriteriaType.SpiderlingAliveCount),
				CustomLeaderboardCriteriaType.SpiderEggAliveCount => nameof(CustomLeaderboardCriteriaType.SpiderEggAliveCount),
				CustomLeaderboardCriteriaType.Squid1AliveCount => nameof(CustomLeaderboardCriteriaType.Squid1AliveCount),
				CustomLeaderboardCriteriaType.Squid2AliveCount => nameof(CustomLeaderboardCriteriaType.Squid2AliveCount),
				CustomLeaderboardCriteriaType.Squid3AliveCount => nameof(CustomLeaderboardCriteriaType.Squid3AliveCount),
				CustomLeaderboardCriteriaType.CentipedeAliveCount => nameof(CustomLeaderboardCriteriaType.CentipedeAliveCount),
				CustomLeaderboardCriteriaType.GigapedeAliveCount => nameof(CustomLeaderboardCriteriaType.GigapedeAliveCount),
				CustomLeaderboardCriteriaType.GhostpedeAliveCount => nameof(CustomLeaderboardCriteriaType.GhostpedeAliveCount),
				CustomLeaderboardCriteriaType.Spider1AliveCount => nameof(CustomLeaderboardCriteriaType.Spider1AliveCount),
				CustomLeaderboardCriteriaType.Spider2AliveCount => nameof(CustomLeaderboardCriteriaType.Spider2AliveCount),
				CustomLeaderboardCriteriaType.LeviathanAliveCount => nameof(CustomLeaderboardCriteriaType.LeviathanAliveCount),
				CustomLeaderboardCriteriaType.OrbAliveCount => nameof(CustomLeaderboardCriteriaType.OrbAliveCount),
				CustomLeaderboardCriteriaType.ThornAliveCount => nameof(CustomLeaderboardCriteriaType.ThornAliveCount),
				CustomLeaderboardCriteriaType.DeathType => nameof(CustomLeaderboardCriteriaType.DeathType),
				CustomLeaderboardCriteriaType.Time => nameof(CustomLeaderboardCriteriaType.Time),
				CustomLeaderboardCriteriaType.LevelUpTime2 => nameof(CustomLeaderboardCriteriaType.LevelUpTime2),
				CustomLeaderboardCriteriaType.LevelUpTime3 => nameof(CustomLeaderboardCriteriaType.LevelUpTime3),
				CustomLeaderboardCriteriaType.LevelUpTime4 => nameof(CustomLeaderboardCriteriaType.LevelUpTime4),
				CustomLeaderboardCriteriaType.EnemiesAlive => nameof(CustomLeaderboardCriteriaType.EnemiesAlive),
				_ => throw new UnreachableException(),
			};
		}

		public bool IsAllowedAsTarget()
		{
			return criteriaType is not (
				CustomLeaderboardCriteriaType.DeathType or
				CustomLeaderboardCriteriaType.Time or
				CustomLeaderboardCriteriaType.LevelUpTime2 or
				CustomLeaderboardCriteriaType.LevelUpTime3 or
				CustomLeaderboardCriteriaType.LevelUpTime4);
		}
	}
}
