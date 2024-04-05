namespace DevilDaggersInfo.Core.Mod.Parsers;

internal sealed class ParsedObjData
{
	public List<Vector3> Positions { get; } = [];
	public List<Vector2> TexCoords { get; } = [];
	public List<Vector3> Normals { get; } = [];
	public List<VertexReference> Vertices { get; } = [];
}
