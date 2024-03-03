namespace DevilDaggersInfo.Core.Mod.Exceptions;

public class InvalidObjException : Exception
{
	public InvalidObjException()
	{
	}

	public InvalidObjException(string? message)
		: base(message)
	{
	}

	public InvalidObjException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
