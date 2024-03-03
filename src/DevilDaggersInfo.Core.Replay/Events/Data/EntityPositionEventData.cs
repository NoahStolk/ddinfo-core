namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record EntityPositionEventData(int EntityId, Int16Vec3 Position) : IEventData
{
#pragma warning disable CA1051 // Visible instance fields.
	public int EntityId = EntityId;
	public Int16Vec3 Position = Position;
#pragma warning restore CA1051

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
