using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Replay.Events.Interfaces;

namespace DevilDaggersInfo.Core.Replay;

public class ReplayEventsData
{
	private readonly List<IEvent> _events = new();
	private readonly List<int> _eventOffsetsPerTick = new() { 0, 0 };
	private readonly List<EntityType> _entityTypes = new() { EntityType.Zero };

	public IReadOnlyList<IEvent> Events => _events;

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

	public void AddEvent(IEvent e)
	{
		_events.Add(e);
		_eventOffsetsPerTick[^1]++;

		if (e is InputsEvent or InitialInputsEvent)
			_eventOffsetsPerTick.Add(_events.Count);
		else if (e is IEntitySpawnEvent ese)
			_entityTypes.Add(ese.EntityType);
	}

	public void RemoveEvent(int index)
	{
		if (index < 0 || index >= _events.Count)
			throw new ArgumentOutOfRangeException(nameof(index));

		IEvent e = _events[index];
		if (e is IEntitySpawnEvent spawnEvent)
		{
			_entityTypes.RemoveAt(spawnEvent.EntityId);

			// Decrement all entity IDs that are higher than the removed entity ID.
			for (int i = 0; i < _events.Count; i++)
			{
				if (i == index)
					continue;

				if (_events[i] is IEntitySpawnEvent otherSpawnEvent && otherSpawnEvent.EntityId > spawnEvent.EntityId)
					otherSpawnEvent.EntityId--;
			}
		}

		_events.Remove(e);

		int? containingTick = null;
		bool isInputsEvent = e is InputsEvent;
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

	public void InsertEvent(int index, IEvent e)
	{
		if (index < 0 || index > _events.Count)
			throw new ArgumentOutOfRangeException(nameof(index));

		_events.Insert(index, e);
		if (e is IEntitySpawnEvent spawnEvent)
		{
			// TODO: EntityId should be automatically calculated and not taken from the event.
			_entityTypes.Insert(spawnEvent.EntityId, spawnEvent.EntityType);

			// Increment all entity IDs that are higher than the added entity ID.
			for (int i = 0; i < _events.Count; i++)
			{
				if (i == index)
					continue;

				if (_events[i] is IEntitySpawnEvent otherSpawnEvent && otherSpawnEvent.EntityId >= spawnEvent.EntityId)
					otherSpawnEvent.EntityId++;
			}
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
