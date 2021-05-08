using TileMap;
using AssetManagerProxy;
using System;
using System.Collections.Generic;


/// <summary>
/// An enemy that follows a specified entity. Deals damage on contact.
/// </summary>
public class Slime : Entity, HealthModifier 
{

    private static const int HEIGHT = 16;
    private static const int WIDTH = 16;
    private static const int MOVEMENT_SPEED = 1;
    private static const int HEALTH = 10;
    private static const String NAME = "Slime";
    private static const int RADAR_BOX_POSITION_MODIFIER = 20 * AssetManagerProxy.GetMapTileSize();
    private static const int RADAR_BOX_SIZE = 20 * 2 * AssetManagerProxy.GetMapTileSize() + Math.Max(WIDTH, HEIGHT);
    private static const int DAMAGE_ON_HIT = 5;
    private static const long HIT_DELAY = 10L * 1_000_000_000;

    private GameObject characterToFollow;
    
    private CollisionBox radarBox;

    private bool canDmg;
    private long dmgDelayCount;

    public Slime(int x, int y, TileMap tileMap) 
    {
        base(x, y, WIDTH, HEIGHT, tileMap, GameObjectType.ENEMY, GameObject.Team.ENEMY);
        SetSpeed(MOVEMENT_SPEED);
        SetHealth(HEALTH);
        face = Direction.RIGHT;
        radarBox = new CollisionBox(posX - RADAR_BOX_POSITION_MODIFIER, posY - RADAR_BOX_POSITION_MODIFIER, RADAR_BOX_SIZE,
                RADAR_BOX_SIZE);
        this.canDmg = true;
        this.dmgDelayCount = 0;
    }

    public override List<GameObject> Update(long delta, List<GameObject> others) 
    {
        var player, oth = others;

        if (!canDmg) 
        {
            dmgDelayCount += delta;
            if (dmgDelayCount >= HIT_DELAY) 
            {
                dmgDelayCount = 0;
                canDmg = true;
            }
        }
        
        // Java streams into LINQ query
        // I tried to use .NET IEnumerable<T> Interface
        player = 
            from obj in oth
            where obj is Player
            select obj;

        if (player == null || !this.radarBox.Overlaps(player.GetHitBox())) 
        {
            SetCharacterToFollow(null);
        } 
        else
        {
            SetCharacterToFollow(player);
        }

        Follow();
        base.Update(delta, others);
        UpdateRadarBoxLocation();
        if (player != null && this.GetHitBox().Overlaps(player.GetHitBox() && canDmg))
        {
            this.UseOn((LivingCharacter) (others.stream().filter((x) => x is Player).FindFirst().Get()));
            canDmg = false;
        }
        this.ResetMovement();
        return new List<GameObject>();
    }
    public override void UseOn(LivingCharacter character) 
    {
        if (this.canDmg) 
        {
            int currentHealth = character.GetHealth();
            currentHealth += this.GetHealthDelta();
            character.SetHealth(currentHealth);
            this.canDmg = false;
        }
    }

    public override Tuple<Action, Direction> GetActionWithDirection() 
    {
        return new Tuple<Action, Direction> (Action.MOVE, this.face);
    }

    public bool CanPerform(Action action) 
    {
        return action == Action.MOVE;
    }

    public override int GetHealth() 
    {
        return this.hp;
    }

    public override void SetHealth(int health) 
    {
        this.hp = health;
    }

    public void SetCharacterToFollow(GameObject obj) 
    {
        characterToFollow = obj;
    }

    public GameObject GetCharacterToFollow() 
    {
        return characterToFollow;
    }

    private void Follow() {
        if (characterToFollow == null) 
        {
            return;
        }
        if (characterToFollow.GetPosX() < posX) 
        {
            left = true;
        } else if (characterToFollow.GetPosX() > posX) 
        {
            right = true;
        } else 
        {
            right = false;
            left = false;
        }
        if (characterToFollow.GetPosY() < posY) 
        {
            up = true;
        } else if (characterToFollow.GetPosY() > posY) 
        {
            down = true;
        } else 
        {
            up = false;
            down = false;
        }
    }

    public void UpdateRadarBoxLocation() 
    {
        radarBox.SetLocation(posX - RADAR_BOX_POSITION_MODIFIER, posY - RADAR_BOX_POSITION_MODIFIER);
    }

    public override String GetName() 
    {
        return Slime.NAME;
    }

    public override int GetHealthDelta() 
    {
        return -this.DAMAGE_ON_HIT;
    }
}
