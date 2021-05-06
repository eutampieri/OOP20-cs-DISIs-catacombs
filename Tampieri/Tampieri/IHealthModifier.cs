namespace Tampieri.Model
{
    public interface IHealthModifier
    {
        int HealthDelta { get; }
        void UseOn(ILivingCharacter character)
        {
            int currentHealth = character.Health += this.HealthDelta;
        }
        string ToString();
    }
}
