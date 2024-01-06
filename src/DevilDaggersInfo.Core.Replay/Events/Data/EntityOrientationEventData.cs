namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record EntityOrientationEventData(int EntityId, Int16Mat3x3 Orientation) : IEventData
{
	public int EntityId = EntityId;
	public Int16Mat3x3 Orientation = Orientation;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x02);
		bw.Write(EntityId);
		bw.Write(Orientation);
	}

	public static EntityOrientationEventData CreateDefault()
	{
		return new(IEventData.DefaultEntityId, Int16Mat3x3.Identity);
	}
}
