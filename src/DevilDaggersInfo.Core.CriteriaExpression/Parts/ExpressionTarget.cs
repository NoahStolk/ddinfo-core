using DevilDaggersInfo.Core.CriteriaExpression.Extensions;

namespace DevilDaggersInfo.Core.CriteriaExpression.Parts;

public sealed record ExpressionTarget(CustomLeaderboardCriteriaType Target) : IExpressionPart
{
	public override string ToString()
	{
		return Target.Display();
	}
}
