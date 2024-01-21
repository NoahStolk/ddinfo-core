using DevilDaggersInfo.Core.Asset;
using DevilDaggersInfo.Core.Mod.Builders;

namespace DevilDaggersInfo.Core.Mod.Test;

[TestClass]
public class ExtractionTests
{
	[DataTestMethod]
	[DataRow(ModBinaryType.Dd, "dd-texture", "pedeblackbody", "pedeblackbody.png")]
	[DataRow(ModBinaryType.Dd, "dd-iconmaskhoming", "iconmaskhoming", "iconmaskhoming.png")]
	public void ExtractTextureAndCompareToSourcePng(ModBinaryType expectedBinaryType, string modFileName, string assetName, string sourcePngFileName)
	{
		string filePath = Path.Combine("Resources", modFileName);
		ModBinary modBinary = new(File.ReadAllBytes(filePath), ModBinaryReadFilter.AllAssets);
		Assert.AreEqual(expectedBinaryType, modBinary.Toc.Type);
		Assert.AreEqual(expectedBinaryType, BinaryFileNameUtils.GetBinaryTypeBasedOnFileName(modFileName));

		KeyValuePair<AssetKey, AssetData> asset = modBinary.AssetMap.First(kvp => kvp.Key.AssetName == assetName);

		byte[] sourcePngContents = File.ReadAllBytes(Path.Combine("Resources", "Texture", sourcePngFileName));

		AssetExtractionResult extractedPngContents = modBinary.ExtractAsset(asset.Key);
		Assert.AreEqual(1, extractedPngContents.ExtractedAssetFiles.Count);
		if (extractedPngContents.ExtractedAssetFiles.TryGetValue($"{assetName}.png", out byte[]? pngContents))
			CollectionAssert.AreEqual(pngContents, sourcePngContents);
		else
			Assert.Fail($"Asset name '{assetName}' not found in extracted asset files.");
	}

	[TestMethod]
	public void ExtractAudioTypes()
	{
		AudioModBinaryBuilder audioBuilder = new();
		audioBuilder.AddAudio("sample", "RIFF"u8.ToArray(), null);
		audioBuilder.AddAudio("loudness", "sample = 10.0"u8.ToArray(), null);

		ModBinary audioModBinary = new(audioBuilder.Compile(), ModBinaryReadFilter.AllAssets);

		AssetExtractionResult sample = audioModBinary.ExtractAsset("sample", AssetType.Audio);
		Assert.AreEqual(1, sample.ExtractedAssetFiles.Count);
		CollectionAssert.AreEqual("RIFF"u8.ToArray(), sample.ExtractedAssetFiles["sample.wav"]);

		AssetExtractionResult loudness = audioModBinary.ExtractAsset("loudness", AssetType.Audio);
		Assert.AreEqual(1, loudness.ExtractedAssetFiles.Count);
		CollectionAssert.AreEqual("sample = 10.0"u8.ToArray(), loudness.ExtractedAssetFiles["loudness.ini"]);
	}

	[TestMethod]
	public void ExtractDdTypes()
	{
		byte[] pngContents = File.ReadAllBytes(Path.Combine("Resources", "Texture", "pedeblackbody.png"));

		DdModBinaryBuilder ddBuilder = new();
		ddBuilder.AddMesh("mesh", "v 0 0 0"u8.ToArray());
		ddBuilder.AddObjectBinding("object_binding", "mesh = diffuse"u8.ToArray());
		ddBuilder.AddShader("shader", "vertex"u8.ToArray(), "fragment"u8.ToArray());
		ddBuilder.AddTexture("texture", pngContents);

		ModBinary ddModBinary = new(ddBuilder.Compile(), ModBinaryReadFilter.AllAssets);

		AssetExtractionResult mesh = ddModBinary.ExtractAsset("mesh", AssetType.Mesh);
		Assert.AreEqual(1, mesh.ExtractedAssetFiles.Count);
		Assert.IsTrue(mesh.ExtractedAssetFiles.ContainsKey("mesh.obj"));

		AssetExtractionResult objectBinding = ddModBinary.ExtractAsset("object_binding", AssetType.ObjectBinding);
		Assert.AreEqual(1, objectBinding.ExtractedAssetFiles.Count);
		Assert.IsTrue(objectBinding.ExtractedAssetFiles.ContainsKey("object_binding.txt"));
		CollectionAssert.AreEqual("mesh = diffuse"u8.ToArray(), objectBinding.ExtractedAssetFiles["object_binding.txt"]);

		AssetExtractionResult shader = ddModBinary.ExtractAsset("shader", AssetType.Shader);
		Assert.AreEqual(2, shader.ExtractedAssetFiles.Count);
		Assert.IsTrue(shader.ExtractedAssetFiles.ContainsKey("shader.vert"));
		Assert.IsTrue(shader.ExtractedAssetFiles.ContainsKey("shader.frag"));
		CollectionAssert.AreEqual("vertex"u8.ToArray(), shader.ExtractedAssetFiles["shader.vert"]);
		CollectionAssert.AreEqual("fragment"u8.ToArray(), shader.ExtractedAssetFiles["shader.frag"]);

		AssetExtractionResult texture = ddModBinary.ExtractAsset("texture", AssetType.Texture);
		Assert.AreEqual(1, texture.ExtractedAssetFiles.Count);
		Assert.IsTrue(texture.ExtractedAssetFiles.ContainsKey("texture.png"));
		CollectionAssert.AreEqual(pngContents, texture.ExtractedAssetFiles["texture.png"]);
	}
}
