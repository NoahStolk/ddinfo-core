using DevilDaggersInfo.Core.Replay.Events.Data;

namespace DevilDaggersInfo.Core.Replay.Events;

public record EntitySpawnReplayEvent : ReplayEvent
{
	internal EntitySpawnReplayEvent(int entityId, ISpawnEventData data)
		: base(data)
	{
		EntityId = entityId;
		Data = data;
	}

	public int EntityId { get; internal set; }
	public new ISpawnEventData Data { get; }
}
