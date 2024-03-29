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
		Assert.AreEqual(1, modBinaryToc.Entries.Count);
		Assert.AreEqual("boid", modBinaryToc.Entries[0].Name);
		Assert.AreEqual(AssetType.Mesh, modBinaryToc.Entries[0].AssetType);
		Assert.IsTrue(AssetContainer.IsProhibited(modBinaryToc.Entries[0].AssetType, modBinaryToc.Entries[0].Name));

		ModBinaryToc modBinaryTocDisabledProhibitedAssets = ModBinaryToc.DisableProhibitedAssets(modBinaryToc);
		Assert.AreEqual(1, modBinaryTocDisabledProhibitedAssets.Entries.Count);
		Assert.AreEqual("BOID", modBinaryTocDisabledProhibitedAssets.Entries[0].Name);
		Assert.AreEqual(AssetType.Mesh, modBinaryTocDisabledProhibitedAssets.Entries[0].AssetType);
		Assert.IsFalse(AssetContainer.IsProhibited(modBinaryTocDisabledProhibitedAssets.Entries[0].AssetType, modBinaryTocDisabledProhibitedAssets.Entries[0].Name));

		ModBinaryToc modBinaryTocEnabledAssets = ModBinaryToc.EnableAllAssets(modBinaryTocDisabledProhibitedAssets);
		Assert.AreEqual(1, modBinaryTocEnabledAssets.Entries.Count);
		Assert.AreEqual("boid", modBinaryTocEnabledAssets.Entries[0].Name);
		Assert.AreEqual(AssetType.Mesh, modBinaryTocEnabledAssets.Entries[0].AssetType);
		Assert.IsTrue(AssetContainer.IsProhibited(modBinaryTocEnabledAssets.Entries[0].AssetType, modBinaryTocEnabledAssets.Entries[0].Name));
	}

	[DataTestMethod]
	[DataRow(false, "BOID")]
	[DataRow(false, "BOID2")]
	[DataRow(false, "Boid2")]
	[DataRow(false, "boiD2")]
	[DataRow(true, "boid")]
	[DataRow(true, "boid2")]
	public void TestIsEnabled(bool expectedIsEnabled, string name)
	{
		ModBinaryTocEntry entry = new(name, 0, 4, AssetType.Mesh);
		Assert.AreEqual(expectedIsEnabled, entry.IsEnabled);
	}
}
