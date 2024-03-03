using System.Diagnostics;

namespace DevilDaggersInfo.Core.Common;

[DebuggerDisplay("{Seconds} seconds ({_gameUnits} game units)")]
public readonly struct GameTime : IEquatable<GameTime>
{
	private const int _gameUnitsPerSecond = 10_000;

	/// <summary>
	/// There are 10,000 game units per second.
	/// </summary>
	private readonly long _gameUnits;

	private GameTime(long gameUnits)
	{
		_gameUnits = gameUnits;
	}

	public double Seconds => _gameUnits / (double)_gameUnitsPerSecond;

	public static bool operator ==(GameTime left, GameTime right)
	{
		return left._gameUnits == right._gameUnits;
	}

	public static bool operator !=(GameTime left, GameTime right)
	{
		return !(left == right);
	}

	public override bool Equals(object? obj)
	{
		return obj is GameTime gameTime && Equals(gameTime);
	}

	public bool Equals(GameTime other)
	{
		return _gameUnits == other._gameUnits;
	}

	public override int GetHashCode()
	{
		return _gameUnits.GetHashCode();
	}

	public static GameTime FromSeconds(int seconds)
	{
		return new(seconds * _gameUnitsPerSecond);
	}

	public static GameTime FromSeconds(double seconds)
	{
		return new((long)(seconds * _gameUnitsPerSecond));
	}

	public static GameTime FromGameUnits(int gameUnits)
	{
		return new(gameUnits);
	}

	public static GameTime FromGameUnits(long gameUnits)
	{
		return new(gameUnits);
	}

	public static GameTime FromGameUnits(ulong gameUnits)
	{
		if (gameUnits > long.MaxValue)
			throw new ArgumentOutOfRangeException(nameof(gameUnits), "Value is too large to fit in a long.");

		return new((long)gameUnits);
	}
}
