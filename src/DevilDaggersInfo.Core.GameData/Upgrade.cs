namespace DevilDaggersInfo.Core.GameData;

public class Upgrade
{
	internal Upgrade(string name, Rgb color, UpgradeUnlockType unlockType, int gemsOrHomingRequired, UpgradeAttack normalAttack, UpgradeAttack? homingAttack)
	{
		Name = name;
		Color = color;
		UnlockType = unlockType;
		GemsOrHomingRequired = gemsOrHomingRequired;
		NormalAttack = normalAttack;
		HomingAttack = homingAttack;
	}

	public string Name { get; }
	public Rgb Color { get; }
	public UpgradeUnlockType UnlockType { get; }
	public int GemsOrHomingRequired { get; } // TODO: Rewrite to union type if this ever gets added to C#.
	public UpgradeAttack NormalAttack { get; }
	public UpgradeAttack? HomingAttack { get; }
}
