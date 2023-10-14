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

		_events.RemoveAt(index);

		int? containingTick = null;
		for (int i = 0; i < _eventOffsetsPerTick.Count; i++)
		{
			if (index > _eventOffsetsPerTick[i])
				continue; // Skip ticks that are before the event.

			// The first tick that does not lie before the event is the tick that contains the event.
			containingTick ??= i;

			// For every tick that is after the event, decrement the offset by 1.
			_eventOffsetsPerTick[i]--;
		}

		// Remove empty tick if needed. This always means an input event was removed.
		if (containingTick.HasValue && _eventOffsetsPerTick[containingTick.Value] == 0)
		{
			_eventOffsetsPerTick.RemoveAt(containingTick.Value);
		}
	}

	public void InsertEvent(int index, IEvent e)
	{
		if (index < 0 || index > _events.Count)
			throw new ArgumentOutOfRangeException(nameof(index));

		_events.Insert(index, e);

		int? containingTick = null;
		for (int i = 0; i < _eventOffsetsPerTick.Count; i++)
		{
			if (index > _eventOffsetsPerTick[i])
				continue; // Skip ticks that are before the event.

			// The first tick that does not lie before the event is the tick that contains the event.
			// Add new tick if needed. This always means an input event was added.
			if (!containingTick.HasValue && e is InputsEvent)
			{
				_eventOffsetsPerTick.Insert(index, 0);
				containingTick = i;
			}

			// For every tick that is after the event, increment the offset by 1.
			_eventOffsetsPerTick[i]++;
		}
	}

	public void ChangeEntityType(int index, EntityType entityType)
	{
		if (index < 0 || index >= _entityTypes.Count)
			throw new ArgumentOutOfRangeException(nameof(index));

		_entityTypes[index] = entityType;
	}
}
