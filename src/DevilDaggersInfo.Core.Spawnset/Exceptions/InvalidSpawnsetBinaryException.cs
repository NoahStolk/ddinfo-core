namespace DevilDaggersInfo.Core.Spawnset.Exceptions;

public class InvalidSpawnsetBinaryException : Exception
{
	public InvalidSpawnsetBinaryException()
	{
	}

	public InvalidSpawnsetBinaryException(string? message)
		: base(message)
	{
	}

	public InvalidSpawnsetBinaryException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
