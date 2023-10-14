using DevilDaggersInfo.Core.Replay.Events.Interfaces;
using System.Runtime.InteropServices;

namespace DevilDaggersInfo.Core.Replay.Events;

[StructLayout(LayoutKind.Sequential)]
public record struct EntityTargetEvent(int EntityId, Int16Vec3 TargetPosition) : IEvent
{
	public int EntityId = EntityId;
	public Int16Vec3 TargetPosition = TargetPosition;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x04);
		bw.Write(EntityId);
		bw.Write(TargetPosition);
	}
}
