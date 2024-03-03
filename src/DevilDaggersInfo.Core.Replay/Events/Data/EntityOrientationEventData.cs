namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record EntityOrientationEventData(int EntityId, Int16Mat3x3 Orientation) : IEventData
{
#pragma warning disable CA1051 // Visible instance fields.
	public int EntityId = EntityId;
	public Int16Mat3x3 Orientation = Orientation;
#pragma warning restore CA1051

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
