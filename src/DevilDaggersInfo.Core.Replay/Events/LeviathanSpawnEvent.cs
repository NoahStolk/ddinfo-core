using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Replay.Events.Interfaces;

namespace DevilDaggersInfo.Core.Replay.Events;

public record LeviathanSpawnEvent(int EntityId, int A) : IEntitySpawnEvent
{
	public int A = A;

	public EntityType EntityType => EntityType.Leviathan;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)0x0b);

		bw.Write(A);
	}
}
