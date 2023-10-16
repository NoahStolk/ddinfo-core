namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record EndEvent : IEventData
{
	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x0b);
	}
}
