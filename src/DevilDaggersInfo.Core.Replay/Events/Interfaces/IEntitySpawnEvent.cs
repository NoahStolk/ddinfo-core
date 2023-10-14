using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.Events.Interfaces;

public interface IEntitySpawnEvent : IEvent
{
	int EntityId { get; internal set; }

	EntityType EntityType { get; }
}
