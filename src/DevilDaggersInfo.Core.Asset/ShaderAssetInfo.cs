namespace DevilDaggersInfo.Core.Asset;

public sealed class ShaderAssetInfo : AssetInfo
{
	public ShaderAssetInfo(string assetName, bool isProhibited)
		: base(assetName, isProhibited)
	{
	}

	public override AssetType AssetType => AssetType.Shader;
}
