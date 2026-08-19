using DevilDaggersInfo.Core.Mod.Extensions;

namespace DevilDaggersInfo.Core.Mod.Test;

internal sealed class NullTerminatorTests
{
	[Test]
	public async Task TestNullTerminatedStrings()
	{
		byte[] buffer = [.. "hand\0dd\0"u8];

		await using MemoryStream ms = new(buffer);
		using BinaryReader br = new(ms);
		string hand = br.ReadNullTerminatedString();
		await Assert.That(hand).IsEqualTo("hand");
		string dd = br.ReadNullTerminatedString();
		await Assert.That(dd).IsEqualTo("dd");
	}
}
