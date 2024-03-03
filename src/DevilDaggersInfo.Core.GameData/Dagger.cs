namespace DevilDaggersInfo.Core.GameData;

public class Dagger
{
	internal Dagger(string name, Rgb color)
	{
		Name = name;
		Color = color;
	}

	public string Name { get; }
	public Rgb Color { get; }
}
