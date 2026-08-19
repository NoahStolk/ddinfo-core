namespace DevilDaggersInfo.Core.Replay.Extensions;

public static class BinaryReaderExtensions
{
	extension(BinaryReader br)
	{
		public Int16Vec3 ReadInt16Vec3()
		{
			return new Int16Vec3(br.ReadInt16(), br.ReadInt16(), br.ReadInt16());
		}

		public Vector3 ReadVector3()
		{
			return new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
		}

		public Int16Mat3x3 ReadInt16Mat3x3()
		{
			return new Int16Mat3x3(br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16(), br.ReadInt16());
		}

		public Matrix3x3 ReadMatrix3x3()
		{
			return new Matrix3x3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
		}
	}
}
