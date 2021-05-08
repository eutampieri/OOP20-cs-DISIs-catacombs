using System;
using System.Collections.Generic;
using System.Collections;
using Tile;
using TileMap;

public class MobFactoryImpl : MobFactory 
{
    public const int MAX_MOB_NUMBER = 420 / 2;
    public const int MIN_MOB_NUMBER = 69;
    private const int MOB_KIND_NUMBER = 2;

    private TileMap tileMap;
    private Random rand = new Random();

    public MobFactoryImpl(TileMap tileMap) 
    {
        this.tileMap = tileMap;
    }

    public void SetNewTileMap(TileMap tileMap) 
    {
        this.tileMap = tileMap;
    }

    public override List<Entity> SpawnAt(int x, int y, SingleObject<Entity> f) 
    {
        if (f == null || !tileMap.CanSpawnAt(x, y)) 
        {
            return new List<Entity>();
        }
        List<Entity> enemies = new ArrayList<>();
        enemies.Add(f.Create(x * AssetManagerProxy.GetMapTileSize(), y * AssetManagerProxy.GetMapTileSize(), this.tileMap));
        return enemies;
    }

    public override List<Entity> SpawnSome(int n, SingleObject<Entity> f) 
    {
        if (f == null) 
        {
            return new List<Entity>();
        }
        int randX, randY;
        List<Entity> enemies = new ArrayList<>();
        for (int i = 0; i < n; i++) {
            do {
                randX = rand.Next(tileMap.width());
                randY = rand.Next(tileMap.height());
            } while (!tileMap.CanSpawnAt(randX, randY));
            enemies.AddRange(SpawnAt(randX, randY, f));
        }
        return enemies;
    }

    public override List<Entity> SpawnRandom()
    {
        int randX, randY, randKind;
        int mobNum = rand.Next(MAX_MOB_NUMBER - MIN_MOB_NUMBER) + MIN_MOB_NUMBER;

        List<Entity> enemies = new ArrayList<>();
        for (int i = 0; i < mobNum; i++) {
            do 
            {
                randX = rand.Next(tileMap.width());
                randY = rand.Next(tileMap.height());
            } while (!tileMap.CanSpawnAt(randX, randY));
            randKind = rand.Next(MOB_KIND_NUMBER);
            if (randKind == 0) 
            {
                enemies.AddRange(SpawnAt(randX, randY, Bat));
            }
            if (randKind == 1) 
            {
                enemies.AddRange(SpawnAt(randX, randY, Slime));
            }
        }
        return enemies;
    }
}
