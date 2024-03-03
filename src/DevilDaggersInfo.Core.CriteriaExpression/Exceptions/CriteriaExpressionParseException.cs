namespace DevilDaggersInfo.Core.CriteriaExpression.Exceptions;

public class CriteriaExpressionParseException : Exception
{
	public CriteriaExpressionParseException()
	{
	}

	public CriteriaExpressionParseException(string? message)
		: base(message)
	{
	}

	public CriteriaExpressionParseException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
