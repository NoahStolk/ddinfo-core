using DevilDaggersInfo.Core.Common;
using DevilDaggersInfo.Core.Wiki;

namespace DevilDaggersInfo.Core.CriteriaExpression.Parts;

public record ExpressionValue(int Value) : IExpressionPart
{
	public override string ToString()
	{
		return Value.ToString();
	}

	public string ToDisplayString(CustomLeaderboardCriteriaType criteriaType)
	{
		if (criteriaType == CustomLeaderboardCriteriaType.DeathType)
			return Deaths.GetDeathByType(GameConstants.CurrentVersion, (byte)Value)?.Name ?? "???";

		bool isTime = criteriaType is CustomLeaderboardCriteriaType.Time or CustomLeaderboardCriteriaType.LevelUpTime2 or CustomLeaderboardCriteriaType.LevelUpTime3 or CustomLeaderboardCriteriaType.LevelUpTime4;
		return isTime ? GameTime.FromGameUnits(Value).Seconds.ToString(StringFormats.TimeFormat) : Value.ToString();
	}
}
