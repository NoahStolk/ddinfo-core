namespace DevilDaggersInfo.Core.GameData;

public interface IGameData
{
	string Name { get; }
	DateOnly ReleaseDate { get; }
	IReadOnlyList<Enemy> Enemies { get; }
	IReadOnlyList<Dagger> Daggers { get; }
	IReadOnlyList<Upgrade> Upgrades { get; }
	IReadOnlyList<UnlockDagger> UnlockDaggers { get; }
	IReadOnlyList<Damage> Damages { get; }
	IReadOnlyList<Death> Deaths { get; }

	public Damage? GetDamage(Dagger dagger, Enemy enemy)
	{
		for (int i = 0; i < Damages.Count; i++)
		{
			Damage damage = Damages[i];
			if (damage.Dagger == dagger && damage.Enemy == enemy)
				return damage;
		}

		return null;
	}
}
