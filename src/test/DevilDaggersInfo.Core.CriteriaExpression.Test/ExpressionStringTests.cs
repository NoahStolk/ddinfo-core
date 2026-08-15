using DevilDaggersInfo.Core.CriteriaExpression.Parts;

namespace DevilDaggersInfo.Core.CriteriaExpression.Test;

internal sealed class ExpressionStringTests
{
	[Test]
	public async Task TestStringConversions()
	{
		await TestExpression("1 - Gems collected", [new ExpressionValue(1), new ExpressionOperator(ExpressionOperatorType.Subtract), new ExpressionTarget(CustomLeaderboardCriteriaType.GemsCollected)]);
		await TestExpression("180 + Gems collected - Daggers fired", [new ExpressionValue(180), new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionTarget(CustomLeaderboardCriteriaType.GemsCollected), new ExpressionOperator(ExpressionOperatorType.Subtract), new ExpressionTarget(CustomLeaderboardCriteriaType.DaggersFired)]);
		await TestExpression("10 + 5", [new ExpressionValue(10), new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionValue(5)]);
		await TestExpression("20", [new ExpressionValue(20)]);

		static async Task TestExpression(string expectedString, List<IExpressionPart> parts)
		{
			Expression expression = new(parts);
			expression.Validate();

			await Assert.That(expression.ToString()).IsEqualTo(expectedString);
			await Assert.That(ContainsSameParts(expression, Expression.Parse(expectedString))).IsTrue();
		}

		static bool ContainsSameParts(Expression a, Expression b)
		{
			if (a.Parts.Count != b.Parts.Count)
				return false;

			for (int i = 0; i < a.Parts.Count; i++)
			{
				// Cannot compare directly because instances of IExpressionPart will never be equal (reference comparison). Casting to the relevant records is required for the equality contracts to work.
				IExpressionPart aPart = a.Parts[i];
				IExpressionPart bPart = b.Parts[i];

				switch (aPart)
				{
					case ExpressionValue aValue when bPart is not ExpressionValue bValue || aValue != bValue:
					case ExpressionOperator aOperator when bPart is not ExpressionOperator bOperator || aOperator != bOperator:
					case ExpressionTarget aTarget when bPart is not ExpressionTarget bTarget || aTarget != bTarget:
						return false;
				}
			}

			return true;
		}
	}
}
