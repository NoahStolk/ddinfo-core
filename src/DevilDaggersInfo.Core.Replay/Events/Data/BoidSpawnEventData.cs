using DevilDaggersInfo.Core.Replay.Events.Enums;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record BoidSpawnEventData(int SpawnerEntityId, BoidType BoidType, Int16Vec3 Position, Int16Mat3x3 Orientation, Vector3 Velocity, float Speed) : ISpawnEventData
{
#pragma warning disable CA1051 // Visible instance fields.
	public int SpawnerEntityId = SpawnerEntityId;
	public BoidType BoidType = BoidType;
	public Int16Vec3 Position = Position;
	public Int16Mat3x3 Orientation = Orientation;
	public Vector3 Velocity = Velocity;
	public float Speed = Speed;
#pragma warning restore CA1051

	public EntityType EntityType => BoidType switch
	{
		BoidType.Skull1 => EntityType.Skull1,
		BoidType.Skull2 => EntityType.Skull2,
		BoidType.Skull3 => EntityType.Skull3,
		BoidType.Spiderling => EntityType.Spiderling,
		BoidType.Skull4 => EntityType.Skull4,
		_ => throw new UnreachableException(),
	};

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)0x06);

		bw.Write(SpawnerEntityId);
		bw.Write((byte)(BoidType switch
		{
			BoidType.Skull1 => 0x01,
			BoidType.Skull2 => 0x02,
			BoidType.Skull3 => 0x03,
			BoidType.Spiderling => 0x04,
			BoidType.Skull4 => 0x05,
			_ => throw new UnreachableException(),
		}));
		bw.Write(Position);
		bw.Write(Orientation);
		bw.Write(Velocity);
		bw.Write(Speed);
	}

	public static BoidSpawnEventData CreateDefault()
	{
		return new(IEventData.DefaultEntityId, BoidType.Skull1, Int16Vec3.Zero, Int16Mat3x3.Identity, Vector3.Zero, 0f);
	}
}
