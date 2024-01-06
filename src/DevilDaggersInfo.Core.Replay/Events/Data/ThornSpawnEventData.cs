using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record ThornSpawnEventData(int A, Vector3 Position, float RotationInRadians) : ISpawnEventData
{
	public int A = A;
	public Vector3 Position = Position;
	public float RotationInRadians = RotationInRadians;

	public EntityType EntityType => EntityType.Thorn;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)0x0d);

		bw.Write(A);
		bw.Write(Position);
		bw.Write(RotationInRadians);
	}

	public static ThornSpawnEventData CreateDefault()
	{
		return new(-1, Vector3.Zero, 0f);
	}
}
