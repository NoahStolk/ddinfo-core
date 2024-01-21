using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod;

public record ModBinaryTocEntry(string Name, int Offset, int Size, AssetType AssetType)
{
	public bool IsLoudness() => AssetType == AssetType.Audio && Name == "loudness";
}
