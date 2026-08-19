namespace DevilDaggersInfo.Core.Replay.Extensions;

public static class BinaryWriterExtensions
{
	extension(BinaryWriter bw)
	{
		public void Write(Int16Vec3 vec3)
		{
			bw.Write(vec3.X);
			bw.Write(vec3.Y);
			bw.Write(vec3.Z);
		}

		public void Write(Vector3 vec3)
		{
			bw.Write(vec3.X);
			bw.Write(vec3.Y);
			bw.Write(vec3.Z);
		}

		public void Write(Int16Mat3x3 mat3x3)
		{
			bw.Write(mat3x3.M11);
			bw.Write(mat3x3.M12);
			bw.Write(mat3x3.M13);
			bw.Write(mat3x3.M21);
			bw.Write(mat3x3.M22);
			bw.Write(mat3x3.M23);
			bw.Write(mat3x3.M31);
			bw.Write(mat3x3.M32);
			bw.Write(mat3x3.M33);
		}

		public void Write(Matrix3x3 mat3x3)
		{
			bw.Write(mat3x3.M11);
			bw.Write(mat3x3.M12);
			bw.Write(mat3x3.M13);
			bw.Write(mat3x3.M21);
			bw.Write(mat3x3.M22);
			bw.Write(mat3x3.M23);
			bw.Write(mat3x3.M31);
			bw.Write(mat3x3.M32);
			bw.Write(mat3x3.M33);
		}
	}
}
