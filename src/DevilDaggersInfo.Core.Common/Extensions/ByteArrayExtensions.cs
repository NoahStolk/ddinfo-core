namespace DevilDaggersInfo.Core.Common.Extensions;

public static class ByteArrayExtensions
{
	// TODO: Move to Core.Encryption and include custom entry validation logic there as well.
	public static string ByteArrayToHexString(this byte[] byteArray)
	{
		return BitConverter.ToString(byteArray).Replace("-", string.Empty);
	}
}
