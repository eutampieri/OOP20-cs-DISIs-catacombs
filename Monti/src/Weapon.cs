using TileMap;
using System;

/// <summary>
/// A weapon that shoots at a specified fire rate and damage.
/// </summary>
public abstract class Weapon : GameObject 
{

    protected int strength;

    protected int ps;
    
    protected long fireRate;    
    protected bool canFire;

    protected long fireDelay, fireDelayCount;

    private TileMap tileMap;

    private Entity user;

    public Weapon(Entity e, TileMap tm, int x, int y, int strength, int ps,
            int fr, Team team) 
    {
        base(x, y, GameObjectType.WEAPON, new CollisionBox(x, y, 0, 0), team);
        this.user = e;
        SetTileMap(tm);
        SetStrength(strength);
        SetProjectileSpeed(ps);
        SetFireRate(fr);
        SetFireDelay(Math.Round(1000f / fireRate));
        SetCanFire(true);
        this.fireDelayCount = 0;
    }

    public override List<GameObject> Update(long delta, List<GameObject> others) {
        if (!canFire) 
        {
            fireDelayCount += delta;
            if (fireDelayCount >= fireDelay) 
            {
                fireDelayCount = 0;
                SetCanFire(true);
            }
        }
        if (this.user != null) 
        {
            this.hitBox.SetPosX(this.user.GetHitBox().GetPosX() + this.user.GetSize()/2 - 1);
            this.hitBox.SetPosY(this.user.GetHitBox().GetPosY() + this.user.GetSize()/2 - 1);
        }
        return new List<GameObject>();
    }

    public void SetTileMap(TileMap tm) 
    {
        this.tileMap = tm;
    }

    public void SetStrength(int str) 
    {
        this.strength = str;
    }

    public void SetProjectileSpeed(int ps) 
    {
        this.ps = ps;
    }

    public void SetFireRate(long fr) 
    {
        this.fireRate = fr;

    }

    public void SetFireDelay(long fd)
    {
        this.fireDelay = fd;
    }

    public void SetCanFire(bool cf) 
    {
        this.canFire = cf;
    }

    public bool CanFire() 
    {
        return this.canFire;
    }

    public List<GameObject> Fire(int psx, int psy) 
    {
       Projectile p = new Projectile(this.GetHitBox().GetPosX(), this.GetHitBox().GetPosY(),
                psx*ps, psy*ps, strength, tileMap, this.GetTeam());
       SetCanFire(false);
       return new List<GameObject>(p);
    }

    public void SetUser(Entity user) 
    {
        this.user = user;
    }
}
