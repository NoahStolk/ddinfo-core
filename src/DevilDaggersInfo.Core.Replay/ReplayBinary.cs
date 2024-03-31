using System.Diagnostics.CodeAnalysis;

namespace DevilDaggersInfo.Core.Replay;

public class ReplayBinary<TReplayBinaryHeader>
	where TReplayBinaryHeader : IReplayBinaryHeader<TReplayBinaryHeader>
{
	public ReplayBinary(byte[] contents)
	{
		using MemoryStream ms = new(contents);
		using BinaryReader br = new(ms);

		Header = TReplayBinaryHeader.CreateFromBinaryReader(br);

		int compressedDataLength;
		if (TReplayBinaryHeader.UsesLengthPrefixedEvents)
			compressedDataLength = br.ReadInt32();
		else
			compressedDataLength = (int)(contents.Length - br.BaseStream.Position);

		Events = ReplayEventsParser.Parse(br.ReadBytes(compressedDataLength));
	}

	public ReplayBinary(TReplayBinaryHeader header, byte[] compressedEvents)
	{
		Header = header;
		Events = ReplayEventsParser.Parse(compressedEvents);
	}

	public ReplayBinary(TReplayBinaryHeader header, IReadOnlyList<ReplayEvent> events)
	{
		Header = header;
		Events = events;
	}

	public TReplayBinaryHeader Header { get; }
	public IReadOnlyList<ReplayEvent> Events { get; }

	public static bool TryParse(byte[] fileContents, [NotNullWhen(true)] out ReplayBinary<TReplayBinaryHeader>? replayBinary)
	{
		try
		{
			replayBinary = new(fileContents);
			return true;
		}
		catch (Exception ex)
		{
			replayBinary = null;
			return false;
		}
	}

	public byte[] Compile()
	{
		using MemoryStream ms = new();
		using BinaryWriter bw = new(ms);

		bw.Write(Header.ToBytes());

		byte[] compressedEvents = ReplayEventsCompiler.CompileEvents(Events);
		bw.Write(compressedEvents.Length);
		bw.Write(compressedEvents);

		return ms.ToArray();
	}
}
