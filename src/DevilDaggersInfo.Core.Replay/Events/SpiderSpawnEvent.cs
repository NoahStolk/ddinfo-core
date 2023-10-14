using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Replay.Events.Interfaces;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DevilDaggersInfo.Core.Replay.Events;

[StructLayout(LayoutKind.Sequential)]
public record struct SpiderSpawnEvent(int EntityId, SpiderType SpiderType, int A, Vector3 Position) : IEntitySpawnEvent
{
	public SpiderType SpiderType = SpiderType;
	public int A = A;
	public Vector3 Position = Position;

	public EntityType EntityType => SpiderType switch
	{
		SpiderType.Spider1 => EntityType.Spider1,
		SpiderType.Spider2 => EntityType.Spider2,
		_ => throw new UnreachableException(),
	};

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)(SpiderType switch
		{
			SpiderType.Spider1 => 0x08,
			SpiderType.Spider2 => 0x09,
			_ => throw new UnreachableException(),
		}));

		bw.Write(A);
		bw.Write(Position);
	}
}
