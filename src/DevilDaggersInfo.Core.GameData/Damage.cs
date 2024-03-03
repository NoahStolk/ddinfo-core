namespace DevilDaggersInfo.Core.GameData;

public class Damage
{
	public required Dagger Dagger { get; init; }

	public required Enemy Enemy { get; init; }

	public required float DamagePercentageToDagger { get; init; }

	public required int DamageToEnemy { get; init; }
}
