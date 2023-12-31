using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod.Extensions;

public static class ModBinaryTypeExtensions
{
	public static bool IsAssetTypeValid(this ModBinaryType modBinaryType, AssetType assetType)
	{
		return assetType == AssetType.Audio && modBinaryType == ModBinaryType.Audio || assetType != AssetType.Audio && modBinaryType != ModBinaryType.Audio;
	}
}
