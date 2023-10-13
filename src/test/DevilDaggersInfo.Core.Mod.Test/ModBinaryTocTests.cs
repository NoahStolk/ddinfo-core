using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod.Test;

[TestClass]
public class ModBinaryTocTests
{
	[DataTestMethod]
	[DataRow(ModBinaryType.Audio, "audio-empty")]
	[DataRow(ModBinaryType.Dd, "dd-mesh")]
	[DataRow(ModBinaryType.Dd, "dd-mesh-shader-texture")]
	[DataRow(ModBinaryType.Dd, "dd-shader")]
	[DataRow(ModBinaryType.Dd, "dd-skull-1-2-same-texture-copied")]
	[DataRow(ModBinaryType.Dd, "dd-texture")]
	public void DetermineModBinaryType(ModBinaryType expectedType, string fileName)
	{
		string filePath = Path.Combine("Resources", fileName);
		byte[] originalBytes = File.ReadAllBytes(filePath);

		ModBinaryType type = ModBinaryToc.DetermineType(originalBytes);
		Assert.AreEqual(expectedType, type);
	}

	// TODO: Test with binary that has both prohibited and non-prohibited assets.
	[TestMethod]
	public void TestDisableProhibitedAssets()
	{
		string filePath = Path.Combine("Resources", "dd-cylinder-boid");
		byte[] originalBytes = File.ReadAllBytes(filePath);

		ModBinaryToc modBinaryToc = ModBinaryToc.FromBytes(originalBytes);
		Assert.AreEqual(1, modBinaryToc.Chunks.Count);
		Assert.AreEqual("boid", modBinaryToc.Chunks[0].Name);
		Assert.AreEqual(AssetType.Mesh, modBinaryToc.Chunks[0].AssetType);
		Assert.AreEqual(true, AssetContainer.GetIsProhibited(modBinaryToc.Chunks[0].AssetType, modBinaryToc.Chunks[0].Name));

		ModBinaryToc modBinaryTocDisabledProhibitedAssets = ModBinaryToc.DisableProhibitedAssets(modBinaryToc);
		Assert.AreEqual(1, modBinaryTocDisabledProhibitedAssets.Chunks.Count);
		Assert.AreEqual("BOID", modBinaryTocDisabledProhibitedAssets.Chunks[0].Name);
		Assert.AreEqual(AssetType.Mesh, modBinaryTocDisabledProhibitedAssets.Chunks[0].AssetType);
		Assert.AreEqual(false, AssetContainer.GetIsProhibited(modBinaryTocDisabledProhibitedAssets.Chunks[0].AssetType, modBinaryTocDisabledProhibitedAssets.Chunks[0].Name));

		ModBinaryToc modBinaryTocEnabledAssets = ModBinaryToc.EnableAllAssets(modBinaryTocDisabledProhibitedAssets);
		Assert.AreEqual(1, modBinaryTocEnabledAssets.Chunks.Count);
		Assert.AreEqual("boid", modBinaryTocEnabledAssets.Chunks[0].Name);
		Assert.AreEqual(AssetType.Mesh, modBinaryTocEnabledAssets.Chunks[0].AssetType);
		Assert.AreEqual(true, AssetContainer.GetIsProhibited(modBinaryTocEnabledAssets.Chunks[0].AssetType, modBinaryTocEnabledAssets.Chunks[0].Name));
	}
}
