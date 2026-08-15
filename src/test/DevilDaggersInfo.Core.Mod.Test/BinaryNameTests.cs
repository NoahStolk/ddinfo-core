namespace DevilDaggersInfo.Core.Mod.Test;

internal sealed class BinaryNameTests
{
	[Test]
	[Arguments(ModBinaryType.Audio, "mod", "main", "audio-mod-main")]
	[Arguments(ModBinaryType.Dd, "mod", "testbinary", "dd-mod-testbinary")]
	[Arguments(ModBinaryType.Dd, "mod", "mod", "dd-mod-mod")]
	[Arguments(ModBinaryType.Dd, "", "main", "dd--main")]
	[Arguments(ModBinaryType.Dd, "mod", "m", "dd-mod-m")]
	[Arguments(ModBinaryType.Dd, "m", "mod", "dd-m-mod")]
	public async Task TestBinaryNames(ModBinaryType modBinaryType, string modName, string name, string expectedFullName)
	{
		BinaryName binaryName = new(modBinaryType, name);
		string fullName = binaryName.ToFullName(modName);

		await Assert.That(expectedFullName).IsEqualTo(fullName);
		await Assert.That(BinaryName.Parse(fullName, modName)).IsEqualTo(binaryName);
	}
}
