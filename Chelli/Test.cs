using System;
using System.Diagnostics;

class Test
{
	private static void CheckMap(ITileMap m)
	{
		for (int y = 0; y < m.Height(); y++)
		{
			for (int x = 0; x < m.Width(); x++)
			{
				Debug.Assert(m.At(x, y) == Tile.FLOOR || m.At(x, y) == Tile.WALL);
				if (y == 0 || x == 0 || y == m.Height() - 1 || x == m.Width() - 1)
				{
					Debug.Assert(m.At(x, y) != Tile.FLOOR);
				}
			}
		}
	}

	private static void TestMultipleMapWontFail()
	{
		TileMapFactoryImpl mf = new TileMapFactoryImpl();
		for (int i = 0; i < 32; i++)
		{
			CheckMap(mf.Def());
		}
	}

	private static void TestMapClone()
	{
		TileMapImpl tm = (TileMapImpl)new TileMapFactoryImpl().Def();
		Tile[,] tiles = tm.GetMap();
		Debug.Assert(tiles.GetLength(1) == tm.Width());
		Debug.Assert(tiles.GetLength(0) == tm.Height());
	}

	private static void TestEmptyMap()
	{
		ITileMap tm = new TileMapFactoryImpl().Empty(3, 3);
		Debug.Assert(Tile.FLOOR == tm.At(1, 1));
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				if (x == y && x == 1)
				{
					continue;
				}
				else
				{
					Debug.Assert(Tile.WALL == tm.At(x, y));
				}
			}
		}
	}

	private static void TestSpawnPoints()
	{
		int mapSize = 8;
		ITileMap tm = new TileMapFactoryImpl().Empty(mapSize, mapSize);
		Debug.Assert(Tile.FLOOR == tm.At(1, 1));
		for (int y = 0; y < mapSize; y++)
		{
			for (int x = 0; x < 8; x++)
			{
				Debug.Assert(tm.CanSpawnAt(x, y) == y > 1 && x > 1 && y < mapSize - 2 && x < mapSize - 2);
			}
		}
	}

	private static void TestOutOfBounds()
	{
		int mapSize = 5;
		ITileMap tm = new TileMapFactoryImpl().Empty(mapSize, mapSize);
		Debug.Assert(Tile.VOID == tm.At(-1, -1));
		Debug.Assert(Tile.VOID == tm.At(mapSize + 1, mapSize + 1));
		Debug.Assert(Tile.VOID == tm.At(-1, 0));
	}

	private static void PrintMapExample()
	{
		ITileMapFactory tmf = new TileMapFactoryImpl();
		ITileMap tm = tmf.Def();
		for (int y = 0; y < tm.Height(); ++y)
		{
			for (int x = 0; x < tm.Width(); ++x)
			{
				if (tm.At(x, y) == Tile.WALL)
				{
					Console.Write("#");
				}
				else
				{
					Console.Write(".");
				}
			}
			Console.WriteLine();
		}
	}

	static void Main(string[] args)
	{
		TestMultipleMapWontFail();
		TestMapClone();
		TestEmptyMap();
		TestSpawnPoints();
		TestOutOfBounds();
		PrintMapExample();
	}
}
