namespace DevilDaggersInfo.Core.Replay.Events.Data;

public sealed record EndEventData : IEventData
{
	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x0b);
	}

	public static EndEventData CreateDefault()
	{
		return new EndEventData();
	}
}
