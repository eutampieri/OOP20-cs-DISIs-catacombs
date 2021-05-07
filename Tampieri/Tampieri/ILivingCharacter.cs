namespace Tampieri.Model
{
    /// <summary>Implement this when you want an entity to possess the concept of health and life</summary>
    public interface ILivingCharacter
    {
        int Health { get; set; }
        bool IsAlive()
        {
            return this.Health > 0;
        }
    }
}
