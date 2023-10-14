using DevilDaggersInfo.Core.Replay.Events.Interfaces;
using System.Runtime.InteropServices;

namespace DevilDaggersInfo.Core.Replay.Events;

[StructLayout(LayoutKind.Sequential)]
public record struct EntityPositionEvent(int EntityId, Int16Vec3 Position) : IEvent
{
	public int EntityId = EntityId;
	public Int16Vec3 Position = Position;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x01);
		bw.Write(EntityId);
		bw.Write(Position);
	}
}
