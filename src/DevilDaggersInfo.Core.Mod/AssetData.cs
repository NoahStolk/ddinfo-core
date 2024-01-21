namespace DevilDaggersInfo.Core.Mod;

/// <summary>
/// Represents the raw DD data of the asset. This is the data that is written directly to the mod binary and does not correspond to formats such as PNG or OBJ.
/// </summary>
public class AssetData
{
	public AssetData(byte[] buffer)
	{
		Buffer = buffer;
	}

	public byte[] Buffer { get; }
}
