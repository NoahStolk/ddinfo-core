using DevilDaggersInfo.Core.Asset;
using DevilDaggersInfo.Core.Mod.Builders;

namespace DevilDaggersInfo.Core.Mod.Test;

internal sealed class ExtractionTests
{
	[Test]
	[Arguments(ModBinaryType.Dd, "dd-texture", "pedeblackbody", "pedeblackbody.png")]
	[Arguments(ModBinaryType.Dd, "dd-iconmaskhoming", "iconmaskhoming", "iconmaskhoming.png")]
	public async Task ExtractTextureAndCompareToSourcePng(ModBinaryType expectedBinaryType, string modFileName, string assetName, string sourcePngFileName)
	{
		string filePath = Path.Combine("Resources", modFileName);
		ModBinary modBinary = new(await File.ReadAllBytesAsync(filePath), ModBinaryReadFilter.AllAssets);
		await Assert.That(modBinary.Toc.Type).IsEqualTo(expectedBinaryType);
		await Assert.That(BinaryFileNameUtils.GetBinaryTypeBasedOnFileName(modFileName)).IsEqualTo(expectedBinaryType);

		KeyValuePair<AssetKey, AssetData> asset = modBinary.AssetMap.First(kvp => kvp.Key.AssetName == assetName);

		byte[] sourcePngContents = await File.ReadAllBytesAsync(Path.Combine("Resources", "Texture", sourcePngFileName));

		AssetExtractionResult extractedPngContents = modBinary.ExtractAsset(asset.Key);
		await Assert.That(extractedPngContents.ExtractedAssetFiles.Count).IsEqualTo(1);
		if (extractedPngContents.ExtractedAssetFiles.TryGetValue($"{assetName}.png", out byte[]? pngContents))
			await Assert.That(sourcePngContents).IsEquivalentTo(pngContents, CollectionOrdering.Matching);
		else
			Assert.Fail($"Asset name '{assetName}' not found in extracted asset files.");
	}

	[Test]
	public async Task ExtractAudioTypes()
	{
		AudioModBinaryBuilder audioBuilder = new();
		audioBuilder.AddAudio("sample", "RIFF"u8.ToArray(), null);
		audioBuilder.AddAudio("loudness", "sample = 10.0"u8.ToArray(), null);

		ModBinary audioModBinary = new(audioBuilder.Compile(), ModBinaryReadFilter.AllAssets);

		AssetExtractionResult sample = audioModBinary.ExtractAsset("sample", AssetType.Audio);
		await Assert.That(sample.ExtractedAssetFiles.Count).IsEqualTo(1);
		await Assert.That(sample.ExtractedAssetFiles["sample.wav"]).IsEquivalentTo("RIFF"u8.ToArray(), CollectionOrdering.Matching);

		AssetExtractionResult loudness = audioModBinary.ExtractAsset("loudness", AssetType.Audio);
		await Assert.That(loudness.ExtractedAssetFiles.Count).IsEqualTo(1);
		await Assert.That(loudness.ExtractedAssetFiles["loudness.ini"]).IsEquivalentTo("sample = 10.0"u8.ToArray(), CollectionOrdering.Matching);
	}

	[Test]
	public async Task ExtractDdTypes()
	{
		byte[] pngContents = await File.ReadAllBytesAsync(Path.Combine("Resources", "Texture", "pedeblackbody.png"));

		DdModBinaryBuilder ddBuilder = new();
		ddBuilder.AddMesh("mesh", "v 0 0 0"u8.ToArray());
		ddBuilder.AddObjectBinding("object_binding", "mesh = diffuse"u8.ToArray());
		ddBuilder.AddShader("shader", "vertex"u8.ToArray(), "fragment"u8.ToArray());
		ddBuilder.AddTexture("texture", pngContents);

		ModBinary ddModBinary = new(ddBuilder.Compile(), ModBinaryReadFilter.AllAssets);

		AssetExtractionResult mesh = ddModBinary.ExtractAsset("mesh", AssetType.Mesh);
		await Assert.That(mesh.ExtractedAssetFiles.Count).IsEqualTo(1);
		await Assert.That(mesh.ExtractedAssetFiles.ContainsKey("mesh.obj")).IsTrue();

		AssetExtractionResult objectBinding = ddModBinary.ExtractAsset("object_binding", AssetType.ObjectBinding);
		await Assert.That(objectBinding.ExtractedAssetFiles.Count).IsEqualTo(1);
		await Assert.That(objectBinding.ExtractedAssetFiles.ContainsKey("object_binding.txt")).IsTrue();
		await Assert.That(objectBinding.ExtractedAssetFiles["object_binding.txt"]).IsEquivalentTo("mesh = diffuse"u8.ToArray(), CollectionOrdering.Matching);

		AssetExtractionResult shader = ddModBinary.ExtractAsset("shader", AssetType.Shader);
		await Assert.That(shader.ExtractedAssetFiles.Count).IsEqualTo(2);
		await Assert.That(shader.ExtractedAssetFiles.ContainsKey("shader.vert")).IsTrue();
		await Assert.That(shader.ExtractedAssetFiles.ContainsKey("shader.frag")).IsTrue();
		await Assert.That(shader.ExtractedAssetFiles["shader.vert"]).IsEquivalentTo("vertex"u8.ToArray(), CollectionOrdering.Matching);
		await Assert.That(shader.ExtractedAssetFiles["shader.frag"]).IsEquivalentTo("fragment"u8.ToArray(), CollectionOrdering.Matching);

		AssetExtractionResult texture = ddModBinary.ExtractAsset("texture", AssetType.Texture);
		await Assert.That(texture.ExtractedAssetFiles.Count).IsEqualTo(1);
		await Assert.That(texture.ExtractedAssetFiles.ContainsKey("texture.png")).IsTrue();
		await Assert.That(texture.ExtractedAssetFiles["texture.png"]).IsEquivalentTo(pngContents, CollectionOrdering.Matching);
	}
}
