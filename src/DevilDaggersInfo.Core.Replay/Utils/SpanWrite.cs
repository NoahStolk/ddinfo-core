namespace DevilDaggersInfo.Core.Replay.Utils;

internal static class SpanWrite
{
	public static bool TryWriteChar(Span<char> destination, ref int charsWritten, char value)
	{
		if (charsWritten + 1 > destination.Length)
			return false;

		destination[charsWritten++] = value;
		return true;
	}

	public static bool TryWriteString(Span<char> destination, ref int charsWritten, string value)
	{
		if (charsWritten + value.Length > destination.Length)
			return false;

		value.AsSpan().CopyTo(destination[charsWritten..]);
		charsWritten += value.Length;
		return true;
	}

	public static bool TryWriteByte(Span<byte> utf8Destination, ref int bytesWritten, byte value)
	{
		if (bytesWritten + 1 > utf8Destination.Length)
			return false;

		utf8Destination[bytesWritten++] = value;
		return true;
	}

	public static bool TryWriteBytes(Span<byte> utf8Destination, ref int bytesWritten, ReadOnlySpan<byte> value)
	{
		if (bytesWritten + value.Length > utf8Destination.Length)
			return false;

		value.CopyTo(utf8Destination[bytesWritten..]);
		bytesWritten += value.Length;
		return true;
	}

	public static bool TryWrite<T>(Span<char> destination, ref int charsWritten, T value, ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
		where T : ISpanFormattable
	{
		if (!value.TryFormat(destination[charsWritten..], out int charsWrittenValue, format, provider))
			return false;

		charsWritten += charsWrittenValue;
		return true;
	}

	public static bool TryWrite<T>(Span<byte> utf8Destination, ref int bytesWritten, T value, ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
		where T : IUtf8SpanFormattable
	{
		if (!value.TryFormat(utf8Destination[bytesWritten..], out int bytesWrittenValue, format, provider))
			return false;

		bytesWritten += bytesWrittenValue;
		return true;
	}
}
