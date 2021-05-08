using Chelli.Tile;
using Chelli.TileMap;
using AssetManagerProxy;
using System;
using GameObject;

/// <summary>
/// Rapresents all living entities of GameObject.
/// </summary>
public abstract class Entity : GameObject, LivingCharacter, Animatable 
{

    protected boolean up, down, right, left;
    
    protected Direction face;
    
    protected int hp;
    
    protected int width, height, size; 
    
    protected TileMap tileMap;

    
    public Entity(int x, int y, int width, int height, TileMap tileMap,
            GameObjectType kind, Team team) 
    {
        base(x, y, kind, new CollisionBox(x, y, width, height), team);
        this.tileMap = tileMap;
        this.width = width;
        this.height = height;
        this.size = width >= height ? width : height;
    }

    public  int GetWidth() 
    {
        return width;
    }
    
    public  void SetWidth(int width) 
    {
        this.width = width;
    }
    
    public  int GetHeight() 
    {
        return height;
    }

    public  void SetHeight(int height) 
    {
        this.height = height;
    }

    public  int GetSize() 
    {
        return this.size;
    }

    public override List<GameObject> Update(long delta, List<GameObject> others) 
    {
        Move();
        UpdateSpriteLocation();
        return new List<GameObject>();
    }

    protected void Move() 
    {

        int dx = 0, dy = 0;
        if (up) 
        {
            dy = -speedY;
            face = Direction.UP;
            while (IsUpCollision(Math.abs(dy))) 
            {
                dy++;
            }
        }
        if (down) 
        {
            dy = speedY;
            face = Direction.DOWN;
            while (IsDownCollision(dy)) 
            {
                dy--;
            }
        }
        if (left)
        {
            dx = -speedX;
            face = Direction.LEFT;
            while (IsLeftCollision(Math.abs(dx))) 
            {
                dx++;
            }
        }
        if (right) 
        {
            dx = speedX;
            face = Direction.RIGHT;
            while (IsRightCollision(dx)) 
            {
                dx--;
            }
        }
        hitBox.Move(dx, dy);
    }

    protected boolean IsUpCollision(int dy) 
    {
        return tileMap.at(hitBox.GetPosX() / AssetManagerProxy.GetMapTileSize(),
                (hitBox.GetPosY() - dy) / AssetManagerProxy.GetMapTileSize()) == Tile.WALL
                || tileMap.at((hitBox.GetPosX() + hitBox.GetWidth()) / AssetManagerProxy.GetMapTileSize(),
                        (hitBox.GetPosY() - dy) / AssetManagerProxy.GetMapTileSize()) == Tile.WALL;
    }

    protected boolean IsRightCollision(int dx) 
    {
        return tileMap.at((hitBox.GetPosX() + hitBox.GetWidth() + dx) / AssetManagerProxy.GetMapTileSize(),
                hitBox.GetPosY() / AssetManagerProxy.GetMapTileSize()) == Tile.WALL
                || tileMap.at((hitBox.GetPosX() + hitBox.GetWidth() + dx) / AssetManagerProxy.GetMapTileSize(),
                        (hitBox.GetPosY() + hitBox.GetHeight()) / AssetManagerProxy.GetMapTileSize()) == Tile.WALL;
    }

    protected boolean IsDownCollision(int dy) 
    {
        return tileMap.at(hitBox.GetPosX() / AssetManagerProxy.GetMapTileSize(),
                (hitBox.GetPosY() + hitBox.GetHeight() + dy) / AssetManagerProxy.GetMapTileSize()) == Tile.WALL
                || tileMap.at((hitBox.GetPosX() + hitBox.GetWidth()) / AssetManagerProxy.GetMapTileSize(),
                        (hitBox.GetPosY() + hitBox.GetHeight() + dy) / AssetManagerProxy.GetMapTileSize()) == Tile.WALL;
    }

    protected boolean IsLeftCollision(int dx) 
    {
        return tileMap.at((hitBox.GetPosX() - dx) / AssetManagerProxy.GetMapTileSize(),
                hitBox.GetPosY() / AssetManagerProxy.GetMapTileSize()) == Tile.WALL
                || tileMap.at((hitBox.GetPosX() - dx) / AssetManagerProxy.GetMapTileSize(),
                        (hitBox.GetPosY() + hitBox.GetHeight()) / AssetManagerProxy.GetMapTileSize()) == Tile.WALL;
    }

    protected void UpdateSpriteLocation() 
    {
        posX = hitBox.GetPosX();
        posY = hitBox.GetPosY();
    }

    protected void ResetMovement() 
    {
        up = false;
        down = false;
        right = false;
        left = false;
    }

    public abstract Tuple<Action, Direction> GetActionWithDirection();

    public override bool IsMarkedForDeletion() 
    {
        return !this.IsAlive();
    }

    public boolean IsMoving()
    {
        return this.right || this.left || this.up || this.down;
    }
}
