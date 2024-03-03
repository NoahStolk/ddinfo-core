using DevilDaggersInfo.Core.Common;

namespace DevilDaggersInfo.Core.GameData;

public class UnlockDagger
{
	public required string Name { get; init; }

	public required Rgb Color { get; init; }

	public required GameTime UnlocksAt { get; init; }
}
