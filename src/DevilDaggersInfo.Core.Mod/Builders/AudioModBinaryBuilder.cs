using DevilDaggersInfo.Core.Asset;

namespace DevilDaggersInfo.Core.Mod.Builders;

public sealed class AudioModBinaryBuilder : ModBinaryBuilder
{
	private readonly Dictionary<string, float> _loudnessValues = [];

	public override ModBinaryType Type => ModBinaryType.Audio;

	public void AddAudio(string assetName, byte[] wavFileContents, float? loudnessValue)
	{
		AssetKey key = new(AssetType.Audio, assetName);
		ValidateAsset(key);
		_assetMap.Add(key, new(wavFileContents));
		RebuildToc();

		if (loudnessValue.HasValue)
			_loudnessValues[assetName] = loudnessValue.Value;
	}

	public override byte[] Compile()
	{
		// If any audio asset is included in this list, we need to create a loudness asset as well.
		if (_loudnessValues.Count > 0)
			AddLoudnessChunk();

		return base.Compile();
	}

	private void AddLoudnessChunk()
	{
		// Any missing audio will need to have its default loudness included, otherwise the game will play those with loudness 1.0.
		foreach (AudioAssetInfo audioAsset in AudioAudio.All)
		{
			// Only add it to the list if it is present in the default loudness file, otherwise the game will detect prohibited mods.
			if (audioAsset.PresentInDefaultLoudness && !_loudnessValues.ContainsKey(audioAsset.AssetName))
				_loudnessValues.Add(audioAsset.AssetName, audioAsset.DefaultLoudness);
		}

		StringBuilder loudness = new();
		foreach (KeyValuePair<string, float> kvp in _loudnessValues)
			loudness.Append(kvp.Key).Append(" = ").AppendFormat("{0:0.0}", kvp.Value).AppendLine();

		AssetKey key = new(AssetType.Audio, "loudness");
		ValidateAsset(key);
		_assetMap.Add(key, new(Encoding.ASCII.GetBytes(loudness.ToString())));
		RebuildToc();
	}
}
