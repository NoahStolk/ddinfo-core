using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod.Test;

internal sealed class ModBinaryTocTests
{
	[Test]
	[Arguments(ModBinaryType.Audio, "audio-empty")]
	[Arguments(ModBinaryType.Dd, "dd-mesh")]
	[Arguments(ModBinaryType.Dd, "dd-mesh-shader-texture")]
	[Arguments(ModBinaryType.Dd, "dd-shader")]
	[Arguments(ModBinaryType.Dd, "dd-skull-1-2-same-texture-copied")]
	[Arguments(ModBinaryType.Dd, "dd-texture")]
	public async Task DetermineModBinaryType(ModBinaryType expectedType, string fileName)
	{
		string filePath = Path.Combine("Resources", fileName);
		byte[] originalBytes = await File.ReadAllBytesAsync(filePath);

		ModBinaryType type = ModBinaryToc.DetermineType(originalBytes);
		await Assert.That(type).IsEqualTo(expectedType);
	}

	// TODO: Test with binary that has both prohibited and non-prohibited assets.
	[Test]
	public async Task TestDisableProhibitedAssets()
	{
		string filePath = Path.Combine("Resources", "dd-cylinder-boid");
		byte[] originalBytes = await File.ReadAllBytesAsync(filePath);

		ModBinaryToc modBinaryToc = ModBinaryToc.FromBytes(originalBytes);
		await Assert.That(modBinaryToc.Entries.Count).IsEqualTo(1);
		await Assert.That(modBinaryToc.Entries[0].Name).IsEqualTo("boid");
		await Assert.That(modBinaryToc.Entries[0].AssetType).IsEqualTo(AssetType.Mesh);
		await Assert.That(AssetContainer.IsProhibited(modBinaryToc.Entries[0].AssetType, modBinaryToc.Entries[0].Name)).IsTrue();

		ModBinaryToc modBinaryTocDisabledProhibitedAssets = ModBinaryToc.DisableProhibitedAssets(modBinaryToc);
		await Assert.That(modBinaryTocDisabledProhibitedAssets.Entries.Count).IsEqualTo(1);
		await Assert.That(modBinaryTocDisabledProhibitedAssets.Entries[0].Name).IsEqualTo("BOID");
		await Assert.That(modBinaryTocDisabledProhibitedAssets.Entries[0].AssetType).IsEqualTo(AssetType.Mesh);
		await Assert.That(AssetContainer.IsProhibited(modBinaryTocDisabledProhibitedAssets.Entries[0].AssetType, modBinaryTocDisabledProhibitedAssets.Entries[0].Name)).IsFalse();

		ModBinaryToc modBinaryTocEnabledAssets = ModBinaryToc.EnableAllAssets(modBinaryTocDisabledProhibitedAssets);
		await Assert.That(modBinaryTocEnabledAssets.Entries.Count).IsEqualTo(1);
		await Assert.That(modBinaryTocEnabledAssets.Entries[0].Name).IsEqualTo("boid");
		await Assert.That(modBinaryTocEnabledAssets.Entries[0].AssetType).IsEqualTo(AssetType.Mesh);
		await Assert.That(AssetContainer.IsProhibited(modBinaryTocEnabledAssets.Entries[0].AssetType, modBinaryTocEnabledAssets.Entries[0].Name)).IsTrue();
	}

	[Test]
	[Arguments(false, "BOID")]
	[Arguments(false, "BOID2")]
	[Arguments(false, "Boid2")]
	[Arguments(false, "boiD2")]
	[Arguments(true, "boid")]
	[Arguments(true, "boid2")]
	public async Task TestIsEnabled(bool expectedIsEnabled, string name)
	{
		ModBinaryTocEntry entry = new(name, 0, 4, AssetType.Mesh);
		await Assert.That(entry.IsEnabled).IsEqualTo(expectedIsEnabled);
	}
}
