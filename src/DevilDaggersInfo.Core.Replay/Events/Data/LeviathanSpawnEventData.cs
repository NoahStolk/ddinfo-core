using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record LeviathanSpawnEventData(int A) : ISpawnEventData
{
	public int A = A;

	public EntityType EntityType => EntityType.Leviathan;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)0x0b);

		bw.Write(A);
	}

	public static LeviathanSpawnEventData CreateDefault()
	{
		return new(-1);
	}
}
