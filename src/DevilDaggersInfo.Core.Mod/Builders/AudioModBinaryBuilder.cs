using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod.Builders;

public sealed class AudioModBinaryBuilder : ModBinaryBuilder
{
	public override ModBinaryType Type => ModBinaryType.Audio;

	public void AddAudio(string assetName, byte[] wavFileContents)
	{
		AssetKey key = new(AssetType.Audio, assetName);
		ValidateAsset(key);
		_assetMap.Add(key, new(wavFileContents));
		RebuildToc();

		// TODO: Keep list of loudness values.
	}

	public override byte[] Compile()
	{
		// TODO: Add loudness values to the end of the mod binary.

		return base.Compile();
	}
}
