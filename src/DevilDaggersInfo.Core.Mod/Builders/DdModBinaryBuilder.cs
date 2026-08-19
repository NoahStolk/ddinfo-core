using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod.Builders;

public sealed class DdModBinaryBuilder : ModBinaryBuilder
{
	public override ModBinaryType Type => ModBinaryType.Dd;

	public void AddMesh(string assetName, byte[] objFileContents)
	{
		AssetKey key = new(AssetType.Mesh, assetName);
		ValidateAsset(key);
		_assetMap.Add(key, new AssetData(ObjFileHandler.Compile(objFileContents)));
		RebuildToc();
	}

	public void AddObjectBinding(string assetName, byte[] txtFileContents)
	{
		AssetKey key = new(AssetType.ObjectBinding, assetName);
		ValidateAsset(key);
		_assetMap.Add(key, new AssetData(txtFileContents));
		RebuildToc();
	}

	public void AddShader(string assetName, byte[] vertexShaderFileContents, byte[] fragmentShaderFileContents)
	{
		AssetKey key = new(AssetType.Shader, assetName);
		ValidateAsset(key);
		_assetMap.Add(key, new AssetData(GlslFileHandler.Compile(assetName, vertexShaderFileContents, fragmentShaderFileContents)));
		RebuildToc();
	}

	public void AddTexture(string assetName, byte[] pngFileContents)
	{
		AssetKey key = new(AssetType.Texture, assetName);
		ValidateAsset(key);
		_assetMap.Add(key, new AssetData(PngFileHandler.Compile(pngFileContents)));
		RebuildToc();
	}
}
