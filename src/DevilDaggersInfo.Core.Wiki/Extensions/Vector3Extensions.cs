using System.Numerics;

namespace DevilDaggersInfo.Core.Wiki.Extensions;

internal static class Vector3Extensions
{
	extension(Vector3)
	{
		public static Vector3 CreateFromRgb(byte r, byte g, byte b)
		{
			return new Vector3(r / 255f, g / 255f, b / 255f);
		}
	}
}
