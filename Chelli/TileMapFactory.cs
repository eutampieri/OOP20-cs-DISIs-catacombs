/// <summary>A factory for TileMap.</summary>
public interface TileMapFactory
{
	///<returns>a TileMap created with the default settings and a non deterministic seed.</returns>
	TileMap Def();

	/// <param name="seed">seed for the rng.</param>
	/// <returns> a TileMap created with the default settings and a given seed.</returns>
	TileMap SeededDef(long seed);

	/// <param name="h">height of the TileMap in tiles.</param>
	/// <param name="w">Width of the TileMap in tiles.</param>
	/// <returns> a TileMap full of Floor and bordered by Wall.</returns>
	TileMap Empty(int h, int w);
}
