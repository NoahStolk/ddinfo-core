namespace DevilDaggersInfo.Core.Encryption.Test;

[TestClass]
public class AesBase32WrapperTests
{
	// The expected values in this class are known-answer vectors produced by the implementation as it existed on .NET 8.
	// They are hard-coded on purpose: a round-trip test alone would still pass if key derivation changed, because both
	// directions would shift together. These vectors pin the actual ciphertext, so any change to the key derivation,
	// cipher mode, padding, or Base32 alphabet fails the build. Do not regenerate them to make a failing test pass.
	private const string _initializationVector = "0123456789ABCDEF";
	private const string _password = "DevilDaggersTest";
	private const string _salt = "TestSalt";

	[DataTestMethod]
	[DataRow("a", "2PEDEIJGARQQXCV5EYXTZD3QGQ======")]
	[DataRow("hello world", "C7MJYAK7YUKO4YJB656277KDPE======")]
	[DataRow("1234567890123456", "GSFP3VAILVEXMN6GXJWHPNQDYM7B32CFDZIEQ5N4XIQ7DCEWCBDQ====")]
	[DataRow("ダガー", "QKFXEEGHJ5FB5MYTA6RYAMNAN4======")]
	[DataRow("12345678|123.4567|9|3|Devil Daggers", "TN52GH7CYWBKS6ARQ5DZHLXK7YITGG6Z7GL7ELEIR4FXKGER6OWGGQOEE7RPOBGUIFTH24RLTOUUG===")]
	public void TestEncryptAndEncode(string input, string expected)
	{
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		Assert.AreEqual(expected, wrapper.EncryptAndEncode(input));
	}

	[DataTestMethod]
	[DataRow("2PEDEIJGARQQXCV5EYXTZD3QGQ======", "a")]
	[DataRow("C7MJYAK7YUKO4YJB656277KDPE======", "hello world")]
	[DataRow("GSFP3VAILVEXMN6GXJWHPNQDYM7B32CFDZIEQ5N4XIQ7DCEWCBDQ====", "1234567890123456")]
	[DataRow("QKFXEEGHJ5FB5MYTA6RYAMNAN4======", "ダガー")]
	[DataRow("TN52GH7CYWBKS6ARQ5DZHLXK7YITGG6Z7GL7ELEIR4FXKGER6OWGGQOEE7RPOBGUIFTH24RLTOUUG===", "12345678|123.4567|9|3|Devil Daggers")]
	public void TestDecodeAndDecrypt(string input, string expected)
	{
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		Assert.AreEqual(expected, wrapper.DecodeAndDecrypt(input));
	}

	[TestMethod]
	public void TestEmptyString()
	{
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		string encrypted = wrapper.EncryptAndEncode(string.Empty);
		Assert.AreEqual("2LIDZ346CEFCBLRL2YK6ZHN2WM======", encrypted);
		Assert.AreEqual(string.Empty, wrapper.DecodeAndDecrypt(encrypted));
	}

	[TestMethod]
	public void TestRoundTrip()
	{
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		const string input = "The quick brown fox jumps over the lazy dog.";
		Assert.AreEqual(input, wrapper.DecodeAndDecrypt(wrapper.EncryptAndEncode(input)));
	}

	[TestMethod]
	public void TestEncryptionIsDeterministic()
	{
		// The initialization vector is fixed rather than random, so encrypting the same input twice must produce the same output.
		AesBase32Wrapper wrapper = new(_initializationVector, _password, _salt);
		Assert.AreEqual(wrapper.EncryptAndEncode("hello world"), wrapper.EncryptAndEncode("hello world"));
	}

	[TestMethod]
	public void TestDifferentParametersProduceDifferentOutput()
	{
		// Guards against a refactor that silently stops feeding the password or salt into key derivation.
		const string input = "hello world";
		string baseline = new AesBase32Wrapper(_initializationVector, _password, _salt).EncryptAndEncode(input);

		Assert.AreNotEqual(baseline, new AesBase32Wrapper("FEDCBA9876543210", _password, _salt).EncryptAndEncode(input));
		Assert.AreNotEqual(baseline, new AesBase32Wrapper(_initializationVector, "DifferentPassword", _salt).EncryptAndEncode(input));
		Assert.AreNotEqual(baseline, new AesBase32Wrapper(_initializationVector, _password, "DifferentSalt").EncryptAndEncode(input));
	}

	[TestMethod]
	public void TestDecryptingWithWrongPasswordThrows()
	{
		string encrypted = new AesBase32Wrapper(_initializationVector, _password, _salt).EncryptAndEncode("hello world");
		AesBase32Wrapper wrongWrapper = new(_initializationVector, "WrongPassword", _salt);
		Assert.ThrowsException<System.Security.Cryptography.CryptographicException>(() => wrongWrapper.DecodeAndDecrypt(encrypted));
	}
}
