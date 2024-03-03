namespace DevilDaggersInfo.Core.Replay.Exceptions;

public class InvalidReplayBinaryException : Exception
{
	public InvalidReplayBinaryException()
	{
	}

	public InvalidReplayBinaryException(string? message)
		: base(message)
	{
	}

	public InvalidReplayBinaryException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
