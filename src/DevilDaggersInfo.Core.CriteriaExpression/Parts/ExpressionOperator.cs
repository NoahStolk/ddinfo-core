namespace DevilDaggersInfo.Core.CriteriaExpression.Parts;

public record ExpressionOperator(ExpressionOperatorType Operator) : IExpressionPart
{
	public override string ToString()
	{
		return Operator switch
		{
			ExpressionOperatorType.Add => "+",
			ExpressionOperatorType.Subtract => "-",
			_ => $"Invalid operator '{Operator}'",
		};
	}
}
