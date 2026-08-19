namespace DevilDaggersInfo.Core.Spawnset;

public readonly record struct Spawn(EnemyType EnemyType, float Delay)
{
	public override string ToString()
	{
		return $"{Delay:0.0000}: {EnemyType}";
	}
}
