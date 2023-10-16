using DevilDaggersInfo.Core.Replay.Events.Enums;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record SquidSpawnEventData(SquidType SquidType, int A, Vector3 Position, Vector3 Direction, float RotationInRadians) : ISpawnEventData
{
	public SquidType SquidType = SquidType;
	public int A = A;
	public Vector3 Position = Position;
	public Vector3 Direction = Direction;
	public float RotationInRadians = RotationInRadians;

	public EntityType EntityType => SquidType switch
	{
		SquidType.Squid1 => EntityType.Squid1,
		SquidType.Squid2 => EntityType.Squid2,
		SquidType.Squid3 => EntityType.Squid3,
		_ => throw new UnreachableException(),
	};

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)(SquidType switch
		{
			SquidType.Squid1 => 0x03,
			SquidType.Squid2 => 0x04,
			SquidType.Squid3 => 0x05,
			_ => throw new UnreachableException(),
		}));

		bw.Write(A);
		bw.Write(Position);
		bw.Write(Direction);
		bw.Write(RotationInRadians);
	}
}
