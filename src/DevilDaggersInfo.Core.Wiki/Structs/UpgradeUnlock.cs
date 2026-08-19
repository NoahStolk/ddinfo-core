namespace DevilDaggersInfo.Core.Wiki.Structs;

public readonly record struct UpgradeUnlock(UpgradeUnlockType UpgradeUnlockType, int Value)
{
	public override string ToString()
	{
		return $"{Value} {(UpgradeUnlockType == UpgradeUnlockType.Gems ? "gems" : "homing daggers")}";
	}
}
