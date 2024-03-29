using DevilDaggersInfo.Core.CriteriaExpression.Parts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DevilDaggersInfo.Core.CriteriaExpression.Test;

[TestClass]
public class ExpressionBinaryTests
{
	[TestMethod]
	public void TestBinaryConversions()
	{
		TestExpression([new ExpressionValue(1), new ExpressionOperator(ExpressionOperatorType.Subtract), new ExpressionTarget(CustomLeaderboardCriteriaType.GemsCollected)]);
		TestExpression([new ExpressionValue(180), new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionTarget(CustomLeaderboardCriteriaType.GemsCollected), new ExpressionOperator(ExpressionOperatorType.Subtract), new ExpressionTarget(CustomLeaderboardCriteriaType.DaggersFired)]);
		TestExpression([new ExpressionValue(10), new ExpressionOperator(ExpressionOperatorType.Add), new ExpressionValue(5)]);
		TestExpression([new ExpressionValue(20)]);

		static void TestExpression(List<IExpressionPart> parts)
		{
			Expression expression = new(parts);
			expression.Validate();

			byte[] bytes = expression.ToBytes();
			Assert.IsTrue(Expression.TryParse(bytes, out Expression? expressionParsed));
			CollectionAssert.AreEqual(bytes, expressionParsed.ToBytes());
		}
	}
}
