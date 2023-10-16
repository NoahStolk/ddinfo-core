namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record GemEventData : IEventData
{
	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x06);
	}
}
