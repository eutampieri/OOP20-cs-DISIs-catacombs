
/// <summary>ITileMap represents a map for the game.</summary>
public interface ITileMap
{
	/// <returns>the height of the map in tiles.</returns>
	int Height
	{
		get;
	}

	/// <returns>the width of the map in tiles.</returns>
	int Width
	{
		get;
	}

	/// <param name="x">column.</param>
	/// <param name="y">row.</param>
	/// <returns>the Tile at column x and row y in the map.</returns>
	Tile this[int x, int y]
	{
		get;
	}

	/// <param name="x">column.</param>
	/// <param name="y">row.</param>
	/// <returns>if the Tile at column x and row y is a tile an entity can spawn on.</returns>
	bool CanSpawnAt(int x, int y);
}
