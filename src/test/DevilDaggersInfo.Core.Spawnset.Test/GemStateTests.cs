namespace DevilDaggersInfo.Core.Spawnset.Test;

internal sealed class GemStateTests
{
	[Test]
	public async Task TestGemStateAdd()
	{
		GemState gemState = new(HandLevel.Level1, 0, 0);
		gemState = gemState.Add(5);
		await AssertGemState(gemState, HandLevel.Level1, 5);

		gemState = gemState.Add(5);
		await AssertGemState(gemState, HandLevel.Level2, 10);

		gemState = gemState.Add(55);
		await AssertGemState(gemState, HandLevel.Level2, 65);

		gemState = gemState.Add(10);
		await AssertGemState(gemState, HandLevel.Level3, 5);

		gemState = gemState.Add(200);
		await AssertGemState(gemState, HandLevel.Level4, 55);

		gemState = new GemState(HandLevel.Level1, 0, 0);
		gemState = gemState.Add(75);
		await AssertGemState(gemState, HandLevel.Level3, 5);
	}

	[AssertionMethod]
	private static async Task AssertGemState(GemState gemState, HandLevel expectedHandLevel, int expectedValue)
	{
		await Assert.That(gemState.HandLevel).IsEqualTo(expectedHandLevel);
		await Assert.That(gemState.Value).IsEqualTo(expectedValue);
	}
}
