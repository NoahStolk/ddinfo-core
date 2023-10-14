using DevilDaggersInfo.Core.Replay.Events.Interfaces;
using System.Runtime.InteropServices;

namespace DevilDaggersInfo.Core.Replay.Events;

[StructLayout(LayoutKind.Sequential)]
public record struct DeathEvent(int DeathType) : IEvent
{
	public int DeathType = DeathType;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x05);
		bw.Write(0);
		bw.Write(DeathType);
		bw.Write(0);
	}
}
