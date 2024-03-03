namespace DevilDaggersInfo.Core.GameData;

public class Upgrade
{
	public required string Name { get; init; }

	public required Rgb Color { get; init; }

	public required UpgradeUnlockType UnlockType { get; init; }

	// TODO: Rewrite to union type if this ever gets added to C#.
	public required int GemsOrHomingRequired { get; init; }

	public required UpgradeAttack NormalAttack { get; init; }

	public required UpgradeAttack? HomingAttack { get; init; }
}
