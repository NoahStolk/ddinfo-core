using DevilDaggersInfo.Core.Common;
using System.Collections.Frozen;

namespace DevilDaggersInfo.Core.GameData;

public class Enemy
{
	public required string Name { get; init; }

	public required int Gems { get; init; }

	public required int WeakPointRepeatCount { get; init; }

	public required int WeakPointHealth { get; init; }

	public required bool IsTargetedByHomingDaggers { get; init; }

	public required Enemy? TransmutesInto { get; init; }

	public required FrozenSet<Enemy> SpawnedBy { get; init; }

	public required EnemyInternalType InternalType { get; init; }

	public required string InternalName { get; init; }

	public required GameTime? FirstSpawn { get; init; }

	public required Rgb Color { get; init; }

	public required Death Death { get; init; }
}
