/// <summary>A TileMap.</summary>
public sealed class TileMapImpl : TileMap
{

	private readonly Tile[,] map;

	/// <summary>constructs a tilemaps from a Tile[][].</summary>
	/// <param name="m"> a matrix of Tiles.</param>
	public TileMapImpl(Tile[,] m)
	{
		map = (Tile[,])m.Clone();
	}

	public int Height()
	{
		return map.GetLength(0);
	}

	public int Width()
	{
		return map.GetLength(1);
	}

	public Tile At(int x, int y)
	{
		if (y < 0 || x < 0 || y >= this.Height() || x >= this.Width())
		{
			return Tile.VOID;
		}
		return map[y, x];
	}


	/// <returns>Internal representation of the tilemap. used for testing.</returns>
	public Tile[,] GetMap()
	{
		return (Tile[,])map.Clone();
	}

	public bool CanSpawnAt(int x, int y)
	{
		return this.At(x, y).IsWalkable() && this.At(x - 1, y).IsWalkable() && this.At(x + 1, y).IsWalkable()
			&& this.At(x, y - 1).IsWalkable() && this.At(x, y + 1).IsWalkable();
	}
}
