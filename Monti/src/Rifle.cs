using System;
using TileMap;


/// <summary>
/// A weapon that shoots with a high fire rate but deals less than avarage damage.
/// </summary>
public class Rifle : Weapon 
{
    private static int STRENGTH = 3;
    private static int FIRE_RATE = 210;
    private static int PROJECTILE_SPEED = 14;
    private static int BOX_WIDTH = (int) (45 * AssetManagerProxy.getWeaponScalingFactor());
    private static int BOX_HEIGHT = 17;

    public Rifle(Entity e, TileMap tm, int x, int y, Team team) 
    {
        base(e, tm, x, y, STRENGTH, PROJECTILE_SPEED, FIRE_RATE, BOX_WIDTH, BOX_HEIGHT, team);
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
