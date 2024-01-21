namespace DevilDaggersInfo.Core.Mod.Builders;

/// <summary>
/// Exposes a way of compiling a mod binary.
/// </summary>
public abstract class ModBinaryBuilder
{
	private const int _tocEntrySizeWithoutName = 15;

	private readonly List<ModBinaryTocEntry> _tocEntries = [];
	private protected readonly Dictionary<AssetKey, AssetData> _assetMap = new();

	public abstract ModBinaryType Type { get; }

	public IReadOnlyList<ModBinaryTocEntry> TocEntries => _tocEntries;

	public IReadOnlyDictionary<AssetKey, AssetData> AssetMap => _assetMap;

	private protected void ValidateAsset(AssetKey assetKey)
	{
		if (!Type.IsAssetTypeValid(assetKey.AssetType))
			throw new InvalidModCompilationException($"Asset type '{assetKey.AssetType}' is not compatible with binary type '{Type}'.");

		if (_tocEntries.Exists(t => t.Name == assetKey.AssetName && t.AssetType == assetKey.AssetType))
			throw new InvalidModCompilationException($"Asset of type '{assetKey.AssetType}' named '{assetKey.AssetName}' already exists in the mod binary.");
	}

	private protected void RebuildToc()
	{
		_tocEntries.Clear();

		int offset = ModBinaryConstants.HeaderSize + _tocEntrySizeWithoutName * _assetMap.Count + _assetMap.Sum(kvp => Encoding.UTF8.GetBytes(kvp.Key.AssetName).Length) + sizeof(short);
		foreach (KeyValuePair<AssetKey, AssetData> kvp in _assetMap)
		{
			int size = kvp.Value.Buffer.Length;
			_tocEntries.Add(new(kvp.Key.AssetName, offset, size, kvp.Key.AssetType));

			offset += size;
		}
	}

	public virtual byte[] Compile()
	{
		int tocBufferSize = _tocEntrySizeWithoutName * _assetMap.Count + _tocEntries.Sum(c => Encoding.UTF8.GetBytes(c.Name).Length) + sizeof(short);
		int offset = ModBinaryConstants.HeaderSize + tocBufferSize;
		byte[]? tocBuffer;
		using (MemoryStream tocStream = new())
		{
			using BinaryWriter tocWriter = new(tocStream);
			foreach ((AssetKey key, AssetData data) in _assetMap)
			{
				tocWriter.Write((ushort)key.AssetType);

				tocWriter.Write(Encoding.UTF8.GetBytes(key.AssetName));
				tocWriter.Write((byte)0);

				int size = data.Buffer.Length;

				tocWriter.Write(offset);
				tocWriter.Write(size);
				tocWriter.Write(0);

				offset += size;
			}

			tocWriter.Write((short)0);

			tocBuffer = tocStream.ToArray();
		}

		if (tocBuffer.Length != tocBufferSize)
			throw new InvalidOperationException($"Invalid TOC buffer size: {tocBuffer.Length}. Expected length was {tocBufferSize}.");

		List<AssetData> uniqueAssets = _assetMap.Select(ad => ad.Value).Distinct().ToList();
		byte[]? assetBuffer;
		using (MemoryStream assetStream = new())
		{
			using BinaryWriter assetWriter = new(assetStream);
			foreach (AssetData assetData in uniqueAssets)
				assetWriter.Write(assetData.Buffer);

			assetBuffer = assetStream.ToArray();
		}

		using MemoryStream ms = new();
		ms.Write(BitConverter.GetBytes(ModBinaryConstants.Identifier));
		ms.Write(BitConverter.GetBytes((uint)tocBuffer.Length));
		ms.Write(tocBuffer);
		ms.Write(assetBuffer);
		return ms.ToArray();
	}
}
