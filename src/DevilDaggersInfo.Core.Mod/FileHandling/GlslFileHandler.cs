namespace DevilDaggersInfo.Core.Mod.FileHandling;

internal static class GlslFileHandler
{
	public static byte[] Compile(string shaderName, byte[] vertexShaderFileContents, byte[] fragmentShaderFileContents)
	{
		using MemoryStream ms = new();
		using BinaryWriter bw = new(ms);

		bw.Write((uint)shaderName.Length);
		bw.Write((uint)vertexShaderFileContents.Length);
		bw.Write((uint)fragmentShaderFileContents.Length);

		bw.Write(Encoding.ASCII.GetBytes(shaderName));
		bw.Write(vertexShaderFileContents);
		bw.Write(fragmentShaderFileContents);

		return ms.ToArray();
	}

	public static ShaderData Extract(byte[] buffer)
	{
		using MemoryStream ms = new(buffer);
		using BinaryReader br = new(ms);

		int shaderNameLength = br.ReadInt32();
		int vertexBufferLength = br.ReadInt32();
		int fragmentBufferLength = br.ReadInt32();

		string shaderName = Encoding.ASCII.GetString(br.ReadBytes(shaderNameLength));
		byte[] vertexBuffer = br.ReadBytes(vertexBufferLength);
		byte[] fragmentBuffer = br.ReadBytes(fragmentBufferLength);

		return new(shaderName, vertexBuffer, fragmentBuffer);
	}
}
