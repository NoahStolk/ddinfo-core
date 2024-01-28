namespace DevilDaggersInfo.Core.Wiki.Objects;

public record Death
{
	internal Death(GameVersion gameVersion, string name, Color color, byte leaderboardDeathType)
	{
		GameVersion = gameVersion;
		Name = name;
		Color = color;
		LeaderboardDeathType = leaderboardDeathType;
	}

	public GameVersion GameVersion { get; }
	public string Name { get; }
	public Color Color { get; }
	public byte LeaderboardDeathType { get; }
}
