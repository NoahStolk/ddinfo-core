namespace DevilDaggersInfo.Core.GameData;

public class UpgradeAttack
{
	public UpgradeAttack(int daggersPerShot, float rapidDaggersPerSecond)
	{
		DaggersPerShot = daggersPerShot;
		RapidDaggersPerSecond = rapidDaggersPerSecond;
	}

	public int DaggersPerShot { get; }
	public float RapidDaggersPerSecond { get; }
}
