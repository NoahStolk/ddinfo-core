namespace DevilDaggersInfo.Core.Encryption.Test;

internal sealed class AesBase32WrapperTests
{
	// The expected values in this class are known-answer vectors produced by the implementation as it existed on .NET 8.
	// They are hard-coded on purpose: a round-trip test alone would still pass if key derivation changed, because both
	// directions would shift together. These vectors pin the actual ciphertext, so any change to the key derivation,
	// cipher mode, padding, or Base32 alphabet fails the build. Do not regenerate them to make a failing test pass.
	private const string _initializationVector = "0123456789ABCDEF";
	private const string _password = "DevilDaggersTest";
	private const string _salt = "TestSalt";

	[Test]
	[Arguments("a", "2PEDEIJGARQQXCV5EYXTZD3QGQ======")]
	[Arguments("hello world", "C7MJYAK7YUKO4YJB656277KDPE======")]
	[Arguments("1234567890123456", "GSFP3VAILVEXMN6GXJWHPNQDYM7B32CFDZIEQ5N4XIQ7DCEWCBDQ====")]
	[Arguments("ダガー", "QKFXEEGHJ5FB5MYTA6RYAMNAN4======")]
	[Arguments("12345678|123.4567|9|3|Devil Daggers", "TN52GH7CYWBKS6ARQ5DZHLXK7YITGG6Z7GL7ELEIR4FXKGER6OWGGQOEE7RPOBGUIFTH24RLTOUUG===")]
	public async Task TestEncryptAndEncode(string input, string expected)
	{
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		await Assert.That(wrapper.EncryptAndEncode(input)).IsEqualTo(expected);
	}

	[Test]
	[Arguments("2PEDEIJGARQQXCV5EYXTZD3QGQ======", "a")]
	[Arguments("C7MJYAK7YUKO4YJB656277KDPE======", "hello world")]
	[Arguments("GSFP3VAILVEXMN6GXJWHPNQDYM7B32CFDZIEQ5N4XIQ7DCEWCBDQ====", "1234567890123456")]
	[Arguments("QKFXEEGHJ5FB5MYTA6RYAMNAN4======", "ダガー")]
	[Arguments("TN52GH7CYWBKS6ARQ5DZHLXK7YITGG6Z7GL7ELEIR4FXKGER6OWGGQOEE7RPOBGUIFTH24RLTOUUG===", "12345678|123.4567|9|3|Devil Daggers")]
	public async Task TestDecodeAndDecrypt(string input, string expected)
	{
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		await Assert.That(wrapper.DecodeAndDecrypt(input)).IsEqualTo(expected);
	}

	[Test]
	public async Task TestEmptyString()
	{
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		string encrypted = wrapper.EncryptAndEncode(string.Empty);
		await Assert.That(encrypted).IsEqualTo("2LIDZ346CEFCBLRL2YK6ZHN2WM======");
		await Assert.That(wrapper.DecodeAndDecrypt(encrypted)).IsEqualTo(string.Empty);
	}

	[Test]
	public async Task TestRoundTrip()
	{
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		const string input = "The quick brown fox jumps over the lazy dog.";
		await Assert.That(wrapper.DecodeAndDecrypt(wrapper.EncryptAndEncode(input))).IsEqualTo(input);
	}

	[Test]
	public async Task TestEncryptionIsDeterministic()
	{
		// The initialization vector is fixed rather than random, so encrypting the same input twice must produce the same output.
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		await Assert.That(wrapper.EncryptAndEncode("hello world")).IsEqualTo(wrapper.EncryptAndEncode("hello world"));
	}

	[Test]
	public async Task TestDifferentParametersProduceDifferentOutput()
	{
		// Guards against a refactor that silently stops feeding the password or salt into key derivation.
		const string input = "hello world";
		string baseline = new AesBase32Wrapper(_initializationVector, _password, _salt).EncryptAndEncode(input);

		await Assert.That(new AesBase32Wrapper("FEDCBA9876543210", _password, _salt).EncryptAndEncode(input)).IsNotEqualTo(baseline);
		await Assert.That(new AesBase32Wrapper(_initializationVector, "DifferentPassword", _salt).EncryptAndEncode(input)).IsNotEqualTo(baseline);
		await Assert.That(new AesBase32Wrapper(_initializationVector, _password, "DifferentSalt").EncryptAndEncode(input)).IsNotEqualTo(baseline);
	}

	[Test]
	public void TestDecryptingWithWrongPasswordThrows()
	{
		string encrypted = new AesBase32Wrapper(_initializationVector, _password, _salt).EncryptAndEncode("hello world");
		AesBase32Wrapper wrongWrapper = new(_initializationVector, "WrongPassword", _salt);
		Assert.ThrowsExactly<System.Security.Cryptography.CryptographicException>(() => wrongWrapper.DecodeAndDecrypt(encrypted));
	}
}
