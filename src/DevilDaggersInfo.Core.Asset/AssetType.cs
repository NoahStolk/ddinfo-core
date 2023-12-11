namespace DevilDaggersInfo.Core.Asset;

#pragma warning disable CA1027 // Mark enums with FlagsAttribute. Reason: Not a flag enum.
#pragma warning disable CA1008 // Enums should have zero value. Reason: Not applicable.
public enum AssetType : byte
#pragma warning restore CA1008, CA1027
{
	Mesh = 0x01,
	Texture = 0x02,
	Shader = 0x10,
	Audio = 0x20,
	ObjectBinding = 0x80,
}
