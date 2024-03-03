using System.Diagnostics;

namespace DevilDaggersInfo.Core.Common;

[DebuggerDisplay("{Seconds} seconds ({_gameUnits} game units)")]
public readonly struct GameTime : IEquatable<GameTime>
{
	private const int _gameUnitsPerSecond = 10_000;

	/// <summary>
	/// There are 10,000 game units per second.
	/// </summary>
	private readonly int _gameUnits;

	public GameTime(int seconds)
	{
		_gameUnits = seconds * _gameUnitsPerSecond;
	}

	public GameTime(double seconds)
	{
		_gameUnits = (int)(seconds * _gameUnitsPerSecond);
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
}
