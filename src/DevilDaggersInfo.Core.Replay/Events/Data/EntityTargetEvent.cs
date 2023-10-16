namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record EntityTargetEvent(int EntityId, Int16Vec3 TargetPosition) : IEventData
{
	public int EntityId = EntityId;
	public Int16Vec3 TargetPosition = TargetPosition;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x04);
		bw.Write(EntityId);
		bw.Write(TargetPosition);
	}
}
