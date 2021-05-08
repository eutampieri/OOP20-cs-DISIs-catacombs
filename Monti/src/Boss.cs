using TileMap;
using AssetManagerProxy;
using System;
using System.Collections.Generic;

/// <summary>
/// The game boss.
/// </summary>
public class Boss : Entity 
{

    private static int HEIGHT = 48;
    private static int WIDTH = 48;
    private static int MOVEMENT_SPEED = 4;
    private static int HEALTH = 100;
    private static int RADAR_BOX_POSITION_MODIFIER = 30 * AssetManagerProxy.GetMapTileSize();
    private static int RADAR_BOX_SIZE = 30 * 2 * AssetManagerProxy.GetMapTileSize() + Math.Max(WIDTH, HEIGHT);
    private static String NAME = "Boss";
    private static long MOVE_DELAY = 15L * 100;
    private static long PAUSE_DELAY = 10L * 100;
    private static long SPAWN_MOB_DELAY = 80L * 100;
    private static int BASE_DAMAGE = 15;
    private static int BASE_PROJECTILE_SPEED = 4;
    private static int BASE_Fire_RATE = 15;
    private static int BULLET_SIZE = 28;
    private static int MAX_CHANCE = 100;
    private static int MOB_SPAWN_CHANCE = 60;
    private static int SPAWN_RADIUS = 20;
    private Weapon weapon;
    private boolean isMoving;
    private int delayCounter;
    private int pauseCounter;
    private CollisionBox radarBox;
    private Tuple<int, int> shootingDirection;

    public Boss(int x, int y, TileMap tileMap) 
    {
        base(x, y, WIDTH, HEIGHT, tileMap, GameObjectType.BOSS, GameObject.Team.ENEMY);
        SetSpeed(MOVEMENT_SPEED);
        SetHealth(HEALTH);
        face = Direction.RIGHT;
        radarBox = new CollisionBox(posX - RADAR_BOX_POSITION_MODIFIER, posY - RADAR_BOX_POSITION_MODIFIER, RADAR_BOX_SIZE,
                RADAR_BOX_SIZE);
        weapon = new Weapon(this, tileMap, this.GetHitBox().GetPosX(), this.GetHitBox().GetPosY(),
                BASE_DAMAGE, BASE_PROJECTILE_SPEED, BASE_Fire_RATE, this.GetTeam(), GameObjectType.BOSS_BULLET, BULLET_SIZE) { };
        shootingDirection = new Tuple<int, int>(0, 0);
        this.delayCounter = 0;
        this.pauseCounter = 0;
        this.isMoving = true;
        
    }

    public override List<GameObject> Update(long delta, List<GameObject> others) 
    {

        var player, oth = others;

        List<GameObject> objs = new ArrayList<>();
        Random rand = new Random();
        ResetShootingDirection();
        if (isMoving) 
        {
            delayCounter += delta;
            if (delayCounter >= MOVE_DELAY) 
            {
                delayCounter = 0;
                isMoving = false;
                EesetMovement();
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
            objs.AddRange(weapon.Fire((int) GetShootingDirection().GetX() * weapon.ps, (int) GetShootingDirection().GetY() * weapon.ps));
            this.weapon.SetCanFire(true);
            objs.AddRange(weapon.Fire((int) GetShootingDirection().GetX() * -weapon.ps, (int) GetShootingDirection().GetY() * weapon.ps));
            this.weapon.setCanFire(true);
            objs.AddRange(weapon.Fire((int) GetShootingDirection().GetX() * weapon.ps, (int) GetShootingDirection().GetY() * -weapon.ps));
            this.weapon.setCanFire(true);
            objs.AddRange(weapon.Fire((int) GetShootingDirection().GetX() * -weapon.ps, (int) GetShootingDirection().GetY() * -weapon.ps));
            this.weapon.setCanFire(true);
            objs.AddRange(weapon.Fire(0, (int) GetShootingDirection().GetY() * weapon.ps));
            this.weapon.setCanFire(true);
            objs.AddRange(weapon.Fire(0, (int) GetShootingDirection().GetY() * -weapon.ps));
            this.weapon.setCanFire(true);
            objs.AddRange(weapon.Fire((int) GetShootingDirection().GetX() * weapon.ps, 0));
            this.weapon.setCanFire(true);
            objs.AddRange(weapon.Fire((int) GetShootingDirection().GetX() * -weapon.ps, 0));
            return objs;
        }
        return new List<GameObject>();
    }

    public override  Tuple<Action, Direction> GetActionWithDirection() 
    {
        if (this.face == Direction.UP || this.face == Direction.DOWN) 
        {
            return new Tuple<Action, Direction>(Action.IDLE, Direction.RIGHT);
        }
        return new Tuple<Action, Direction>(this.IsMoving() ? Action.MOVE : Action.IDLE, this.face);
    }

    public override boolean CanPerform(Action action) 
    {
        switch (action) 
        {
        case IDLE:
        case MOVE:
            return true;
        default:
            return false;
        }
    }

    private void ChangeDirection() 
    {
        Random rand = new Random();
        int c = rand.Next(8);
        switch (Math.Floor(c / 2)) 
        {
            case 0:
                face = Direction.UP;
                up = true;
            break;
            case 1:
                face = Direction.DOWN;
                down = true;
            break;
            case 2:
                face = Direction.LEFT;
                left = true;
            break;
            case 3:
                face = Direction.RIGHT;
                right = true;
            break;
            default:
                face = Direction.LEFT;
                ResetMovement();
            break;
        }

    }

    private void UpdateRadarBoxLocation() 
    {
        radarBox.SetLocation(posX - RADAR_BOX_POSITION_MODIFIER, posY - RADAR_BOX_POSITION_MODIFIER);
    }

    public override int GetHealth() 
    {
        return this.hp;
    }

    public override void SetHealth(int health) 
    {
        this.hp = health;
    }

    public String GetName() 
    {
        return Boss.NAME;
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
