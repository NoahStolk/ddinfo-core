using DevilDaggersInfo.Core.Asset;
using DevilDaggersInfo.Core.Mod.Builders;
using System.Text;

namespace DevilDaggersInfo.Core.Mod.Test;

[TestClass]
public class LoudnessTests
{
	[TestMethod]
	public void CreateModWithLoudness()
	{
		AudioModBinaryBuilder builder = new();
		builder.AddAudio("death", "RIFF"u8.ToArray(), 10f); // Default is 99.0.
		builder.AddAudio("fall", "RIFF"u8.ToArray(), 20f); // Default is 100.0.

		byte[] binary = builder.Compile();

		ModBinary modBinary = new(binary, ModBinaryReadFilter.AllAssets);
		Assert.AreEqual(3, modBinary.Toc.Entries.Count);
		Assert.IsTrue(modBinary.Toc.Entries.Any(e => e.Name == "death"));
		Assert.IsTrue(modBinary.Toc.Entries.Any(e => e.Name == "fall"));
		Assert.IsTrue(modBinary.Toc.Entries.Any(e => e.Name == "loudness"));
		Assert.AreEqual(3, modBinary.AssetMap.Count);
		Assert.IsTrue(modBinary.AssetMap.ContainsKey(new(AssetType.Audio, "death")));
		Assert.IsTrue(modBinary.AssetMap.ContainsKey(new(AssetType.Audio, "fall")));
		Assert.IsTrue(modBinary.AssetMap.ContainsKey(new(AssetType.Audio, "loudness")));

		AssetExtractionResult loudness = modBinary.ExtractAsset("loudness", AssetType.Audio);
		Assert.AreEqual(1, loudness.ExtractedAssetFiles.Count);
		Assert.IsTrue(loudness.ExtractedAssetFiles.TryGetValue("loudness.ini", out byte[]? loudnessIniContents));

		string loudnessIni = Encoding.ASCII.GetString(loudnessIniContents);
		string[] lines = loudnessIni.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
		Assert.AreEqual(AudioAudio.All.Count(a => a.PresentInDefaultLoudness), lines.Length);

		string deathLine = lines.First(l => l.StartsWith("death"));
		Assert.AreEqual("death = 10.0", deathLine);

		string fallLine = lines.First(l => l.StartsWith("fall"));
		Assert.AreEqual("fall = 20.0", fallLine);

		// Check some default audio assets.
		for (int i = 0; i < AudioAudio.All.Count; i++)
		{
			AudioAssetInfo audio = AudioAudio.All[i];
			if (audio.AssetName is "death" or "fall")
				continue;

			if (!audio.PresentInDefaultLoudness)
			{
				Assert.IsFalse(Array.Exists(lines, l => l.StartsWith(audio.AssetName)));
			}
			else
			{
				string line = lines.First(l => l.StartsWith(audio.AssetName));
				Assert.AreEqual($"{audio.AssetName} = {audio.DefaultLoudness:0.0}", line);
			}
		}
	}
}
