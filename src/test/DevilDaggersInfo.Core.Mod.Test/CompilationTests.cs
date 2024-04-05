using DevilDaggersInfo.Core.Asset;
using DevilDaggersInfo.Core.Mod.Builders;
using DevilDaggersInfo.Core.Mod.Exceptions;
using DevilDaggersInfo.Core.Mod.FileHandling;
using DevilDaggersInfo.Core.Mod.Parsers;
using System.Text;

namespace DevilDaggersInfo.Core.Mod.Test;

[TestClass]
public class CompilationTests
{
	[DataTestMethod]
	[DataRow("iconmaskhoming.png", "iconmaskhoming", "dd-iconmaskhoming")]
	public void CompileTextureIntoModBinary(string sourcePngFileName, string assetName, string modFileName)
	{
		byte[] sourcePngContents = File.ReadAllBytes(Path.Combine("Resources", "Texture", sourcePngFileName));

		DdModBinaryBuilder builder = new();
		builder.AddTexture(assetName, sourcePngContents);

		byte[] compiledModBinary = builder.Compile();

		byte[] sourceModBinary = File.ReadAllBytes(Path.Combine("Resources", modFileName));
		CollectionAssert.AreEqual(compiledModBinary, sourceModBinary);
	}

	[DataTestMethod]
	[DataRow("depth.vert", "depth.frag", "depth")]
	public void CompileShaderIntoModBinary(string sourceVertexFileName, string sourceFragmentFileName, string assetName)
	{
		byte[] sourceVertexContents = File.ReadAllBytes(Path.Combine("Resources", "Shader", sourceVertexFileName));
		byte[] sourceFragmentContents = File.ReadAllBytes(Path.Combine("Resources", "Shader", sourceFragmentFileName));

		DdModBinaryBuilder modBinary = new();
		modBinary.AddShader(assetName, sourceVertexContents, sourceFragmentContents);

		byte[] compiledModBinary = modBinary.Compile();

		ModBinary extractedModBinary = new(compiledModBinary, ModBinaryReadFilter.AllAssets);
		AssetExtractionResult assetExtractionResult = extractedModBinary.ExtractAsset(assetName, AssetType.Shader);
		Assert.AreEqual(2, assetExtractionResult.ExtractedAssetFiles.Count);

		if (assetExtractionResult.ExtractedAssetFiles.TryGetValue($"{assetName}.vert", out byte[]? vertexContents))
			CollectionAssert.AreEqual(vertexContents, sourceVertexContents);
		else
			Assert.Fail("Vertex shader not found in extracted asset files.");

		if (assetExtractionResult.ExtractedAssetFiles.TryGetValue($"{assetName}.frag", out byte[]? fragmentContents))
			CollectionAssert.AreEqual(fragmentContents, sourceFragmentContents);
		else
			Assert.Fail("Fragment shader not found in extracted asset files.");
	}

	[TestMethod]
	public void TestDuplicateAsset()
	{
		DdModBinaryBuilder builder = new();
		builder.AddObjectBinding("test", Array.Empty<byte>());
		builder.AddMesh("test", Array.Empty<byte>());
		Assert.ThrowsException<InvalidModCompilationException>(() => builder.AddObjectBinding("test", Array.Empty<byte>()));
	}

	[TestMethod]
	public void TestBoidMeshCompilation()
	{
		ObjParsingContext objParsingContext = new();
		ParsedObjData obj = objParsingContext.Parse(Encoding.UTF8.GetString(File.ReadAllBytes(Path.Combine("Resources", "Mesh", "boid.obj"))));
		Assert.AreEqual(396, obj.Positions.Count);
		Assert.AreEqual(396, obj.TexCoords.Count);
		Assert.AreEqual(396, obj.Normals.Count);
		Assert.AreEqual(396, obj.Vertices.Count);
	}
}
