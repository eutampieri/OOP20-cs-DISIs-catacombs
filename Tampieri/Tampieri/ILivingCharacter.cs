namespace Tampieri.Model
{
    public interface ILivingCharacter
    {
        int Health { get; set; }
        bool IsAlive()
        {
            return this.Health > 0;
        }
    }
}
