using System.Numerics;

namespace DevilDaggersInfo.Core.Wiki.Objects;

public sealed record Dagger
{
	internal Dagger(GameVersion gameVersion, string name, Vector3 color, int unlockSecond)
	{
		GameVersion = gameVersion;
		Name = name;
		Color = color;
		UnlockSecond = unlockSecond;
	}

	public GameVersion GameVersion { get; }
	public string Name { get; }
	public Vector3 Color { get; }
	public int UnlockSecond { get; }
}
