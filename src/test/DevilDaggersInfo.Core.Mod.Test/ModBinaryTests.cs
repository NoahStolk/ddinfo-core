using DevilDaggersInfo.Core.Asset;
using DevilDaggersInfo.Core.Mod.Builders;
using DevilDaggersInfo.Core.Mod.Extensions;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Mod.Test;

[TestClass]
public class ModBinaryTests
{
	[DataTestMethod]
	[DataRow(ModBinaryType.Audio, "audio-empty")]
	[DataRow(ModBinaryType.Dd, "dd-mesh")]
	[DataRow(ModBinaryType.Dd, "dd-mesh-shader-texture")]
	[DataRow(ModBinaryType.Dd, "dd-shader")]
	[DataRow(ModBinaryType.Dd, "dd-skull-1-2-same-texture-copied", true)] // Cannot compare exact texture bytes, because the resizing algorithm is different. TODO: Compile using the same code instead of legacy DDAE.
	[DataRow(ModBinaryType.Dd, "dd-texture")] // This works because the textures are 1x1.
	public void CompareBinaryOutput(ModBinaryType type, string fileName, bool ignoreExactAssetData = false)
	{
		string filePath = Path.Combine("Resources", fileName);
		byte[] originalBytes = File.ReadAllBytes(filePath);

		ModBinary modBinary = new(originalBytes, ModBinaryReadFilter.AllAssets);
		AudioModBinaryBuilder audioBuilder = new();
		DdModBinaryBuilder ddBuilder = new();

		foreach (ModBinaryTocEntry entry in modBinary.Toc.Entries)
		{
			AssetExtractionResult extractedAsset = modBinary.ExtractAsset(entry.Name, entry.AssetType);
			switch (entry.AssetType)
			{
				case AssetType.Audio: audioBuilder.AddAudio(entry.Name, extractedAsset.ExtractedAssetFiles[$"{entry.Name}.wav"]); break;
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

		CollectionAssert.AreEqual(modBinary.Toc.Entries.ToList(), builder.TocEntries.ToList());

		Assert.AreEqual(modBinary.AssetMap.Count, builder.AssetMap.Count);

		if (ignoreExactAssetData)
			return;

		foreach (KeyValuePair<AssetKey, AssetData> asset in modBinary.AssetMap)
			CollectionAssert.AreEqual(asset.Value.Buffer, builder.AssetMap[asset.Key].Buffer);
	}

	[TestMethod]
	public void ValidateTocSingleAsset()
	{
		const string fileName = "dd-single-asset";
		string filePath = Path.Combine("Resources", fileName);
		byte[] originalBytes = File.ReadAllBytes(filePath);
		ModBinary modBinary = new(originalBytes, ModBinaryReadFilter.NoAssets);

		Assert.AreEqual(1, modBinary.Toc.Entries.Count);
		ModBinaryTocEntry tocEntry = modBinary.Toc.Entries[0];
		Assert.AreEqual("dagger6", tocEntry.Name);
		Assert.AreEqual(AssetType.Texture, tocEntry.AssetType);
		Assert.AreEqual(21855, tocEntry.Size);
	}

	[TestMethod]
	public void ValidateTocMultipleAssets()
	{
		const string fileName = "dd-nohand";
		string filePath = Path.Combine("Resources", fileName);
		byte[] originalBytes = File.ReadAllBytes(filePath);
		ModBinary modBinary = new(originalBytes, ModBinaryReadFilter.NoAssets);

		Assert.AreEqual(8, modBinary.Toc.Entries.Count);

		string[] names = ["hand", "hand2", "hand2left", "hand3", "hand3left", "hand4", "hand4left", "handleft"];
		int[] sizes = [166, 166, 198, 166, 198, 262, 390, 198];
		for (int i = 0; i < 8; i++)
		{
			ModBinaryTocEntry tocEntry = modBinary.Toc.Entries[i];
			Assert.AreEqual(names[i], tocEntry.Name);
			Assert.AreEqual(AssetType.Mesh, tocEntry.AssetType);
			Assert.AreEqual(sizes[i], tocEntry.Size);
		}
	}

	[TestMethod]
	public void ValidateAssetTypes()
	{
		Assert.IsTrue(ModBinaryType.Audio.IsAssetTypeValid(AssetType.Audio));
		Assert.IsFalse(ModBinaryType.Audio.IsAssetTypeValid(AssetType.Mesh));
		Assert.IsFalse(ModBinaryType.Audio.IsAssetTypeValid(AssetType.ObjectBinding));
		Assert.IsFalse(ModBinaryType.Audio.IsAssetTypeValid(AssetType.Shader));
		Assert.IsFalse(ModBinaryType.Audio.IsAssetTypeValid(AssetType.Texture));

		Assert.IsFalse(ModBinaryType.Dd.IsAssetTypeValid(AssetType.Audio));
		Assert.IsTrue(ModBinaryType.Dd.IsAssetTypeValid(AssetType.Mesh));
		Assert.IsTrue(ModBinaryType.Dd.IsAssetTypeValid(AssetType.ObjectBinding));
		Assert.IsTrue(ModBinaryType.Dd.IsAssetTypeValid(AssetType.Shader));
		Assert.IsTrue(ModBinaryType.Dd.IsAssetTypeValid(AssetType.Texture));
	}
}
