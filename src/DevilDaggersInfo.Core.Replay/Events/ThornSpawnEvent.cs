using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Replay.Events.Interfaces;

namespace DevilDaggersInfo.Core.Replay.Events;

public record ThornSpawnEvent(int EntityId, int A, Vector3 Position, float RotationInRadians) : IEntitySpawnEvent
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
}
