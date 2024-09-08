namespace DevilDaggersInfo.Core.Asset.Extensions;

public static class AssetTypeExtensions
{
	public static AssetType? GetAssetType(this ushort type)
	{
		return type switch
		{
			0x01 => AssetType.Mesh,
			0x02 => AssetType.Texture,
			0x10 => AssetType.Shader,
			0x20 => AssetType.Audio,
			0x80 => AssetType.ObjectBinding,
			_ => null,
		};
	}
}
