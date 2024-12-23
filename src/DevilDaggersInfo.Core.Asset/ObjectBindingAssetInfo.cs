namespace DevilDaggersInfo.Core.Asset;

public sealed class ObjectBindingAssetInfo : AssetInfo
{
	public ObjectBindingAssetInfo(string assetName, bool isProhibited)
		: base(assetName, isProhibited)
	{
	}

	public override AssetType AssetType => AssetType.ObjectBinding;
}
