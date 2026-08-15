using DevilDaggersInfo.Core.CriteriaExpression.Parts;
using TUnit.Assertions.Enums;

namespace DevilDaggersInfo.Core.CriteriaExpression.Test;

internal sealed class ExpressionBinaryTests
{
	[Test]
	public async Task TestBinaryConversions()
	{
		await TestExpression([new ExpressionValue(1), new ExpressionOperator(ExpressionOperatorType.Subtract), new ExpressionTarget(CustomLeaderboardCriteriaType.GemsCollected)]);
		await TestExpression([new ExpressionValue(180), new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionTarget(CustomLeaderboardCriteriaType.GemsCollected), new ExpressionOperator(ExpressionOperatorType.Subtract), new ExpressionTarget(CustomLeaderboardCriteriaType.DaggersFired)]);
		await TestExpression([new ExpressionValue(10), new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionValue(5)]);
		await TestExpression([new ExpressionValue(20)]);

		static async Task TestExpression(List<IExpressionPart> parts)
		{
			Expression expression = new(parts);
			expression.Validate();

			byte[] bytes = expression.ToBytes();
			await Assert.That(Expression.TryParse(bytes, out Expression? expressionParsed)).IsTrue();
			await Assert.That(expressionParsed).IsNotNull();
			await Assert.That(expressionParsed?.ToBytes()).IsEquivalentTo(bytes, CollectionOrdering.Matching);
		}
	}
}
