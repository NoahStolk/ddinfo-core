using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay;

/// <summary>
/// Represents all the events in a replay.
/// <remarks>
/// IMPORTANT: This API is unfinished and will change in the future. Right now, the class generally lets you corrupt its state for the sake of performance and ease of use. This will be solved in the future.
/// <list type="bullet">
/// <item><description>When changing the internal type of a spawn event, be sure to also update the list of entity types using <see cref="ChangeEntityType(int, EntityType)"/>.</description></item>
/// <item><description>When adding or inserting a spawn event, the entity ID is re-calculated and overwritten. This will be changed in the future.</description></item>
/// <item><description>The event types currently let you change their ID, but this should only ever be done by the <see cref="ReplayEventsData"/> class itself. This will be removed in the future.</description></item>
/// </list>
/// </remarks>
/// </summary>
// TODO: Rewrite:
// We should rewrite the event classes to be mutable structs and exclude EntityId and EntityType from them, then add a new wrapper class containing EntityId, EntityType, and TEventStruct as properties instead. This fixes point 2 above.
// The wrapper class should have an internal set for EntityId. This fixes point 3 above.
// Point 1 above could be solved by referencing to all Spawn events (instances of the wrapper class) from the _events list, instead of keeping a list of EntityType enums.
public class ReplayEventsData
{
	private readonly List<ReplayEvent> _events = new();
	private readonly List<int> _eventOffsetsPerTick = new() { 0, 0 };
	private readonly List<EntityType> _entityTypes = new() { EntityType.Zero };

	public IReadOnlyList<ReplayEvent> Events => _events;

	public IReadOnlyList<int> EventOffsetsPerTick => _eventOffsetsPerTick;

	public IReadOnlyList<EntityType> EntityTypes => _entityTypes;

	public int TickCount => EventOffsetsPerTick.Count - 1;

	public void Clear()
	{
		_events.Clear();

		_eventOffsetsPerTick.Clear();
		_eventOffsetsPerTick.Add(0);
		_eventOffsetsPerTick.Add(0);

		_entityTypes.Clear();
		_entityTypes.Add(EntityType.Zero);
	}

	public void AddEvent(IEventData e)
	{
		if (e is ISpawnEventData spawnEvent)
			_events.Add(new EntitySpawnReplayEvent(_entityTypes.Count, spawnEvent.EntityType, e));
		else
			_events.Add(new(e));

		_eventOffsetsPerTick[^1]++;

		if (e is InputsEvent or InitialInputsEvent)
			_eventOffsetsPerTick.Add(_events.Count);
		else if (e is ISpawnEventData ese)
			_entityTypes.Add(ese.EntityType);
	}

	public void RemoveEvent(int index)
	{
		if (index < 0 || index >= _events.Count)
			throw new ArgumentOutOfRangeException(nameof(index));

		ReplayEvent e = _events[index];
		if (e is EntitySpawnReplayEvent spawnEvent)
		{
			_entityTypes.RemoveAt(spawnEvent.EntityId);

			// Decrement all entity IDs that are higher than the removed entity ID.
			for (int i = 0; i < _events.Count; i++)
			{
				if (i == index)
					continue;

				if (_events[i] is EntitySpawnReplayEvent otherSpawnEvent && otherSpawnEvent.EntityId > spawnEvent.EntityId)
					otherSpawnEvent.EntityId--;
			}
		}

		_events.Remove(e);

		int? containingTick = null;
		bool isInputsEvent = e.Data is InputsEvent;
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

		if (e is ISpawnEventData spawnEvent)
		{
			// Increment all entity IDs that are higher than the added entity ID.
			int entityId = 1; // Skip 0 as it is always reserved.
			for (int i = 0; i < _events.Count; i++)
			{
				if (i == index)
				{
					_events.Insert(index, new EntitySpawnReplayEvent(entityId, spawnEvent.EntityType, e));
					_entityTypes.Insert(entityId, spawnEvent.EntityType);
				}
				else if (_events[i] is EntitySpawnReplayEvent otherSpawnEvent)
				{
					if (i >= index)
						otherSpawnEvent.EntityId++;
					else
						entityId++;
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
			if (!containingTick.HasValue && e is InputsEvent)
			{
				int previousOffset = i > 0 ? _eventOffsetsPerTick[i - 1] : 0;
				_eventOffsetsPerTick.Insert(i, previousOffset);
				containingTick = i;
			}

			// For every tick that is after the event, increment the offset by 1.
			_eventOffsetsPerTick[i]++;
		}
	}

	public void ChangeEntityType(int entityId, EntityType entityType)
	{
		if (entityId < 0 || entityId >= _entityTypes.Count)
			throw new ArgumentOutOfRangeException(nameof(entityId));

		_entityTypes[entityId] = entityType;
	}
}
