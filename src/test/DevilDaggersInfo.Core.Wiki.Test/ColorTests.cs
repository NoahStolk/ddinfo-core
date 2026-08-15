namespace DevilDaggersInfo.Core.Wiki.Test;

internal sealed class ColorTests
{
	[Test]
	public async Task TestHexCode()
	{
		await Assert.That(new Color(0xFF, 0xFF, 0xFF).HexCode).IsEqualTo("#FFFFFF");
		await Assert.That(new Color(0x00, 0x00, 0x00).HexCode).IsEqualTo("#000000");
		await Assert.That(new Color(0x00, 0xA0, 0x00).HexCode).IsEqualTo("#00A000");
		await Assert.That(new Color(0x64, 0xAE, 0xB1).HexCode).IsEqualTo("#64AEB1");
	}
}
