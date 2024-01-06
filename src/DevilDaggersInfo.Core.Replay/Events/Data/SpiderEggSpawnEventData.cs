using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record SpiderEggSpawnEventData(int SpawnerEntityId, Vector3 Position, Vector3 TargetPosition) : ISpawnEventData
{
	public int SpawnerEntityId = SpawnerEntityId;
	public Vector3 Position = Position;
	public Vector3 TargetPosition = TargetPosition;

	public EntityType EntityType => EntityType.SpiderEgg;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)0x0a);

		bw.Write(SpawnerEntityId);
		bw.Write(Position);
		bw.Write(TargetPosition);
	}

	public static SpiderEggSpawnEventData CreateDefault()
	{
		return new(IEventData.DefaultEntityId, Vector3.Zero, Vector3.Zero);
	}
}
