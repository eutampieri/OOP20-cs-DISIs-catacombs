using System;


/// <summary>
/// GameObject base rapresenting every object inside the game as a set of coordinates and an hitbox.
/// </summary>
public abstract class GameObject 
{
    
    public enum GameObjectType 
    {
        ENEMY, PLAYER, WEAPON, BULLET, PICKUP, ITEM, BOSS_BULLET,
    };
    
    public enum Team 
    {
        ENEMY, FREIND,
    };

    protected Team team;

    protected int posX, posY;

    protected GameObjectType kind;

    protected int speedX, speedY;

    protected CollisionBox hitBox; 

    public GameObject(int x, int y, GameObjectType kind, CollisionBox hitBox, Team team) 
    {
        this.SetPosX(x);
        this.SetPosY(y);
        this.kind = kind;
        this.hitBox = hitBox;
        this.team = team;
    }

    public abstract List<GameObject> Update(long delta, List<GameObject> others);

    public int GetPosX() 
    {
        return posX;
    }

    public void SetPosX(int posX) 
    {
        this.posX = posX;
    }

    public int GetPosY() {
        return posY;
    }

    public void SetPosY(int posY) {
        this.posY = posY;
    }

    public void SetPos(int posX, int posY) 
    {
        this.posX = posX;
        this.posY = posY;
    }

    public int GetSpeedX() 
    {
        return speedX;
    }

    public void SetSpeedX(int speedX) 
    {
        this.speedX = speedX;
    }

    public int GetSpeedY() 
    {
        return speedY;
    }

    public void SetSpeedY(int speedY) 
    {
        this.speedY = speedY;
    }

    public void SetSpeed(int speed) 
    {
        this.SetSpeedX(speed);
        this.SetSpeedY(speed);
    }

    public GameObjectType GetKind() 
    {
        return kind;
    }

    public bool IsMarkedForDeletion() 
    {
        return false;
    }

    public CollisionBox GetHitBox() 
    {
        return hitBox;
    }

    public Team GetTeam() 
    {
        return team;
    }
}
