using DevilDaggersInfo.Core.Replay.Events.Interfaces;

namespace DevilDaggersInfo.Core.Replay.Events;

public record HitEvent(int EntityIdA, int EntityIdB, int UserData) : IEvent
{
	public int EntityIdA = EntityIdA;
	public int EntityIdB = EntityIdB;
	public int UserData = UserData;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x05);
		bw.Write(EntityIdA);
		bw.Write(EntityIdB);
		bw.Write(UserData);
	}
}
