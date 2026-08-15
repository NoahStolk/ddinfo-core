using DevilDaggersInfo.Core.Mod.Extensions;

namespace DevilDaggersInfo.Core.Mod.Test;

internal sealed class NullTerminatorTests
{
	[Test]
	public async Task TestNullTerminatedStrings()
	{
		byte[] buffer = [0x68, 0x61, 0x6E, 0x64, 0, 0x64, 0x64, 0];

		using MemoryStream ms = new(buffer);
		using BinaryReader br = new(ms);
		string hand = br.ReadNullTerminatedString();
		await Assert.That(hand).IsEqualTo("hand");
		string dd = br.ReadNullTerminatedString();
		await Assert.That(dd).IsEqualTo("dd");
	}
}
