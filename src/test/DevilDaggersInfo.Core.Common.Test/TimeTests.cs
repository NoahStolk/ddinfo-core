namespace DevilDaggersInfo.Core.Common.Test;

[TestClass]
public class TimeTests
{
	[TestMethod]
	public void TestTime()
	{
		Assert.AreEqual(1000.1998, GameTime.FromGameUnits(10001998).Seconds);
		Assert.AreEqual(1.0, GameTime.FromSeconds(1.0).Seconds);
		Assert.AreEqual(1.0, GameTime.FromSeconds(1).Seconds);
		Assert.AreEqual(1234567890.1234, GameTime.FromGameUnits(12345678901234UL).Seconds);
		Assert.AreEqual(1234567890.1234, GameTime.FromGameUnits(12345678901234L).Seconds);
	}
}
