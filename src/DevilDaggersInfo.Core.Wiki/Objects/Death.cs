using System.Numerics;

namespace DevilDaggersInfo.Core.Wiki.Objects;

public sealed record Death
{
	internal Death(GameVersion gameVersion, string name, Vector3 color, byte leaderboardDeathType)
	{
		GameVersion = gameVersion;
		Name = name;
		Color = color;
		LeaderboardDeathType = leaderboardDeathType;
	}

	public GameVersion GameVersion { get; }
	public string Name { get; }
	public Vector3 Color { get; }
	public byte LeaderboardDeathType { get; }
}
