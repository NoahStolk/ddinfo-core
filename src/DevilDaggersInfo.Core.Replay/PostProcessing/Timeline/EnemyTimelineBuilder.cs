using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.PostProcessing.Timeline;

public class EnemyTimelineBuilder
{
	private readonly List<EnemyTimelineBuildContext> _builds = new();
	private readonly Dictionary<int, EntityType> _daggers = new();

	public List<EnemyTimeline> Build(IReadOnlyList<ReplayEvent> events)
	{
		_builds.Clear();
		_daggers.Clear();
		int currentTick = 0;
		int currentEntityId = 0;

		foreach (IEventData e in events.Select(e => e.Data))
		{
			if (e is DaggerSpawnEventData or BoidSpawnEventData or LeviathanSpawnEventData or PedeSpawnEventData or SquidSpawnEventData or SpiderSpawnEventData or SpiderEggSpawnEventData or ThornSpawnEventData)
			{
				currentEntityId++;
			}

			if (e is DaggerSpawnEventData dagger)
			{
				_daggers.Add(currentEntityId, dagger.EntityType);
			}
			else if (e is ISpawnEventData spawn)
			{
				_builds.Add(new(currentEntityId, spawn.EntityType, currentTick));
			}
			else if (e is HitEventData hit)
			{
				EnemyTimelineBuildContext? enemy = _builds.Find(c => c.EntityId == hit.EntityIdA);
				if (enemy == null)
					continue;

				if (!_daggers.TryGetValue(hit.EntityIdB, out EntityType daggerType))
					continue;

				int damage = enemy.EntityType.GetDamage(daggerType, hit.UserData);
				if (damage == 0)
					continue;

				if (enemy.HpPerTick.ContainsKey(currentTick))
					enemy.HpPerTick[currentTick] -= damage;
				else
					enemy.HpPerTick.Add(currentTick, enemy.CurrentHp - damage);

				enemy.CurrentHp -= damage;
			}
			else if (e is TransmuteEventData transmute)
			{
				EnemyTimelineBuildContext? enemy = _builds.Find(c => c.EntityId == transmute.EntityId);
				if (enemy == null)
					continue;

				enemy.Transmute();
			}
			else if (e is InputsEventData or InitialInputsEventData)
			{
				currentTick++;
			}
		}

		return _builds.ConvertAll(b => new EnemyTimeline(b.EntityId, b.HpPerTick.Select(d => new EnemyTimelineEvent(d.Key, d.Value)).ToList()));
	}

	private sealed class EnemyTimelineBuildContext
	{
		public EnemyTimelineBuildContext(int entityId, EntityType entityType, int currentTick)
		{
			EntityId = entityId;
			EntityType = entityType;

			CurrentHp = entityType.GetInitialHp();
			HpPerTick.Add(currentTick, CurrentHp);
		}

		public int EntityId { get; }

		public EntityType EntityType { get; }

		public int CurrentHp { get; set; }

		public Dictionary<int, int> HpPerTick { get; } = new();

		public void Transmute()
		{
			CurrentHp = EntityType.GetInitialTransmuteHp();
		}
	}
}
