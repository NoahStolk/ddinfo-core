using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Replay.Events.Interfaces;
using System.Runtime.InteropServices;

namespace DevilDaggersInfo.Core.Replay.Events;

[StructLayout(LayoutKind.Sequential)]
public record struct SpiderEggSpawnEvent(int EntityId, int SpawnerEntityId, Vector3 Position, Vector3 TargetPosition) : IEntitySpawnEvent
{
	public int SpawnerEntityId = SpawnerEntityId;
	public Vector3 Position = Position;
	public Vector3 TargetPosition = TargetPosition;

	public EntityType EntityType => EntityType.SpiderEgg;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)0x0a);

		bw.Write(SpawnerEntityId);
		bw.Write(Position);
		bw.Write(TargetPosition);
	}
}
