namespace Tampieri.Model
{
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
