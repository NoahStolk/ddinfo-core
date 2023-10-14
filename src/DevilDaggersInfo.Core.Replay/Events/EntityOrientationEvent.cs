using DevilDaggersInfo.Core.Replay.Events.Interfaces;
using System.Runtime.InteropServices;

namespace DevilDaggersInfo.Core.Replay.Events;

[StructLayout(LayoutKind.Sequential)]
public record struct EntityOrientationEvent(int EntityId, Int16Mat3x3 Orientation) : IEvent
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
