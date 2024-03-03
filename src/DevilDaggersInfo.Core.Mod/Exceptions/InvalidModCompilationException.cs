namespace DevilDaggersInfo.Core.Mod.Exceptions;

public class InvalidModCompilationException : Exception
{
	public InvalidModCompilationException()
	{
	}

	public InvalidModCompilationException(string? message)
		: base(message)
	{
	}

	public InvalidModCompilationException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
