using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.PostProcessing.HitLog;

public static class EnemyHitLogBuilder
{
	public static EnemyHitLog? Build(IReadOnlyList<ReplayEvent> events, int enemyEntityId)
	{
		int currentTick = 0;
		int currentEntityId = 0;
		EnemyHitLogBuildContext? buildContext = null;
		Dictionary<int, EntityType> daggers = new();

		foreach (IEventData e in events.Select(e => e.Data))
		{
			if (e is DaggerSpawnEventData or BoidSpawnEventData or LeviathanSpawnEventData or PedeSpawnEventData or SquidSpawnEventData or SpiderSpawnEventData or SpiderEggSpawnEventData or ThornSpawnEventData)
			{
				currentEntityId++;
			}

			if (e is DaggerSpawnEventData dagger)
			{
				daggers.Add(currentEntityId, dagger.EntityType);
			}
			else if (e is ISpawnEventData spawn)
			{
				if (currentEntityId != enemyEntityId)
					continue;

				buildContext = new(spawn.EntityType, currentTick);
			}
			else if (e is HitEventData hit && hit.EntityIdA == enemyEntityId)
			{
				if (buildContext == null)
					continue;

 #pragma warning disable S4158
				EntityType? daggerEntityType = daggers.TryGetValue(hit.EntityIdB, out EntityType value) ? value : null;
 #pragma warning restore S4158
				if (daggerEntityType == null)
					continue;

				// TODO: In case of enemies with multiple weak points (squids, pedes, etc), we need to keep track of all the weak point's HP separately in the EnemyHitLogBuildContext.
				// If we don't do that we subtract too much damage from the enemy's HP.
				// For example, level 4 homing deals 10 damage, but when hitting a Gigapede segment with only 3 HP left, this should not count as 10 damage.
				int damage = buildContext.EntityType.GetDamage(daggerEntityType.Value, hit.UserData);
				buildContext.CurrentHp -= damage;
				buildContext.Events.Add(new(currentTick, buildContext.CurrentHp, damage, daggerEntityType.Value.GetDaggerType(), hit.UserData));
			}
			else if (e is TransmuteEventData transmute && transmute.EntityId == enemyEntityId)
			{
				if (buildContext == null)
					continue;

				buildContext.Transmute();
			}
			else if (e is InputsEventData or InitialInputsEventData)
			{
				currentTick++;
			}
		}

		if (buildContext == null)
			return null;

		List<EnemyHitLogEvent> hitLogEvents = buildContext.Events.ConvertAll(e => new EnemyHitLogEvent
		{
			DaggerType = e.DaggerType,
			Damage = e.Damage,
			Hp = e.Hp,
			Tick = e.Tick,
			UserData = e.UserData,
		});

		return new(enemyEntityId, buildContext.EntityType, buildContext.SpawnTick, hitLogEvents);
	}

	private sealed class EnemyHitLogBuildContext
	{
		public EnemyHitLogBuildContext(EntityType entityType, int spawnTick)
		{
			EntityType = entityType;
			SpawnTick = spawnTick;

			CurrentHp = entityType.GetInitialHp();
		}

		public EntityType EntityType { get; }

		public int SpawnTick { get; }

		public List<EnemyHitLogEvent> Events { get; } = [];

		public int CurrentHp { get; set; }

		public void Transmute()
		{
			CurrentHp = EntityType.GetInitialTransmuteHp();
		}
	}
}
