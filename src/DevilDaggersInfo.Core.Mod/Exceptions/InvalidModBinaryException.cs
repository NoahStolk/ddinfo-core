namespace DevilDaggersInfo.Core.Mod.Exceptions;

public class InvalidModBinaryException : Exception
{
	public InvalidModBinaryException()
	{
	}

	public InvalidModBinaryException(string? message)
		: base(message)
	{
	}

	public InvalidModBinaryException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
