using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay;

/// <summary>
/// Represents all the events in a replay.
/// </summary>
public class ReplayEventsData
{
	private readonly List<ReplayEvent> _events = [];

	public IReadOnlyList<ReplayEvent> Events => _events;

	internal void AddEvent(IEventData e)
	{
		_events.Add(new(e));
	}
}
