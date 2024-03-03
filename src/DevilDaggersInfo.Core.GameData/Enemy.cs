using DevilDaggersInfo.Core.Common;
using System.Collections.Frozen;

namespace DevilDaggersInfo.Core.GameData;

public class Enemy
{
	public Enemy(string name, int gems, int weakPointRepeatCount, int weakPointHealth, bool isTargetedByHomingDaggers, GameTime? firstSpawn, Rgb color, Death death)
	{
		Name = name;
		Gems = gems;
		WeakPointRepeatCount = weakPointRepeatCount;
		WeakPointHealth = weakPointHealth;
		IsTargetedByHomingDaggers = isTargetedByHomingDaggers;
		FirstSpawn = firstSpawn;
		Color = color;
		Death = death;
	}

	public string Name { get; }
	public int Gems { get; }
	public int WeakPointRepeatCount { get; }
	public int WeakPointHealth { get; }
	public bool IsTargetedByHomingDaggers { get; }
	public Enemy? TransmutesInto { get; private set; }
	public FrozenSet<Enemy>? SpawnedBy { get; private set; }
	// public EnemyInternalType InternalType { get; }
	// public string InternalName { get; }
	public GameTime? FirstSpawn { get; }
	public Rgb Color { get; }
	public Death Death { get; }

	internal void SetTransmuteInto(Enemy enemy)
	{
		TransmutesInto = enemy;
	}

	internal void SetSpawnedBy(Enemy enemy)
	{
		SpawnedBy = new[] { enemy }.ToFrozenSet();
	}

	internal void SetSpawnedBy(params Enemy[] enemies)
	{
		SpawnedBy = enemies.ToFrozenSet();
	}
}
