namespace DevilDaggersInfo.Core.Mod.Utils;

public static class MipmapUtils
{
	public static byte GetMipmapCount(int width, int height)
	{
		return (byte)(Math.Log(Math.Min(width, height), 2) + 1);
	}
}
