using DevilDaggersInfo.Core.Common;

namespace DevilDaggersInfo.Core.GameData;

public class UnlockDagger
{
	public UnlockDagger(string name, Rgb color, GameTime unlocksAt)
	{
		Name = name;
		Color = color;
		UnlocksAt = unlocksAt;
	}

	public string Name { get; }
	public Rgb Color { get; }
	public GameTime UnlocksAt { get; }
}
