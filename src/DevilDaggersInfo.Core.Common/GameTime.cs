using System.Diagnostics;

namespace DevilDaggersInfo.Core.Common;

[DebuggerDisplay("{Seconds} seconds ({GameUnits} game units)")]
public readonly struct GameTime : IEquatable<GameTime>
{
	private const int _gameUnitsPerSecond = 10_000;

	private GameTime(long gameUnits)
	{
		GameUnits = gameUnits;
	}

	public long GameUnits { get; }

	public double Seconds => GameUnits / (double)_gameUnitsPerSecond;

	public static GameTime operator +(GameTime left, GameTime right)
	{
		return new GameTime(left.GameUnits + right.GameUnits);
	}

	public static GameTime operator -(GameTime left, GameTime right)
	{
		return new GameTime(left.GameUnits - right.GameUnits);
	}

	public static GameTime operator *(GameTime left, GameTime right)
	{
		return new GameTime(left.GameUnits * right.GameUnits);
	}

	public static GameTime operator /(GameTime left, GameTime right)
	{
		return new GameTime(left.GameUnits / right.GameUnits);
	}

	public static GameTime operator %(GameTime left, GameTime right)
	{
		return new GameTime(left.GameUnits % right.GameUnits);
	}

	public static bool operator <(GameTime left, GameTime right)
	{
		return left.GameUnits < right.GameUnits;
	}

	public static bool operator <=(GameTime left, GameTime right)
	{
		return left.GameUnits <= right.GameUnits;
	}

	public static bool operator >(GameTime left, GameTime right)
	{
		return left.GameUnits > right.GameUnits;
	}

	public static bool operator >=(GameTime left, GameTime right)
	{
		return left.GameUnits >= right.GameUnits;
	}

	public static bool operator ==(GameTime left, GameTime right)
	{
		return left.GameUnits == right.GameUnits;
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
		return GameUnits == other.GameUnits;
	}

	public override int GetHashCode()
	{
		return GameUnits.GetHashCode();
	}

	public static GameTime FromSeconds(int seconds)
	{
		return new GameTime(seconds * _gameUnitsPerSecond);
	}

	public static GameTime FromSeconds(double seconds)
	{
		return new GameTime((long)(seconds * _gameUnitsPerSecond));
	}

	public static GameTime FromGameUnits(int gameUnits)
	{
		return new GameTime(gameUnits);
	}

	public static GameTime FromGameUnits(long gameUnits)
	{
		return new GameTime(gameUnits);
	}

	public static GameTime FromGameUnits(ulong gameUnits)
	{
		if (gameUnits > long.MaxValue)
			throw new ArgumentOutOfRangeException(nameof(gameUnits), "Value is too large to fit in a long.");

		return new GameTime((long)gameUnits);
	}
}
