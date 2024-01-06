namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record EntityPositionEventData(int EntityId, Int16Vec3 Position) : IEventData
{
	public int EntityId = EntityId;
	public Int16Vec3 Position = Position;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x01);
		bw.Write(EntityId);
		bw.Write(Position);
	}

	public static EntityPositionEventData CreateDefault()
	{
		return new(IEventData.DefaultEntityId, Int16Vec3.Zero);
	}
}
