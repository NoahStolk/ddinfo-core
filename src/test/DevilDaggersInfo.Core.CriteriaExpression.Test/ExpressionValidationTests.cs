using DevilDaggersInfo.Core.CriteriaExpression.Exceptions;
using DevilDaggersInfo.Core.CriteriaExpression.Parts;

namespace DevilDaggersInfo.Core.CriteriaExpression.Test;

internal sealed class ExpressionValidationTests
{
	[Test]
	public void TestValidExpressions()
	{
		TestExpression([new ExpressionValue(1), new ExpressionOperator(ExpressionOperatorType.Subtract), new ExpressionTarget(CustomLeaderboardCriteriaType.GemsCollected)]);
		TestExpression([new ExpressionValue(180), new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionTarget(CustomLeaderboardCriteriaType.GemsCollected), new ExpressionOperator(ExpressionOperatorType.Subtract), new ExpressionTarget(CustomLeaderboardCriteriaType.DaggersFired)]);
		TestExpression([new ExpressionValue(10), new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionValue(5)]);
		TestExpression([new ExpressionValue(20)]);
		TestExpression([new ExpressionTarget(CustomLeaderboardCriteriaType.GemsCollected)]);
	}

	[Test]
	public void TestInvalidExpressions()
	{
		Assert.ThrowsExactly<CriteriaExpressionParseException>(() => TestExpression([new ExpressionValue(1), new ExpressionOperator(ExpressionOperatorType.Subtract)]));
		Assert.ThrowsExactly<CriteriaExpressionParseException>(() => TestExpression([new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionValue(5), new ExpressionValue(10)]));
		Assert.ThrowsExactly<CriteriaExpressionParseException>(() => TestExpression([new ExpressionValue(20), new ExpressionValue(20)]));
		Assert.ThrowsExactly<CriteriaExpressionParseException>(() => TestExpression([new ExpressionOperator(ExpressionOperatorType.Subtract)]));
		Assert.ThrowsExactly<CriteriaExpressionParseException>(() => TestExpression([]));
		Assert.ThrowsExactly<CriteriaExpressionParseException>(() => TestExpression([new ExpressionTarget(CustomLeaderboardCriteriaType.Time), new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionValue(5)]));
		Assert.ThrowsExactly<CriteriaExpressionParseException>(() => TestExpression([new ExpressionTarget(CustomLeaderboardCriteriaType.Time)]));
		Assert.ThrowsExactly<CriteriaExpressionParseException>(() => TestExpression([new ExpressionTarget(CustomLeaderboardCriteriaType.DeathType)]));
		Assert.ThrowsExactly<CriteriaExpressionParseException>(() => TestExpression([new ExpressionTarget(CustomLeaderboardCriteriaType.LevelUpTime2)]));
	}

	private static void TestExpression(List<IExpressionPart> parts)
	{
		Expression expression = new(parts);
		expression.Validate();
	}
}
