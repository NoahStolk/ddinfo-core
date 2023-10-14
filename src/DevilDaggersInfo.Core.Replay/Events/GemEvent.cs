using DevilDaggersInfo.Core.Replay.Events.Interfaces;

namespace DevilDaggersInfo.Core.Replay.Events;

public record GemEvent : IEvent
{
	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x06);
	}
}
