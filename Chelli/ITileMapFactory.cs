/// <summary>A factory for ITileMap.</summary>
public interface ITileMapFactory
{
	///<returns>a ITileMap created with the default settings and a non deterministic seed.</returns>
	ITileMap Def();

	/// <param name="seed">seed for the rng.</param>
	/// <returns> a ITileMap created with the default settings and a given seed.</returns>
	ITileMap SeededDef(long seed);

	/// <param name="h">height of the ITileMap in tiles.</param>
	/// <param name="w">Width of the ITileMap in tiles.</param>
	/// <returns> a ITileMap full of Floor and bordered by Wall.</returns>
	ITileMap Empty(int h, int w);
}
