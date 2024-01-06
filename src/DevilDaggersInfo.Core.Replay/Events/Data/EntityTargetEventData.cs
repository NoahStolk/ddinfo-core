namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record EntityTargetEventData(int EntityId, Int16Vec3 TargetPosition) : IEventData
{
	public int EntityId = EntityId;
	public Int16Vec3 TargetPosition = TargetPosition;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x04);
		bw.Write(EntityId);
		bw.Write(TargetPosition);
	}

	public static EntityTargetEventData CreateDefault()
	{
		return new(IEventData.DefaultEntityId, Int16Vec3.Zero);
	}
}
