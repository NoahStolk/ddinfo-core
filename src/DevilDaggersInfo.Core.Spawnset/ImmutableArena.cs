using System.Collections;

namespace DevilDaggersInfo.Core.Spawnset;

public sealed class ImmutableArena
{
	private readonly int _dimension;
	private readonly float[,] _heights;

	// TODO: Make sure it's not possible to modify the contents of the reference passed to the ctor.
	public ImmutableArena(int dimension, float[,] heights)
	{
		if (dimension is < 0 or > SpawnsetBinary.ArenaDimensionMax)
			throw new ArgumentOutOfRangeException(nameof(dimension), $"Dimension cannot be negative or greater than {SpawnsetBinary.ArenaDimensionMax}.");

		if (heights.GetLength(0) != dimension || heights.GetLength(1) != dimension)
			throw new ArgumentOutOfRangeException(nameof(heights), $"Arena array must be {dimension} by {dimension}.");

		_dimension = dimension;
		_heights = heights;
	}

	public float this[int x, int y]
	{
		get
		{
			if (x < 0 || x >= _dimension)
				throw new ArgumentOutOfRangeException(nameof(x), $"Parameter {x} is out of range; must not be negative, and must not be equal to or greater than {_dimension}.");
			if (y < 0 || y >= _dimension)
				throw new ArgumentOutOfRangeException(nameof(y), $"Parameter {y} is out of range; must not be negative, and must not be equal to or greater than {_dimension}.");

			return _heights[x, y];
		}
	}

	public float[,] GetMutableClone()
	{
		float[,] arenaTiles = new float[_dimension, _dimension];
		for (int i = 0; i < _dimension; i++)
		{
			for (int j = 0; j < _dimension; j++)
				arenaTiles[i, j] = this[i, j];
		}

		return arenaTiles;
	}

	public byte[] GetCompressedArenaBytes()
	{
		if (_dimension == 0)
			return [];

		byte[] packed = Pack();

		using MemoryStream stream = new();
		using BinaryWriter writer = new(stream);

		writer.Write(_dimension);

		int consecutiveEqualValues = 1;
		byte currentByte = packed[0];
		for (int i = 1; i < packed.Length; i++)
		{
			if (packed[i] == currentByte)
			{
				consecutiveEqualValues++;
			}
			else
			{
				// Write the current byte and the number of consecutive equal values.
				writer.Write7BitEncodedInt(consecutiveEqualValues);
				writer.Write(currentByte);

				currentByte = packed[i];
				consecutiveEqualValues = 1;
			}
		}

		// Write the last byte and the number of consecutive equal values.
		writer.Write7BitEncodedInt(consecutiveEqualValues);
		writer.Write(currentByte);

		return stream.ToArray();
	}

	private byte[] Pack()
	{
		const int maxHeight = 15;

		// Encode each tile as an 8-bit value. All values will be between 0 and 15, meaning the heights will be approximated.
		byte[] raw = new byte[_dimension * _dimension];
		for (int i = 0; i < _dimension; i++)
		{
			for (int j = 0; j < _dimension; j++)
			{
				float clampedHeight = Math.Clamp(_heights[i, j], 0, maxHeight);
				raw[i * _dimension + j] = (byte)clampedHeight;
			}
		}

		return raw;
	}
}
