using DevilDaggersInfo.Core.Asset;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Mod;

public class ModBinary
{
	private readonly ModBinaryReadFilter _readFilter;

	public ModBinary(byte[] fileContents, ModBinaryReadFilter readFilter)
	{
		_readFilter = readFilter;

		Toc = ModBinaryToc.FromBytes(fileContents);
		AssetMap = new();

		using MemoryStream ms = new(fileContents);
		using BinaryReader br = new(ms);
		BuildAssetMap(br);
	}

	public ModBinary(Stream stream, ModBinaryReadFilter readFilter)
	{
		_readFilter = readFilter;

		using BinaryReader br = new(stream);
		Toc = ModBinaryToc.FromReader(br);
		AssetMap = new();

		BuildAssetMap(br);
	}

	public ModBinaryToc Toc { get; }
	public Dictionary<AssetKey, AssetData> AssetMap { get; }

	private void BuildAssetMap(BinaryReader br)
	{
		// If entries point to the same asset position; the asset is re-used (TODO: test if this even works in DD -- if not, remove DistinctBy).
		foreach (ModBinaryTocEntry entry in Toc.Entries.DistinctBy(c => c.Offset))
		{
			if (!_readFilter.ShouldRead(new(entry.AssetType, entry.Name)))
				continue;

			br.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
			byte[] buffer = br.ReadBytes(entry.Size);

			AssetMap[new(entry.AssetType, entry.Name)] = new(buffer);
		}
	}

	public AssetExtractionResult ExtractAsset(AssetKey assetKey)
	{
		return ExtractAsset(assetKey.AssetName, assetKey.AssetType);
	}

	public AssetExtractionResult ExtractAsset(string assetName, AssetType assetType)
	{
		if (!_readFilter.ShouldRead(new(assetType, assetName)))
			throw new InvalidOperationException("This asset has not been read. It was not included in the filter, so it cannot be extracted.");

		AssetKey key = new(assetType, assetName);
		if (!AssetMap.TryGetValue(key, out AssetData? value))
			throw new InvalidOperationException($"Mod binary does not contain an asset of type '{assetType}' with name '{assetName}'.");

		return assetType switch
		{
			AssetType.Audio => AssetExtractionResult.Single(assetName, assetName == "loudness" ? "ini" : "wav", value.Buffer),
			AssetType.Mesh => AssetExtractionResult.Single(assetName, "obj", ObjFileHandler.Extract(value.Buffer)),
			AssetType.ObjectBinding => AssetExtractionResult.Single(assetName, "txt", value.Buffer),
			AssetType.Shader => AssetExtractionResult.Shader(assetName, GlslFileHandler.Extract(value.Buffer)),
			AssetType.Texture => AssetExtractionResult.Single(assetName, "png", PngFileHandler.Extract(value.Buffer)),
			_ => throw new UnreachableException($"Asset type '{assetType}' not supported."),
		};
	}
}
