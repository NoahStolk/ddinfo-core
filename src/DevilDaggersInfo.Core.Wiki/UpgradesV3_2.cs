namespace DevilDaggersInfo.Core.Wiki;

#pragma warning disable CA1707
// ReSharper disable once InconsistentNaming
public static class UpgradesV3_2
#pragma warning restore CA1707
{
	public static readonly Upgrade Level1 = new(GameVersion.V3_2, "Level 1", UpgradeColors.Level1, 1, new Damage(10, 20f), null, new UpgradeUnlock(UpgradeUnlockType.Gems, 0));
	public static readonly Upgrade Level2 = new(GameVersion.V3_2, "Level 2", UpgradeColors.Level2, 2, new Damage(20, 40f), null, new UpgradeUnlock(UpgradeUnlockType.Gems, 10));
	public static readonly Upgrade Level3 = new(GameVersion.V3_2, "Level 3", UpgradeColors.Level3, 3, new Damage(40, 80f), new Damage(20, 40), new UpgradeUnlock(UpgradeUnlockType.Gems, 70));
	public static readonly Upgrade Level4 = new(GameVersion.V3_2, "Level 4", UpgradeColors.Level4, 4, new Damage(60, 106.666f), new Damage(30, 40), new UpgradeUnlock(UpgradeUnlockType.Homing, 150));

	internal static readonly IReadOnlyList<Upgrade> All = new List<Upgrade>
	{
		Level1,
		Level2,
		Level3,
		Level4,
	};
}
