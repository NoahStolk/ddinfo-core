namespace DevilDaggersInfo.Core.Common.Test;

internal sealed class TimeTests
{
	[Test]
	public async Task TestTime()
	{
		await Assert.That(GameTime.FromGameUnits(10001998).Seconds).IsEqualTo(1000.1998);
		await Assert.That(GameTime.FromSeconds(1.0).Seconds).IsEqualTo(1.0);
		await Assert.That(GameTime.FromSeconds(1).Seconds).IsEqualTo(1.0);
		await Assert.That(GameTime.FromGameUnits(12345678901234UL).Seconds).IsEqualTo(1234567890.1234);
		await Assert.That(GameTime.FromGameUnits(12345678901234L).Seconds).IsEqualTo(1234567890.1234);
	}
}
