namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record HitEventData(int EntityIdA, int EntityIdB, int UserData) : IEventData
{
#pragma warning disable CA1051 // Visible instance fields.
	public int EntityIdA = EntityIdA;
	public int EntityIdB = EntityIdB;
	public int UserData = UserData;
#pragma warning restore CA1051

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x05);
		bw.Write(EntityIdA);
		bw.Write(EntityIdB);
		bw.Write(UserData);
	}

	public static HitEventData CreateDefault()
	{
		return new(IEventData.DefaultEntityId, IEventData.DefaultEntityId, 0);
	}
}
