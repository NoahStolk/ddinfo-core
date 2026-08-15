using DevilDaggersInfo.Core.Replay.Utils;
using System.Runtime.InteropServices;

namespace DevilDaggersInfo.Core.Replay.Numerics;

[StructLayout(LayoutKind.Sequential)]
public record struct Int16Vec3(short X, short Y, short Z)
	: ISpanFormattable, IUtf8SpanFormattable
{
	public short X = X;
	public short Y = Y;
	public short Z = Z;

	public static Int16Vec3 Zero { get; } = new(0, 0, 0);

	public static Int16Vec3 operator +(Int16Vec3 a, Int16Vec3 b)
	{
		return new Int16Vec3(
			(short)(a.X + b.X),
			(short)(a.Y + b.Y),
			(short)(a.Z + b.Z));
	}

	public override string ToString()
	{
		return $"{X}, {Y}, {Z}";
	}

	public string ToString(string? format, IFormatProvider? formatProvider)
	{
		return $"{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}";
	}

	public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
	{
		charsWritten = 0;

		return
			SpanWrite.TryWrite(destination, ref charsWritten, X, format, provider) &&
			SpanWrite.TryWriteString(destination, ref charsWritten, ", ") &&
			SpanWrite.TryWrite(destination, ref charsWritten, Y, format, provider) &&
			SpanWrite.TryWriteString(destination, ref charsWritten, ", ") &&
			SpanWrite.TryWrite(destination, ref charsWritten, Z, format, provider);
	}

	public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
	{
		bytesWritten = 0;

		return
			SpanWrite.TryWrite(utf8Destination, ref bytesWritten, X, format, provider) &&
			SpanWrite.TryWriteBytes(utf8Destination, ref bytesWritten, ", "u8) &&
			SpanWrite.TryWrite(utf8Destination, ref bytesWritten, Y, format, provider) &&
			SpanWrite.TryWriteBytes(utf8Destination, ref bytesWritten, ", "u8) &&
			SpanWrite.TryWrite(utf8Destination, ref bytesWritten, Z, format, provider);
	}
}
