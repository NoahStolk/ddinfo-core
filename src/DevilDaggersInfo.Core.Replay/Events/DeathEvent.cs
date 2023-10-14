using DevilDaggersInfo.Core.Replay.Events.Interfaces;

namespace DevilDaggersInfo.Core.Replay.Events;

public record DeathEvent(int DeathType) : IEvent
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
