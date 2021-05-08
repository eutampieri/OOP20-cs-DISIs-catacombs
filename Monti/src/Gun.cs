using System;
using TileMap;


/// <summary>
/// A weapon that shoots at a modarate fire rate and deals modarate damage.
/// </summary>
public class Gun : Weapon 
{
    private static int STRENGTH = 7;
    private static int FIRE_RATE = 90;
    private static int PROJECTILE_SPEED = 15;

    public Gun(Entity e, TileMap tm, int x, int y, Team team) 
    {
        base(e, tm, x, y, STRENGTH, PROJECTILE_SPEED, FIRE_RATE, team);
    }

    public int GetStrength() 
    {
        return STRENGTH;
    }

    public int GetFireRate() 
    {
        return FIRE_RATE;
    }

    public int GetProjectileSpeed() 
    {
        return PROJECTILE_SPEED;
    }
}
