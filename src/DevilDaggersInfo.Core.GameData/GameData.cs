using System.Collections.Frozen;

namespace DevilDaggersInfo.Core.GameData;

public class GameData
{
	public required string Name { get; init; }

	public required DateOnly ReleaseDate { get; init; }

	public required FrozenSet<Enemy> Enemies { get; init; }

	public required FrozenSet<Dagger> Daggers { get; init; }

	public required FrozenSet<Upgrade> Upgrades { get; init; }

	public required FrozenSet<UnlockDagger> UnlockDaggers { get; init; }

	public required FrozenSet<Damage> Damages { get; init; }

	public required FrozenSet<Death> Deaths { get; init; }
}
