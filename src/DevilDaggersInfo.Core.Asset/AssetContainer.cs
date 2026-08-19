using System.Diagnostics;

namespace DevilDaggersInfo.Core.Asset;

public static class AssetContainer
{
	public static bool IsProhibited(AssetType assetType, string assetName)
	{
		return assetType switch
		{
			AssetType.Audio => IsProhibited(AudioAudio.All, assetName),
			AssetType.ObjectBinding => IsProhibited(DdObjectBindings.All, assetName),
			AssetType.Mesh => IsProhibited(DdMeshes.All, assetName),
			AssetType.Shader => IsProhibited(DdShaders.All, assetName),
			AssetType.Texture => IsProhibited(DdTextures.All, assetName),
			_ => throw new UnreachableException(),
		};
	}

	private static bool IsProhibited(IReadOnlyList<AssetInfo> assets, string assetName)
	{
		// Do not use LINQ here for memory allocation reasons.
		for (int i = 0; i < assets.Count; i++)
		{
			AssetInfo assetInfo = assets[i];
			if (assetInfo.AssetName == assetName)
				return assetInfo.IsProhibited;
		}

		return false;
	}
}
