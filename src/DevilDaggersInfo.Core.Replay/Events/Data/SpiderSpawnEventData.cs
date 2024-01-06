using DevilDaggersInfo.Core.Replay.Events.Enums;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Replay.Events.Data;

public record SpiderSpawnEventData(SpiderType SpiderType, int A, Vector3 Position) : ISpawnEventData
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

	public static SpiderSpawnEventData CreateDefault()
	{
		return new(SpiderType.Spider1, -1, Vector3.Zero);
	}
}
