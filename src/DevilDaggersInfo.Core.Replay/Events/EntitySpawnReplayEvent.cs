using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.Events;

public record EntitySpawnReplayEvent(int EntityId, IEventData Data) : ReplayEvent(Data)
{
	public int EntityId { get; internal set; } = EntityId;
}
