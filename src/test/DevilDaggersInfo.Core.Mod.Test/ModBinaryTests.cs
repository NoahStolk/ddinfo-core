using DevilDaggersInfo.Core.Asset;
using DevilDaggersInfo.Core.Mod.Builders;
using DevilDaggersInfo.Core.Mod.Extensions;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Mod.Test;

internal sealed class ModBinaryTests
{
	[Test]
	[Arguments(ModBinaryType.Audio, "audio-empty")]
	[Arguments(ModBinaryType.Dd, "dd-mesh")]
	[Arguments(ModBinaryType.Dd, "dd-mesh-shader-texture")]
	[Arguments(ModBinaryType.Dd, "dd-shader")]
	[Arguments(ModBinaryType.Dd, "dd-skull-1-2-same-texture-copied", true)] // Cannot compare exact texture bytes, because the resizing algorithm is different. TODO: Compile using the same code instead of legacy DDAE.
	[Arguments(ModBinaryType.Dd, "dd-texture")] // This works because the textures are 1x1.
	public async Task CompareBinaryOutput(ModBinaryType type, string fileName, bool ignoreExactAssetData = false)
	{
		string filePath = Path.Combine("Resources", fileName);
		byte[] originalBytes = await File.ReadAllBytesAsync(filePath);

		ModBinary modBinary = new(originalBytes, ModBinaryReadFilter.AllAssets);
		AudioModBinaryBuilder audioBuilder = new();
		DdModBinaryBuilder ddBuilder = new();

		foreach (ModBinaryTocEntry entry in modBinary.Toc.Entries)
		{
			AssetExtractionResult extractedAsset = modBinary.ExtractAsset(entry.Name, entry.AssetType);
			switch (entry.AssetType)
			{
				case AssetType.Audio: audioBuilder.AddAudio(entry.Name, extractedAsset.ExtractedAssetFiles[$"{entry.Name}.{(entry.Name == "loudness" ? "ini" : "wav")}"], null); break;
				case AssetType.Mesh: ddBuilder.AddMesh(entry.Name, extractedAsset.ExtractedAssetFiles[$"{entry.Name}.obj"]); break;
				case AssetType.ObjectBinding: ddBuilder.AddObjectBinding(entry.Name, extractedAsset.ExtractedAssetFiles[$"{entry.Name}.txt"]); break;
				case AssetType.Shader: ddBuilder.AddShader(entry.Name, extractedAsset.ExtractedAssetFiles[$"{entry.Name}.vert"], extractedAsset.ExtractedAssetFiles[$"{entry.Name}.frag"]); break;
				case AssetType.Texture: ddBuilder.AddTexture(entry.Name, extractedAsset.ExtractedAssetFiles[$"{entry.Name}.png"]); break;
				default: throw new UnreachableException();
			}
		}

		ModBinaryBuilder builder = type switch
		{
			ModBinaryType.Audio => audioBuilder,
			ModBinaryType.Dd => ddBuilder,
			_ => throw new UnreachableException(),
		};

		await Assert.That(builder.TocEntries.ToList()).IsEquivalentTo([.. modBinary.Toc.Entries], CollectionOrdering.Matching);

		await Assert.That(builder.AssetMap.Count).IsEqualTo(modBinary.AssetMap.Count);

		if (ignoreExactAssetData)
			return;

		foreach (KeyValuePair<AssetKey, AssetData> asset in modBinary.AssetMap)
			await Assert.That(builder.AssetMap[asset.Key].Buffer).IsEquivalentTo(asset.Value.Buffer, CollectionOrdering.Matching);
	}

	[Test]
	public async Task ValidateTocSingleAsset()
	{
		const string fileName = "dd-single-asset";
		string filePath = Path.Combine("Resources", fileName);
		byte[] originalBytes = await File.ReadAllBytesAsync(filePath);
		ModBinary modBinary = new(originalBytes, ModBinaryReadFilter.NoAssets);

		await Assert.That(modBinary.Toc.Entries.Count).IsEqualTo(1);
		ModBinaryTocEntry tocEntry = modBinary.Toc.Entries[0];
		await Assert.That(tocEntry.Name).IsEqualTo("dagger6");
		await Assert.That(tocEntry.AssetType).IsEqualTo(AssetType.Texture);
		await Assert.That(tocEntry.Size).IsEqualTo(21855);
	}

	[Test]
	public async Task ValidateTocMultipleAssets()
	{
		const string fileName = "dd-nohand";
		string filePath = Path.Combine("Resources", fileName);
		byte[] originalBytes = await File.ReadAllBytesAsync(filePath);
		ModBinary modBinary = new(originalBytes, ModBinaryReadFilter.NoAssets);

		await Assert.That(modBinary.Toc.Entries.Count).IsEqualTo(8);

		string[] names = ["hand", "hand2", "hand2left", "hand3", "hand3left", "hand4", "hand4left", "handleft"];
		int[] sizes = [166, 166, 198, 166, 198, 262, 390, 198];
		for (int i = 0; i < 8; i++)
		{
			ModBinaryTocEntry tocEntry = modBinary.Toc.Entries[i];
			await Assert.That(tocEntry.Name).IsEqualTo(names[i]);
			await Assert.That(tocEntry.AssetType).IsEqualTo(AssetType.Mesh);
			await Assert.That(tocEntry.Size).IsEqualTo(sizes[i]);
		}
	}

	[Test]
	public async Task ValidateAssetTypes()
	{
		await Assert.That(ModBinaryType.Audio.IsAssetTypeValid(AssetType.Audio)).IsTrue();
		await Assert.That(ModBinaryType.Audio.IsAssetTypeValid(AssetType.Mesh)).IsFalse();
		await Assert.That(ModBinaryType.Audio.IsAssetTypeValid(AssetType.ObjectBinding)).IsFalse();
		await Assert.That(ModBinaryType.Audio.IsAssetTypeValid(AssetType.Shader)).IsFalse();
		await Assert.That(ModBinaryType.Audio.IsAssetTypeValid(AssetType.Texture)).IsFalse();

		await Assert.That(ModBinaryType.Dd.IsAssetTypeValid(AssetType.Audio)).IsFalse();
		await Assert.That(ModBinaryType.Dd.IsAssetTypeValid(AssetType.Mesh)).IsTrue();
		await Assert.That(ModBinaryType.Dd.IsAssetTypeValid(AssetType.ObjectBinding)).IsTrue();
		await Assert.That(ModBinaryType.Dd.IsAssetTypeValid(AssetType.Shader)).IsTrue();
		await Assert.That(ModBinaryType.Dd.IsAssetTypeValid(AssetType.Texture)).IsTrue();
	}
}
