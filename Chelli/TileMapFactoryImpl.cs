using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>A TileMapFactory.</summary>
public sealed class TileMapFactoryImpl : TileMapFactory
{
	private const int NORMAL_N_ROOMS = 16;
	private const int NORMAL_MIN_ROOM_SIDE = 8;
	private const int NORMAL_MAX_ROOM_SIDE = 16;
	private const int NORMAL_MIN_ROOM_DIST = 32;
	private const int NORMAL_MAX_ROOM_DIST = 42;

	/// <summary>prng used to generate maps.</summary>
	private Random rand = new Random();

	/// <summary>A useful helper class for map generation.</summary>
	private class Point
	{
		public readonly int x, y;

		public Point() : this(0, 0)
		{ }

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public override bool Equals(Object obj)
		{
			//Check for null and compare run-time types.
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				Point p = (Point)obj;
				return (x == p.x) && (y == p.y);
			}
		}

		public override int GetHashCode()
		{
			return (x << 2) ^ y;
		}
		public int dist(Point o)
		{
			return Math.Abs(x - o.x) + Math.Abs(y - o.y);
		}
	}


	/// <param name="a">a point to connect</param>
	/// <param name="b">the other point to connect</param>
	/// <param name="res">the map in which to connect them</param>
	private void MakeCorridor(Point a, Point b, Tile[,] res)
	{
		int y = a.y;
		int x = a.x;
		while (y != b.y && x != b.x)
		{
			if (rand.Next(2) == 1)
			{
				if (y < b.y)
				{
					y++;
				}
				else
				{
					y--;
				}
			}
			else
			{
				if (x < b.x)
				{
					x++;
				}
				else
				{
					x--;
				}
			}
			res[y, x] = Tile.FLOOR;
		}
		while (y != b.y)
		{
			if (y < b.y)
			{
				y++;
			}
			else
			{
				y--;
			}
			res[y, x] = Tile.FLOOR;
		}
		while (x != b.x)
		{
			if (x < b.x)
			{
				x++;
			}
			else
			{
				x--;
			}
			res[y, x] = Tile.FLOOR;
		}
	}

	/// <param name="nRooms">      number of rooms to generate</param>
	/// <param name="maxRoomSide"> maximum side length of a room</param>
	/// <param name="minRoomDist"> minimum distance between two rooms' centers</param>
	/// <param name="maxRoomDist"> maximum distance with the closest room's center for each room center</param>
	/// <returns> A List with nRooms points for room centers according to the parameters</returns>
	private List<Point> DecideRoomCenters(int nRooms, int maxRoomSide, int minRoomDist,
			int maxRoomDist)
	{
		List<Point> pool = new List<Point>(); // pool of all points at acceptable distances from the already
											  // selected
											  // rooms
		var centers = new List<Point>(); // selected rooms' centers
		var dist = new Dictionary<Point, int>(); // distance to closest selected center for all points
		pool.Add(new Point(0, 0)); // starting point does not matter to the structure
		dist[pool[0]] = 0;
		for (int room = 0; room < nRooms; room++)
		{
			Point p = pool[rand.Next(pool.Count)]; // get a random point from the pool
			centers.Add(p);
			for (int dy = -maxRoomDist; dy <= maxRoomDist; dy++)
			{ // recalculate distances of all points in range
				for (int dx = Math.Abs(dy) - maxRoomDist; dx <= maxRoomDist - Math.Abs(dy); dx++)
				{
					Point cp = new Point(p.x + dx, p.y + dy);
					int formerDist = dist.GetValueOrDefault(cp, maxRoomDist + 1);
					int currentDist = cp.dist(p);
					if (currentDist < formerDist)
					{
						dist[cp] = currentDist;
						if (formerDist > maxRoomDist)
						{
							pool.Add(cp); // add points that were too far before
						}
					}
				}
			}
			for (int i = 0; i < pool.Count; i++)
			{
				if (dist[pool[i]] < minRoomDist)
				{ // remove points that are now too close
					pool[i] = pool[pool.Count - 1];
					pool.RemoveAt(pool.Count - 1);
					i--;
				}
			}
		}
		// get map boundaries to shift coordinates and calculate map size
		int minY = centers.Select(p => p.y).Min();
		int minX = centers.Select(p => p.x).Min();
		int dxa = 2 + (maxRoomSide + 1) / 2 - minX;
		int dya = 2 + (maxRoomSide + 1) / 2 - minY;
		for (int i = 0; i < centers.Count; i++)
		{
			var p = centers[i];
			centers[i] = new Point(p.x + dxa, p.y + dya);
		}
		return centers;
	}

	/// <param name="nRooms">      number of rooms to generate</param>
	/// <param name="minRoomSide"> minimum side length of a room</param>
	/// <param name="maxRoomSide"> maximum side length of a room</param>
	/// <param name="minRoomDist"> minimum distance between two rooms' centers</param>
	/// <param name="maxRoomDist"> maximum distance with the closest room's center for each room center</param>
	/// <returns> A Tilemap with nRooms square rooms connected by corridors in a tree, plus some random corridors minRoomDist &gt; maxRoomSide is recommended</returns>
	private TileMap Normal(int nRooms, int minRoomSide, int maxRoomSide, int minRoomDist,
			int maxRoomDist)
	{
		if (nRooms <= 0 || minRoomSide <= 0 || maxRoomSide < minRoomSide || minRoomDist < 0
				|| maxRoomDist < minRoomDist)
		{
			throw new ArgumentException();
		}
		List<Point> centers = DecideRoomCenters(nRooms, maxRoomSide, minRoomDist, maxRoomDist);
		int minY = centers.Select(p => p.y).Min();
		int maxY = centers.Select(p => p.y).Max();
		int minX = centers.Select(p => p.x).Min();
		int maxX = centers.Select(p => p.x).Max();

		int w = maxX - minX + maxRoomSide + 4; // this way a room should not touch the edges
		int h = maxY - minY + maxRoomSide + 4;
		var res = new Tile[h, w]; // new tile map initially filled with wall
		for (int y = 0; y < h; y++)
		{
			for (int x = 0; x < w; x++)
			{
				res[y, x] = Tile.WALL;
			}
		}
		foreach (Point p in centers)
		{ // add the rooms with random sizes
			int roomH = rand.Next(maxRoomSide - minRoomSide + 1) + minRoomSide;
			int roomW = rand.Next(maxRoomSide - minRoomSide + 1) + minRoomSide;
			for (int y = p.y - roomH / 2; y <= p.y + (roomH + 1) / 2; y++)
			{
				for (int x = p.x - roomW / 2; x <= p.x + (roomW + 1) / 2; x++)
				{
					res[y, x] = Tile.FLOOR;
				}
			}
		}
		for (int i = 1; i < centers.Count; i++)
		{
			var p = centers[i];
			var p0 = centers[0];
			for (int j = 1; j < i; j++)
			{ // get closest room already connected to the tree
				if (p.dist(centers[j]) < p.dist(p0))
				{
					p0 = centers[j];
				}
			}
			MakeCorridor(p0, p, res); // add the corridor to the tree
			if (rand.Next(4) == 0)
			{ // choose if to add a random corridor (dead end or cycle) to this room too
				var randomPoint = new Point(rand.Next(w - 2) + 1, rand.Next(h - 2) + 1);
				if (p.dist(randomPoint) <= 2 * maxRoomDist)
				{ // if the corridor would be too long, don't add it
					MakeCorridor(p, randomPoint, res);
				}
			}
		}
		return new TileMapImpl(res);
	}

	/// <returns>a TileMap with the default settings using a given seed</returns>
	public TileMap SeededDef(long seed)
	{
		rand = new Random((int)seed);
		return Normal(NORMAL_N_ROOMS, NORMAL_MIN_ROOM_SIDE, NORMAL_MAX_ROOM_SIDE, NORMAL_MIN_ROOM_DIST,
				NORMAL_MAX_ROOM_DIST); // and call the normal builder with default parameters
	}

	/// <returns>a TileMap with the default settings using a seed based on time</returns>
	public TileMap Def()
	{
		return SeededDef(DateTime.Now.Ticks);
	}

	/// <returns>an hxw TileMap with walls on the borders and floor inside</returns>
	public TileMap Empty(int h, int w)
	{
		if (h < 1 || w < 1)
		{
			throw new ArgumentException();
		}
		var res = new Tile[h, w];
		for (int y = 0; y < h; y++)
		{
			res[y, 0] = Tile.WALL;
			res[y, w - 1] = Tile.WALL;
		}
		for (int x = 0; x < w; x++)
		{
			res[0, x] = Tile.WALL;
			res[h - 1, x] = Tile.WALL;
		}
		for (int y = 1; y < h - 1; y++)
		{
			for (int x = 1; x < w - 1; x++)
			{
				res[y, x] = Tile.FLOOR;
			}
		}
		return new TileMapImpl(res);
	}

}
