using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod;

public record ModBinaryTocEntry(string Name, int Offset, int Size, AssetType AssetType)
{
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
