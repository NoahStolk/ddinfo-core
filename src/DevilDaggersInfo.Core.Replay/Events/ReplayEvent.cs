using DevilDaggersInfo.Core.Replay.Events.Data;

namespace DevilDaggersInfo.Core.Replay.Events;

public sealed record ReplayEvent
{
	public ReplayEvent(IEventData data)
	{
		Data = data;
	}

	public IEventData Data { get; }
}
