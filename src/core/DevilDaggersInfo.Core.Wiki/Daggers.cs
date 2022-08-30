namespace DevilDaggersInfo.Core.Wiki;

public static class Daggers
{
	public static IReadOnlyList<Dagger> GetDaggers(GameVersion gameVersion) => gameVersion switch
	{
		GameVersion.V1_0 => DaggersV1_0.All,
		GameVersion.V2_0 => DaggersV2_0.All,
		GameVersion.V3_0 => DaggersV3_0.All,
		GameVersion.V3_1 => DaggersV3_1.All,
		GameVersion.V3_2 => DaggersV3_2.All,
		_ => throw new ArgumentOutOfRangeException(nameof(gameVersion)),
	};

	public static Dagger? GetDaggerByName(string name)
	{
		Dagger dagger = GetDaggers(GameConstants.CurrentVersion).FirstOrDefault(d => d.Name == name);
		return dagger == default ? null : dagger;
	}

	public static Dagger GetDaggerFromSeconds(GameVersion gameVersion, double timeInSeconds)
	{
		IReadOnlyList<Dagger> daggers = GetDaggers(gameVersion);
		for (int i = daggers.Count - 1; i >= 0; i--)
		{
			if (timeInSeconds >= daggers[i].UnlockSecond)
				return daggers[i];
		}

		throw new ArgumentOutOfRangeException(nameof(timeInSeconds), $"Could not determine dagger based on time '{timeInSeconds:0.0000}'.");
	}
}
