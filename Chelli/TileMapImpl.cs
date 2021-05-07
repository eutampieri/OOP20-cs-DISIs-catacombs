/// <summary>A ITileMap.</summary>
public sealed class TileMapImpl : ITileMap
{

	private readonly Tile[,] map;

	/// <summary>constructs a tilemaps from a Tile[][].</summary>
	/// <param name="m"> a matrix of Tiles.</param>
	public TileMapImpl(Tile[,] m)
	{
		map = (Tile[,])m.Clone();
	}

	public int Height
	{
		get => map.GetLength(0);
	}

	public int Width
	{
		get => map.GetLength(1);
	}

	public Tile this[int x, int y]
	{
		get {
			if (y < 0 || x < 0 || y >= this.Height || x >= this.Width)
			{
				return Tile.Void;
			}
			return map[y, x];
		}
	}

	/// <returns>Internal representation of the tilemap. used for testing.</returns>
	public Tile[,] GetMap()
	{
		return (Tile[,])map.Clone();
	}

	public bool CanSpawnAt(int x, int y)
	{
		return this[x, y].IsWalkable() && this[x - 1, y].IsWalkable() && this[x + 1, y].IsWalkable()
			&& this[x, y - 1].IsWalkable() && this[x, y + 1].IsWalkable();
	}
}
