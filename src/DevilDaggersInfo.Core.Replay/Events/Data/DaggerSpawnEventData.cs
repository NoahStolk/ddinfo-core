using DevilDaggersInfo.Core.Replay.Events.Enums;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record DaggerSpawnEventData(int A, Int16Vec3 Position, Int16Mat3x3 Orientation, bool IsShot, DaggerType DaggerType) : ISpawnEventData
{
#pragma warning disable CA1051 // Visible instance fields.
	public int A = A;
	public Int16Vec3 Position = Position;
	public Int16Mat3x3 Orientation = Orientation;
	public bool IsShot = IsShot;
	public DaggerType DaggerType = DaggerType;
#pragma warning restore CA1051

	public EntityType EntityType => DaggerType switch
	{
		DaggerType.Level1 => EntityType.Level1Dagger,
		DaggerType.Level2 => EntityType.Level2Dagger,
		DaggerType.Level3 => EntityType.Level3Dagger,
		DaggerType.Level3Homing => EntityType.Level3HomingDagger,
		DaggerType.Level4 => EntityType.Level4Dagger,
		DaggerType.Level4Homing => EntityType.Level4HomingDagger,
		DaggerType.Level4HomingSplash => EntityType.Level4HomingSplash,
		_ => throw new UnreachableException(),
	};

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)0x01);

		bw.Write(A);
		bw.Write(Position);
		bw.Write(Orientation);
		bw.Write(IsShot);
		bw.Write((byte)DaggerType);
	}

	public static DaggerSpawnEventData CreateDefault()
	{
		return new(-1, Int16Vec3.Zero, Int16Mat3x3.Identity, false, DaggerType.Level1);
	}
}
