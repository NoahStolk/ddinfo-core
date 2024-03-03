using DevilDaggersInfo.Core.Replay.Events.Enums;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record SquidSpawnEventData(SquidType SquidType, int A, Vector3 Position, Vector3 Direction, float RotationInRadians) : ISpawnEventData
{
#pragma warning disable CA1051 // Visible instance fields.
	public SquidType SquidType = SquidType;
	public int A = A;
	public Vector3 Position = Position;
	public Vector3 Direction = Direction;
	public float RotationInRadians = RotationInRadians;
#pragma warning restore CA1051

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

	public static SquidSpawnEventData CreateDefault()
	{
		return new(SquidType.Squid1, -1, Vector3.Zero, Vector3.Zero, 0f);
	}
}
