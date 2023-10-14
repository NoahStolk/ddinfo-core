using DevilDaggersInfo.Core.Replay.Events.Interfaces;

namespace DevilDaggersInfo.Core.Replay.Events;

public record EndEvent : IEvent
{
	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x0b);
	}
}
