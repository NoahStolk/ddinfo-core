using DevilDaggersInfo.Core.Mod.Exceptions;
using DevilDaggersInfo.Core.Mod.Parsers;

namespace DevilDaggersInfo.Core.Mod.Test;

internal sealed class ObjParseTests
{
	[Test]
	[Arguments("3dsMax-claw.obj", 430, 368, 430, 784)]
	[Arguments("3dsMax-hand.obj", 274, 329, 274, 528)]
	[Arguments("Wings3d-cylinder.obj", 32, 32, 32, 60)]
	[Arguments("Wings3d-cube.obj", 8, 8, 8, 12)]
	public async Task ParseObj(string fileName, int sourcePositionCount, int sourceTexCoordCount, int sourceNormalCount, int sourceFaceCount)
	{
		ObjParsingContext parser = new();
		string objText = await File.ReadAllTextAsync(Path.Combine("Resources", "Mesh", fileName));
		ParsedObjData parsed = parser.Parse(objText);

		// TODO: Test counts more accurately.
		await Assert.That(sourcePositionCount <= parsed.Positions.Count).IsTrue();
		await Assert.That(sourceTexCoordCount <= parsed.TexCoords.Count).IsTrue();
		await Assert.That(sourceNormalCount <= parsed.Normals.Count).IsTrue();
		await Assert.That(sourceFaceCount <= parsed.Vertices.Count).IsTrue();
	}

	[Test]
	[Arguments("Wings3d-cube-invalid-face.obj")]
	[Arguments("Wings3d-cube-no-uv.obj")]
	public async Task ParseInvalidObj(string fileName)
	{
		ObjParsingContext parser = new();
		string objText = await File.ReadAllTextAsync(Path.Combine("Resources", "Mesh", fileName));
		Assert.ThrowsExactly<InvalidObjException>(() => parser.Parse(objText));
	}
}
