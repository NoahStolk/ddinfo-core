using DevilDaggersInfo.Core.Asset;
using DevilDaggersInfo.Core.Mod.Builders;
using System.Text;

namespace DevilDaggersInfo.Core.Mod.Test;

internal sealed class LoudnessTests
{
	[Test]
	public async Task CreateModWithLoudness()
	{
		AudioModBinaryBuilder builder = new();
		builder.AddAudio("death", "RIFF"u8.ToArray(), 10f); // Default is 99.0.
		builder.AddAudio("fall", "RIFF"u8.ToArray(), 20f); // Default is 100.0.

		byte[] binary = builder.Compile();

		ModBinary modBinary = new(binary, ModBinaryReadFilter.AllAssets);
		await Assert.That(modBinary.Toc.Entries.Count).IsEqualTo(3);
		await Assert.That(modBinary.Toc.Entries.Any(e => e.Name == "death")).IsTrue();
		await Assert.That(modBinary.Toc.Entries.Any(e => e.Name == "fall")).IsTrue();
		await Assert.That(modBinary.Toc.Entries.Any(e => e.Name == "loudness")).IsTrue();
		await Assert.That(modBinary.AssetMap.Count).IsEqualTo(3);
		await Assert.That(modBinary.AssetMap.ContainsKey(new(AssetType.Audio, "death"))).IsTrue();
		await Assert.That(modBinary.AssetMap.ContainsKey(new(AssetType.Audio, "fall"))).IsTrue();
		await Assert.That(modBinary.AssetMap.ContainsKey(new(AssetType.Audio, "loudness"))).IsTrue();

		AssetExtractionResult loudness = modBinary.ExtractAsset("loudness", AssetType.Audio);
		await Assert.That(loudness.ExtractedAssetFiles.Count).IsEqualTo(1);
		await Assert.That(loudness.ExtractedAssetFiles.ContainsKey("loudness.ini")).IsTrue();

		string loudnessIni = Encoding.ASCII.GetString(loudness.ExtractedAssetFiles["loudness.ini"]);
		string[] lines = loudnessIni.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
		await Assert.That(lines.Length).IsEqualTo(AudioAudio.All.Count(a => a.PresentInDefaultLoudness));

		string deathLine = lines.First(l => l.StartsWith("death"));
		await Assert.That(deathLine).IsEqualTo("death = 10.0");

		string fallLine = lines.First(l => l.StartsWith("fall"));
		await Assert.That(fallLine).IsEqualTo("fall = 20.0");

		// Check some default audio assets.
		for (int i = 0; i < AudioAudio.All.Count; i++)
		{
			AudioAssetInfo audio = AudioAudio.All[i];
			if (audio.AssetName is "death" or "fall")
				continue;

			if (!audio.PresentInDefaultLoudness)
			{
				await Assert.That(Array.Exists(lines, l => l.StartsWith(audio.AssetName))).IsFalse();
			}
			else
			{
				string line = lines.First(l => l.StartsWith(audio.AssetName));
				await Assert.That(line).IsEqualTo($"{audio.AssetName} = {audio.DefaultLoudness:0.0}");
			}
		}
	}
}
