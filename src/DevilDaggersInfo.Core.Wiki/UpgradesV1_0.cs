namespace DevilDaggersInfo.Core.Wiki;

#pragma warning disable CA1707
// ReSharper disable once InconsistentNaming
public static class UpgradesV1_0
#pragma warning restore CA1707
{
	public static readonly Upgrade Level1 = new(GameVersion.V1_0, "Level 1", UpgradeColors.Level1, 1, new Damage(10, 20f), null, new UpgradeUnlock(UpgradeUnlockType.Gems, 0));
	public static readonly Upgrade Level2 = new(GameVersion.V1_0, "Level 2", UpgradeColors.Level2, 2, new Damage(20, 40f), null, new UpgradeUnlock(UpgradeUnlockType.Gems, 10));
	public static readonly Upgrade Level3 = new(GameVersion.V1_0, "Level 3", UpgradeColors.Level3, 3, new Damage(40, 80f), new Damage(40, 40), new UpgradeUnlock(UpgradeUnlockType.Gems, 70));

	internal static readonly IReadOnlyList<Upgrade> All = new List<Upgrade>
	{
		Level1,
		Level2,
		Level3,
	};
}
