namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record DeathEventData(int DeathType) : IEventData
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
