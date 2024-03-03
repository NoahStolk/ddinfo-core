namespace DevilDaggersInfo.Core.GameData;

public class Damage
{
	internal Damage(Dagger dagger, Enemy enemy, float daggerDepletionPercentage, int enemyDamage)
	{
		Dagger = dagger;
		Enemy = enemy;
		DaggerDepletionPercentage = daggerDepletionPercentage;
		EnemyDamage = enemyDamage;
	}

	public Dagger Dagger { get; }
	public Enemy Enemy { get; }
	public float DaggerDepletionPercentage { get; } // TODO: Not sure if this makes sense for splash.
	public int EnemyDamage { get; }
}
