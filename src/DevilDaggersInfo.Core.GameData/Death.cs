namespace DevilDaggersInfo.Core.GameData;

public class Death
{
	internal Death(int value, string name, Rgb color)
	{
		Value = value;
		Name = name;
		Color = color;
	}

	public int Value { get; }
	public string Name { get; }
	public Rgb Color { get; }
}
