namespace DevilDaggersInfo.Core.Replay.Extensions;

public static class BinaryReaderExtensions
{
	public static Int16Vec3 ReadInt16Vec3(this BinaryReader br) => new(br.ReadInt16(), br.ReadInt16(), br.ReadInt16());

	public static Vector3 ReadVector3(this BinaryReader br) => new(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

	public static Int16Mat3x3 ReadInt16Mat3x3(this BinaryReader br) => new(br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16());

	public static Matrix3x3 ReadMatrix3x3(this BinaryReader br) => new(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
}
