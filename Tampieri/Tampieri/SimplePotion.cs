namespace Tampieri.Model
{
    /// <summary>A simple potion, which increases a <c>LivingCharacter</c>'s health by a fixed amount</summary>
    public sealed class SimplePotion : IHealthModifier
    {
        private string name;
        private int healingPower;

        public int HealthDelta => this.healingPower;

        public override string ToString()
        {
            return this.name;
        }

        public SimplePotion(string name, int healing)
        {
            this.name = name;
            this.healingPower = healing;
        }
    }
}
