namespace DevilDaggersInfo.Core.Wiki.Objects;

public sealed record Dagger
{
	internal Dagger(GameVersion gameVersion, string name, Color color, int unlockSecond)
	{
		GameVersion = gameVersion;
		Name = name;
		Color = color;
		UnlockSecond = unlockSecond;
	}

	public GameVersion GameVersion { get; }
	public string Name { get; }
	public Color Color { get; }
	public int UnlockSecond { get; }
}
