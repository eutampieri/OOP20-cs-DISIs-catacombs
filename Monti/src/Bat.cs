using TileMap;
using AssetManagerProxy;
using System;
using System.Collections.Generic;

/// <summary>
/// An enemy entity that moves right to left and viceversa. Can shoot towards an entity with a basic direction.
/// </summary>
public class Bat : Entity 
{

    private static const int HEIGHT = 16;
    private static const int WIDTH = 16;
    private static const int MOVEMENT_SPEED = 3;
    private static const int HEALTH = 8;
    private static const int RADAR_BOX_POSITION_MODIFIER = 20 * AssetManagerProxy.GetMapTileSize();
    private static const int RADAR_BOX_SIZE = 20 * 2 * AssetManagerProxy.GetMapTileSize() + Math.Max(WIDTH, HEIGHT);
    private static const int BASE_DAMAGE = 2;
    private static const int BASE_FIRE_RATE = 1;
    private static const int BASE_PROJECTILE_SPEED = 3;
    private static const String NAME = "Bat";
    private static const long MOVE_DELAY = 5L * 100;
    private static const long PAUSE_DELAY = 10L * 100;

    private Weapon weapon;
    private bool isMoving;
    private long delayCounter;
    private long pauseCounter;
    private CollisionBox radarBox;
    private Tuple<int, int> shootingDirection;

    public Bat(int x, int y, TileMap tileMap) 
    {
        base(x, y, WIDTH, HEIGHT, tileMap, GameObjectType.ENEMY, GameObject.Team.ENEMY);
        SetSpeed(MOVEMENT_SPEED);
        SetHealth(HEALTH);
        face = Direction.RIGHT;
        radarBox = new CollisionBox(posX - RADAR_BOX_POSITION_MODIFIER, posY - RADAR_BOX_POSITION_MODIFIER,
                RADAR_BOX_SIZE, RADAR_BOX_SIZE);
        weapon = new Weapon(this, tileMap, this.GetHitBox().GetPosX(), this.GetHitBox().GetPosY(),
                BASE_DAMAGE, BASE_PROJECTILE_SPEED, BASE_FIRE_RATE, this.GetTeam()) { };
        shootingDirection = new Tuple<int, int>(0, 0);
        this.delayCounter = 0;
        this.pauseCounter = 0;
        this.isMoving = true;

    }

    public override List<GameObject> Update(long delta, List<GameObject> others) 
    {

        var player, oth = others;

        ResetShootingDirection();
        if (isMoving) 
        {
            delayCounter += delta;
            if (delayCounter >= MOVE_DELAY) 
            {
                delayCounter = 0;
                isMoving = false;
                ResetMovement();
            }
        } 
        else 
        {
            pauseCounter += delta;
            if (pauseCounter >= PAUSE_DELAY) 
            {
                pauseCounter = 0;
                isMoving = true;
                ChangeDirection();
            }
        }

        /* 
         * Java streams into LINQ query
         * I tried to use .NET IEnumerable<T> Interface
         */
        player = 
            from obj in oth
            where obj is Player
            select obj;

        if (player == null || !this.radarBox.Overlaps(player.GetHitBox())) 
        {
            this.weapon.SetCanFire(false);
        } 
        else if (this.weapon.CanFire())
        {
            SetShootingDirection(player);
        }

        base.Update(delta, others);
        UpdateRadarBoxLocation();
        weapon.Update(delta, others);
        if (this.weapon.canFire && this.GetShootingDirection().GetX() != 0 && this.GetShootingDirection().GetY() != 0) 
        {
            return weapon.Fire((int)GetShootingDirection().GetX() * weapon.ps, (int)GetShootingDirection().GetY() * weapon.ps);
        }
        return new List<GameObject>();
    }

    public override Tuple<Action, Direction> GetActionWithDirection() 
    {
        // TODO Auto-generated method stub
        return new Tuple<Action, Direction>(Action.MOVE, this.face);
    }

    public override bool CanPerform(Action action) 
    {
        switch (action) 
        {
            case ATTACK:
            case MOVE:
                return true;
            default:
                return false;
        }
    }

    public override int GetHealth() 
    {
        return this.hp;
    }

    public override void setHealth(int health) 
    {
        this.hp = health;
    }

    private void ChangeDirection() 
    {
        if (face == Direction.RIGHT) 
        {
            left = true;
            right = false;
            face = Direction.LEFT;
        } 
        else 
        {
            right = true;
            left = false;
            face = Direction.RIGHT;
        }
    }

    private void UpdateRadarBoxLocation() 
    {
        radarBox.SetLocation(posX - RADAR_BOX_POSITION_MODIFIER, posY - RADAR_BOX_POSITION_MODIFIER);
    }

    public String GetName() 
    {
        return Bat.NAME;
    }

    public Tuple<int, int> GetShootingDirection() 
    {
        return this.shootingDirection;
    }

    public void ResetShootingDirection() 
    {
        this.shootingDirection = (0, 0);
    }

    public void SetShootingDirection(GameObject e) 
    {
        if (e == null) 
        {
            return;
        }
        /* Transposition of x = Integer.Compare(e.GetHitBox().GetPosX(), this.GetHitBox().GetPosX());
         *              and y = Integer.Compare(e.GetHitBox().GetPosY(), this.GetHitBox().GetPosY());
         * without using java libraries.
         */
        int x = 0, y = 0;
        if (e.GetHitBox().GetPosX() > this.GetHitBox().GetPosX())
        {
             x = 1;
        } else if (e.GetHitBox().GetPosX() < this.GetHitBox().GetPosX())
        {
            x = -1;
        }
        if (e.GetHitBox().GetPosY() < this.GetHitBox().GetPosY())
        {
             y = 1;
        } else if (e.GetHitBox().GetPosY() > this.GetHitBox().GetPosY())
        {
            y = -1;
        }
        this.shootingDirection = (x, y);
    }

}
