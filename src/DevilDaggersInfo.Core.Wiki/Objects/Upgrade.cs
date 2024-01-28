namespace DevilDaggersInfo.Core.Wiki.Objects;

public record Upgrade
{
	internal Upgrade(GameVersion gameVersion, string name, Color color, byte level, Damage defaultDamage, Damage? homingDamage, UpgradeUnlock upgradeUnlock)
	{
		GameVersion = gameVersion;
		Name = name;
		Color = color;
		Level = level;
		DefaultDamage = defaultDamage;
		HomingDamage = homingDamage;
		UpgradeUnlock = upgradeUnlock;
	}

	public GameVersion GameVersion { get; }
	public string Name { get; }
	public Color Color { get; }
	public byte Level { get; }
	public Damage DefaultDamage { get; }
	public Damage? HomingDamage { get; }
	public UpgradeUnlock UpgradeUnlock { get; }
}
