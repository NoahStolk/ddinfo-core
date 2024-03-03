namespace DevilDaggersInfo.Core.Common.Extensions;

[Obsolete("Use GameTime struct instead.")]
public static class TimeExtensions
{
	[Obsolete("Use GameTime struct instead.")]
	public static int To10thMilliTime(this float time)
	{
		return (int)(time * 10000.0);
	}

	[Obsolete("Use GameTime struct instead.")]
	public static int To10thMilliTime(this double time)
	{
		return (int)(time * 10000.0);
	}

	[Obsolete("Use GameTime struct instead.")]
	public static double ToSecondsTime(this int time)
	{
		return time * 0.0001;
	}

	[Obsolete("Use GameTime struct instead.")]
	public static double ToSecondsTime(this uint time)
	{
		return time * 0.0001;
	}

	[Obsolete("Use GameTime struct instead.")]
	public static double ToSecondsTime(this ulong time)
	{
		return time * 0.0001;
	}
}
