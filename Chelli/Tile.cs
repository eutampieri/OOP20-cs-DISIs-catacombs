public static class TileExtensions
{
	/// <returns>if the Tile can be walked on.
	public static bool IsWalkable(this Tile t)
	{
		return t == Tile.Floor || t == Tile.Stairs;
	}
}

/// <summary>Tile represents a cell type for the map.</summary>
public enum Tile
{
	/// <summary>A void tile, which contains nothing.</summary>
	Void,
	/// <summary>A wall tile, which forms a wall.</summary>
	Wall,
	/// <summary>A floor tile, which makes floors.</summary>
	Floor,
	/// <summary>A stair tile, which will form flight of stairs.</summary>
	Stairs,
}
