using DevilDaggersInfo.Core.Asset;
using DevilDaggersInfo.Core.Mod.Builders;
using DevilDaggersInfo.Core.Mod.Exceptions;
using DevilDaggersInfo.Core.Mod.Parsers;
using System.Text;

namespace DevilDaggersInfo.Core.Mod.Test;

internal sealed class CompilationTests
{
	[Test]
	[Arguments("iconmaskhoming.png", "iconmaskhoming", "dd-iconmaskhoming")]
	public async Task CompileTextureIntoModBinary(string sourcePngFileName, string assetName, string modFileName)
	{
		byte[] sourcePngContents = await File.ReadAllBytesAsync(Path.Combine("Resources", "Texture", sourcePngFileName));

		DdModBinaryBuilder builder = new();
		builder.AddTexture(assetName, sourcePngContents);

		byte[] compiledModBinary = builder.Compile();

		byte[] sourceModBinary = await File.ReadAllBytesAsync(Path.Combine("Resources", modFileName));
		await Assert.That(sourceModBinary).IsEquivalentTo(compiledModBinary, CollectionOrdering.Matching);
	}

	[Test]
	[Arguments("depth.vert", "depth.frag", "depth")]
	public async Task CompileShaderIntoModBinary(string sourceVertexFileName, string sourceFragmentFileName, string assetName)
	{
		byte[] sourceVertexContents = await File.ReadAllBytesAsync(Path.Combine("Resources", "Shader", sourceVertexFileName));
		byte[] sourceFragmentContents = await File.ReadAllBytesAsync(Path.Combine("Resources", "Shader", sourceFragmentFileName));

		DdModBinaryBuilder modBinary = new();
		modBinary.AddShader(assetName, sourceVertexContents, sourceFragmentContents);

		byte[] compiledModBinary = modBinary.Compile();

		ModBinary extractedModBinary = new(compiledModBinary, ModBinaryReadFilter.AllAssets);
		AssetExtractionResult assetExtractionResult = extractedModBinary.ExtractAsset(assetName, AssetType.Shader);
		await Assert.That(assetExtractionResult.ExtractedAssetFiles.Count).IsEqualTo(2);

		if (assetExtractionResult.ExtractedAssetFiles.TryGetValue($"{assetName}.vert", out byte[]? vertexContents))
			await Assert.That(sourceVertexContents).IsEquivalentTo(vertexContents, CollectionOrdering.Matching);
		else
			Assert.Fail("Vertex shader not found in extracted asset files.");

		if (assetExtractionResult.ExtractedAssetFiles.TryGetValue($"{assetName}.frag", out byte[]? fragmentContents))
			await Assert.That(sourceFragmentContents).IsEquivalentTo(fragmentContents, CollectionOrdering.Matching);
		else
			Assert.Fail("Fragment shader not found in extracted asset files.");
	}

	[Test]
	public void TestDuplicateAsset()
	{
		DdModBinaryBuilder builder = new();
		builder.AddObjectBinding("test", []);
		builder.AddMesh("test", []);
		Assert.ThrowsExactly<InvalidModCompilationException>(() => builder.AddObjectBinding("test", []));
	}

	[Test]
	public async Task TestBoidMeshCompilation()
	{
		ObjParsingContext objParsingContext = new();
		ParsedObjData obj = objParsingContext.Parse(Encoding.UTF8.GetString(await File.ReadAllBytesAsync(Path.Combine("Resources", "Mesh", "boid.obj"))));
		await Assert.That(obj.Positions.Count).IsEqualTo(396);
		await Assert.That(obj.TexCoords.Count).IsEqualTo(396);
		await Assert.That(obj.Normals.Count).IsEqualTo(396);
		await Assert.That(obj.Vertices.Count).IsEqualTo(396);
	}
}
