namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record GemEvent : IEventData
{
	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x06);
	}
}
