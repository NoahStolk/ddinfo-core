using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod;

public sealed class ModBinaryToc
{
	private ModBinaryToc(ModBinaryType type, IReadOnlyList<ModBinaryTocEntry> entries)
	{
		Type = type;
		Entries = entries;
	}

	public ModBinaryType Type { get; }

	public IReadOnlyList<ModBinaryTocEntry> Entries { get; }

	/// <summary>
	/// Reads the mod binary header and TOC from the byte array.
	/// </summary>
	public static ModBinaryToc FromBytes(byte[] contents)
	{
		using MemoryStream ms = new(contents);
		using BinaryReader br = new(ms);
		return FromReader(br);
	}

	/// <summary>
	/// Reads the mod binary header and TOC from the stream.
	/// </summary>
	public static ModBinaryToc FromReader(BinaryReader br)
	{
		uint tocSize = GetSize(br.BaseStream.Length, br);

		ModBinaryType? modBinaryType = null;
		List<ModBinaryTocEntry> entries = [];
		while (br.BaseStream.Position < ModBinaryConstants.HeaderSize + tocSize)
		{
			ushort type = br.ReadUInt16();
			if (type == 0)
				break; // Break the loop when the end of the TOC is reached (which is 0x0000).

			ModBinaryTocEntry? entry = ReadEntry(tocSize, type, br);
			if (entry == null)
				continue; // Skip invalid entries.

			entries.Add(entry);

			if (!modBinaryType.HasValue)
				modBinaryType = entry.AssetType == AssetType.Audio ? ModBinaryType.Audio : ModBinaryType.Dd;
			else if (!modBinaryType.Value.IsAssetTypeValid(entry.AssetType))
				throw new InvalidModBinaryException($"Asset type '{entry.AssetType}' is not compatible with binary type '{modBinaryType}'.");
		}

		if (!modBinaryType.HasValue)
			throw new InvalidModBinaryException("Mod binary type could not be determined, probably because there are no assets.");

		return new(modBinaryType.Value, entries);
	}

	public static ModBinaryType DetermineType(byte[] contents)
	{
		using MemoryStream ms = new(contents);
		using BinaryReader br = new(ms);
		uint tocSize = GetSize(ms.Length, br);

		while (br.BaseStream.Position < ModBinaryConstants.HeaderSize + tocSize)
		{
			ushort type = br.ReadUInt16();
			if (type == 0)
				break;

			ModBinaryTocEntry? entry = ReadEntry(tocSize, type, br);
			if (entry != null)
				return entry.AssetType == AssetType.Audio ? ModBinaryType.Audio : ModBinaryType.Dd;
		}

		throw new InvalidModBinaryException("Mod binary type could not be determined, probably because there are no assets.");
	}

	/// <summary>
	/// Enables all assets in the TOC by converting their names to lowercase. All the original assets use lowercase names, so this is the default state.
	/// </summary>
	public static ModBinaryToc EnableAllAssets(ModBinaryToc original)
	{
		List<ModBinaryTocEntry> entries = [];
		foreach (ModBinaryTocEntry entry in original.Entries)
			entries.Add(entry with { Name = entry.Name.ToLower() });

		return new(original.Type, entries);
	}

	/// <summary>
	/// Disables all prohibited assets in the TOC by converting their names to uppercase. These assets will not be loaded by the game, resulting in the mod not being prohibited.
	/// </summary>
	public static ModBinaryToc DisableProhibitedAssets(ModBinaryToc original)
	{
		List<ModBinaryTocEntry> entries = [];
		foreach (ModBinaryTocEntry entry in original.Entries)
		{
			if (AssetContainer.IsProhibited(entry.AssetType, entry.Name))
				entries.Add(entry with { Name = entry.Name.ToUpper() });
			else
				entries.Add(entry);
		}

		return new(original.Type, entries);
	}

	private static uint GetSize(long totalStreamLength, BinaryReader br)
	{
		if (totalStreamLength < ModBinaryConstants.HeaderSize)
			throw new InvalidModBinaryException($"Invalid binary; must be at least {ModBinaryConstants.HeaderSize} bytes in length.");

		ulong fileIdentifier = br.ReadUInt64();
		if (fileIdentifier != ModBinaryConstants.Identifier)
			throw new InvalidModBinaryException("Invalid binary; incorrect header values.");

		uint tocSize = br.ReadUInt32();
		if (tocSize > totalStreamLength - ModBinaryConstants.HeaderSize)
			throw new InvalidModBinaryException("Invalid binary; TOC size is larger than the remaining amount of bytes.");

		return tocSize;
	}

	private static ModBinaryTocEntry? ReadEntry(uint tocSize, ushort type, BinaryReader br)
	{
		// Read everything first.
		AssetType? assetType = type.GetAssetType();
		string name = br.ReadNullTerminatedString();
		int offset = br.ReadInt32();
		int size = br.ReadInt32();
		_ = br.ReadInt32();

		// Skip invalid entries (present in default dd binary).
		if (size <= 0 || offset < ModBinaryConstants.HeaderSize + tocSize)
			return null;

		// Skip unknown or obsolete types (such as 0x11, which is an outdated type for (fragment?) shaders).
		if (!assetType.HasValue)
			return null;

		return new(name, offset, size, assetType.Value);
	}
}
