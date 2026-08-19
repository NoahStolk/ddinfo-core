namespace DevilDaggersInfo.Core.Mod.Extensions;

public static class BinaryReaderExtensions
{
	extension(BinaryReader binaryReader)
	{
		public string ReadNullTerminatedString()
		{
			StringBuilder sb = new();
			while (true)
			{
				byte b = binaryReader.ReadByte();
				if (b == 0x00)
					break;
				sb.Append((char)b);
			}

			return sb.ToString();
		}

		public Vertex ReadVertex()
		{
			Vector3 position = new(
				x: binaryReader.ReadSingle(),
				y: binaryReader.ReadSingle(),
				z: binaryReader.ReadSingle());
			Vector3 normal = new(
				x: binaryReader.ReadSingle(),
				y: binaryReader.ReadSingle(),
				z: binaryReader.ReadSingle());
			Vector2 texCoord = new(
				x: binaryReader.ReadSingle(),
				y: binaryReader.ReadSingle());
			return new Vertex(position, normal, texCoord);
		}
	}
}
