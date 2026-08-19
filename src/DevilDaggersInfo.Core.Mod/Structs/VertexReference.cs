namespace DevilDaggersInfo.Core.Mod.Structs;

public readonly record struct VertexReference(int PositionReference, int TexCoordReference, int NormalReference)
{
	public VertexReference(int unifiedReference)
		: this(unifiedReference, unifiedReference, unifiedReference)
	{
	}

	public override string ToString()
	{
		if (PositionReference == TexCoordReference && PositionReference == NormalReference)
			return PositionReference.ToString();

		return $"{PositionReference}/{TexCoordReference}/{NormalReference}";
	}
}
