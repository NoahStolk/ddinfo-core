namespace DevilDaggersInfo.Core.Wiki.Objects;

public record Enemy
{
	internal Enemy(GameVersion gameVersion, string name, Color color, int hp, int gems, int noFarmGems, Death death, HomingDamage homingDamage, int? firstSpawnSecond, params Enemy[] spawnedBy)
	{
		GameVersion = gameVersion;
		Name = name;
		Color = color;
		Hp = hp;
		Gems = gems;
		NoFarmGems = noFarmGems;
		Death = death;
		HomingDamage = homingDamage;
		FirstSpawnSecond = firstSpawnSecond;
		SpawnedBy = spawnedBy;
	}

	public GameVersion GameVersion { get; }
	public string Name { get; }
	public Color Color { get; }
	public int Hp { get; }
	public int Gems { get; }
	public int NoFarmGems { get; }
	public Death Death { get; }
	public HomingDamage HomingDamage { get; }
	public int? FirstSpawnSecond { get; }
	public Enemy[] SpawnedBy { get; }

	public int GemHp => Hp / Gems;

	public string GetGemHpString()
		=> $"({GemHp} x {Gems})";

	public string GetImageName()
	{
		return Name
			.Replace(" IV", "-4")
			.Replace(" III", "-3")
			.Replace(" II", "-2")
			.Replace(" I", "-1")
			.Replace(' ', '-')
			.ToLower();
	}
}
