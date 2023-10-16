using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.Events.Data;

public interface ISpawnEventData : IEventData
{
	EntityType EntityType { get; }
}
