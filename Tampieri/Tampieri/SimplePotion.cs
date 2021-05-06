using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampieri
{
    class SimplePotion : IHealthModifier
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
