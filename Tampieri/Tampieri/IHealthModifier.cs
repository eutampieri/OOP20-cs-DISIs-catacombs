namespace Tampieri.Model
{
    /// <summary>This interface describes an item that, if colliding with an entity, modifies its health</summary>
    public interface IHealthModifier
    {
        /// <summary>The value to add to the entity's health</summary>
        int HealthDelta { get; }
        /// <summary>Apply <c>this.HealthDelta</c> to the entity</summary>
        void UseOn(ILivingCharacter character)
        {
            int currentHealth = character.Health += this.HealthDelta;
        }
        string ToString();
    }
}
