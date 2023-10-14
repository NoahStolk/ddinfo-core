using DevilDaggersInfo.Core.Replay.Events.Interfaces;

namespace DevilDaggersInfo.Core.Replay.Events;

public record EntityOrientationEvent(int EntityId, Int16Mat3x3 Orientation) : IEvent
{
	public int EntityId = EntityId;
	public Int16Mat3x3 Orientation = Orientation;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x02);
		bw.Write(EntityId);
		bw.Write(Orientation);
	}
}
