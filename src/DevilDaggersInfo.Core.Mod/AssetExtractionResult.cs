namespace DevilDaggersInfo.Core.Mod;

public record AssetExtractionResult(IReadOnlyDictionary<string, byte[]> ExtractedAssetFiles)
{
	internal static AssetExtractionResult Single(string assetName, string fileExtension, byte[] assetFileContents)
	{
		return new(new Dictionary<string, byte[]>
		{
			[$"{assetName}.{fileExtension}"] = assetFileContents,
		});
	}

	internal static AssetExtractionResult Shader(string assetName, ShaderData shaderData)
	{
		return new(new Dictionary<string, byte[]>
		{
			[$"{assetName}.vert"] = shaderData.VertexShaderFileContents,
			[$"{assetName}.frag"] = shaderData.FragmentShaderFileContents,
		});
	}
}
