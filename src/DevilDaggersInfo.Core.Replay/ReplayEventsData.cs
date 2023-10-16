using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay;

/// <summary>
/// Represents all the events in a replay.
/// </summary>
public class ReplayEventsData
{
	private readonly List<ReplayEvent> _events = new();
	private readonly List<int> _eventOffsetsPerTick = new() { 0, 0 };
	private readonly List<EntitySpawnReplayEvent> _entitySpawnReplayEvents = new();

	public IReadOnlyList<ReplayEvent> Events => _events;
	public IReadOnlyList<int> EventOffsetsPerTick => _eventOffsetsPerTick;

	public int TickCount => EventOffsetsPerTick.Count - 1;
	public int SpawnEventCount => _entitySpawnReplayEvents.Count;

	public void Clear()
	{
		_events.Clear();

		_eventOffsetsPerTick.Clear();
		_eventOffsetsPerTick.Add(0);
		_eventOffsetsPerTick.Add(0);

		_entitySpawnReplayEvents.Clear();
	}

	public void AddEvent(IEventData e)
	{
		if (e is ISpawnEventData spawnEventData)
		{
			EntitySpawnReplayEvent spawnEvent = new(_entitySpawnReplayEvents.Count + 1, spawnEventData);
			_events.Add(spawnEvent);
			_entitySpawnReplayEvents.Add(spawnEvent);
		}
		else
		{
			_events.Add(new(e));
		}

		_eventOffsetsPerTick[^1]++;
		if (e is InputsEventData or InitialInputsEventData)
			_eventOffsetsPerTick.Add(_events.Count);
	}

	public void RemoveEvent(int index)
	{
		if (index < 0 || index >= _events.Count)
			throw new ArgumentOutOfRangeException(nameof(index));

		ReplayEvent e = _events[index];
		if (e is EntitySpawnReplayEvent spawnEvent)
		{
			_entitySpawnReplayEvents.RemoveAt(spawnEvent.EntityId - 1);

			// Decrement all entity IDs that are higher than the removed entity ID.
			for (int i = 0; i < _events.Count; i++)
			{
				if (i == index)
					continue;

				if (_events[i] is EntitySpawnReplayEvent otherSpawnEvent && otherSpawnEvent.EntityId > spawnEvent.EntityId)
					otherSpawnEvent.EntityId--;

				if (i >= index)
				{
					IEventData data = _events[i].Data;
					UpdateReferringEntityIdsOnRemove(data, spawnEvent.EntityId);
				}
			}
		}

		_events.Remove(e);

		int? containingTick = null;
		bool isInputsEvent = e.Data is InputsEventData or InitialInputsEventData;
		for (int i = 0; i < _eventOffsetsPerTick.Count; i++)
		{
			if (index >= _eventOffsetsPerTick[i])
				continue; // Skip ticks that are before the event.

			// Remove the tick offset when removing an inputs event.
			if (!containingTick.HasValue && isInputsEvent)
			{
				_eventOffsetsPerTick.RemoveAt(i);
				containingTick = i;
			}

			// For every tick that is after the event, decrement the offset by 1.
			if (_eventOffsetsPerTick.Count > i && _eventOffsetsPerTick[i] > 0)
				_eventOffsetsPerTick[i]--;

			// If the tick offset is the same as the previous one, remove it.
			if (i > 0 && _eventOffsetsPerTick.Count > i && _eventOffsetsPerTick[i] == _eventOffsetsPerTick[i - 1])
				_eventOffsetsPerTick.RemoveAt(i);
		}
	}

	public void InsertEvent(int index, IEventData e)
	{
		if (index < 0 || index > _events.Count)
			throw new ArgumentOutOfRangeException(nameof(index));

		if (e is ISpawnEventData spawnEventData)
		{
			// Increment all entity IDs that are higher than the added entity ID.
			int entityId = 1; // Skip 0 as it is always reserved.
			for (int i = 0; i < _events.Count; i++)
			{
				if (i == index)
				{
					EntitySpawnReplayEvent spawnEvent = new(entityId, spawnEventData);
					_events.Insert(index, spawnEvent);
					_entitySpawnReplayEvents.Insert(entityId - 1, spawnEvent);
				}
				else if (_events[i] is EntitySpawnReplayEvent otherSpawnEvent)
				{
					if (i >= index)
						otherSpawnEvent.EntityId++;
					else
						entityId++;
				}

				if (i >= index)
				{
					IEventData data = _events[i].Data;
					UpdateReferringEntityIdsOnInsert(data, entityId);
				}
			}
		}
		else
		{
			_events.Insert(index, new(e));
		}

		int? containingTick = null;
		for (int i = 0; i < _eventOffsetsPerTick.Count; i++)
		{
			if (index >= _eventOffsetsPerTick[i])
				continue; // Skip ticks that are before the event.

			// The first tick that does not lie before the event is the tick that contains the event.
			// Add new tick if needed. This always means an input event was added.
			if (!containingTick.HasValue && e is InputsEventData or InitialInputsEventData)
			{
				int previousOffset = i > 0 ? _eventOffsetsPerTick[i - 1] : 0;
				_eventOffsetsPerTick.Insert(i, previousOffset);
				containingTick = i;
			}

			// For every tick that is after the event, increment the offset by 1.
			_eventOffsetsPerTick[i]++;
		}
	}

