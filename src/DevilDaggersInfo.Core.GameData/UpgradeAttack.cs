namespace DevilDaggersInfo.Core.GameData;

public sealed class UpgradeAttack
{
	internal UpgradeAttack(int daggersPerShot, float rapidDaggersPerSecond)
	{
		DaggersPerShot = daggersPerShot;
		RapidDaggersPerSecond = rapidDaggersPerSecond;
	}

	public int DaggersPerShot { get; }
	public float RapidDaggersPerSecond { get; }
}
