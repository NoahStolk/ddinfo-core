using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod;

public record ModBinaryTocEntry
{
	public ModBinaryTocEntry(string name, int offset, int size, AssetType assetType)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Asset name cannot be empty or whitespace.", nameof(name));

		Name = name;
		Offset = offset;
		Size = size;
		AssetType = assetType;

		IsEnabled = true;
		for (int i = 0; i < name.Length; i++)
		{
			if (!char.IsUpper(name[i]))
				continue;

			IsEnabled = false;
			break;
		}
	}

	public string Name { get; init; }
	public int Offset { get; init; }
	public int Size { get; init; }
	public AssetType AssetType { get; init; }
	public bool IsEnabled { get; }

	public bool IsLoudness()
	{
		return AssetType == AssetType.Audio && Name == "loudness";
	}

	public ModBinaryTocEntry Enable()
	{
		return this with { Name = Name.ToLower() };
	}

	public ModBinaryTocEntry Disable()
	{
		return this with { Name = Name.ToUpper() };
	}
}