	/// <summary>
	/// Returns the entity type of the entity with the specified entity ID.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="entityId"/> is negative or higher than or equal to <see cref="SpawnEventCount"/>.</exception>
	public EntityType GetEntityType(int entityId)
	{
		if (entityId == 0)
			return EntityType.Zero;

		if (entityId < 0 || entityId >= _entitySpawnReplayEvents.Count + 1)
			throw new ArgumentOutOfRangeException(nameof(entityId));

		return _entitySpawnReplayEvents[entityId - 1].Data.EntityType;
	}

	/// <summary>
	/// When any event after the removed event is referring to an entity that also comes after the current removed entity id, decrement that entity id as well.
	/// In case any event refers to the removed event, set the entity id to -1.
	/// </summary>
	private static void UpdateReferringEntityIdsOnRemove(IEventData data, int removedEntityId)
	{
		if (data is BoidSpawnEventData boidSpawnEventData && boidSpawnEventData.SpawnerEntityId >= removedEntityId)
		{
			DecrementOrRemove(ref boidSpawnEventData.SpawnerEntityId, removedEntityId);
		}
		else if (data is EntityOrientationEventData entityOrientationEventData && entityOrientationEventData.EntityId >= removedEntityId)
		{
			DecrementOrRemove(ref entityOrientationEventData.EntityId, removedEntityId);
		}
		else if (data is EntityPositionEventData entityPositionEventData && entityPositionEventData.EntityId >= removedEntityId)
		{
			DecrementOrRemove(ref entityPositionEventData.EntityId, removedEntityId);
		}
		else if (data is EntityTargetEventData entityTargetEventData && entityTargetEventData.EntityId >= removedEntityId)
		{
			DecrementOrRemove(ref entityTargetEventData.EntityId, removedEntityId);
		}
		else if (data is HitEventData hitEventData)
		{
			if (hitEventData.EntityIdA >= removedEntityId)
				DecrementOrRemove(ref hitEventData.EntityIdA, removedEntityId);
			if (hitEventData.EntityIdB >= removedEntityId)
				DecrementOrRemove(ref hitEventData.EntityIdB, removedEntityId);
		}
		else if (data is SpiderEggSpawnEventData spiderEggSpawnEventData && spiderEggSpawnEventData.SpawnerEntityId >= removedEntityId)
		{
			DecrementOrRemove(ref spiderEggSpawnEventData.SpawnerEntityId, removedEntityId);
		}
		else if (data is TransmuteEventData transmuteEventData && transmuteEventData.EntityId >= removedEntityId)
		{
			DecrementOrRemove(ref transmuteEventData.EntityId, removedEntityId);
		}

		static void DecrementOrRemove(ref int entityId, int removedEntityId)
		{
			entityId = entityId == removedEntityId ? -1 : entityId - 1;
		}
	}

	/// <summary>
	/// When any event after the new event is referring to an entity that also comes after the current new entity id, increment that entity id as well.
	/// </summary>
	private static void UpdateReferringEntityIdsOnInsert(IEventData data, int insertedEntityId)
	{
		if (data is BoidSpawnEventData boidSpawnEventData && boidSpawnEventData.SpawnerEntityId >= insertedEntityId)
		{
			boidSpawnEventData.SpawnerEntityId++;
		}
		else if (data is EntityOrientationEventData entityOrientationEventData && entityOrientationEventData.EntityId >= insertedEntityId)
		{
			entityOrientationEventData.EntityId++;
		}
		else if (data is EntityPositionEventData entityPositionEventData && entityPositionEventData.EntityId >= insertedEntityId)
		{
			entityPositionEventData.EntityId++;
		}
		else if (data is EntityTargetEventData entityTargetEventData && entityTargetEventData.EntityId >= insertedEntityId)
		{
			entityTargetEventData.EntityId++;
		}
		else if (data is HitEventData hitEventData)
		{
			if (hitEventData.EntityIdA >= insertedEntityId)
				hitEventData.EntityIdA++;
			if (hitEventData.EntityIdB >= insertedEntityId)
				hitEventData.EntityIdB++;
		}
		else if (data is SpiderEggSpawnEventData spiderEggSpawnEventData && spiderEggSpawnEventData.SpawnerEntityId >= insertedEntityId)
		{
			spiderEggSpawnEventData.SpawnerEntityId++;
		}
		else if (data is TransmuteEventData transmuteEventData && transmuteEventData.EntityId >= insertedEntityId)
		{
			transmuteEventData.EntityId++;
		}
	}
}
