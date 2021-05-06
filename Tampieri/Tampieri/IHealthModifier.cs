using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampieri
{
    interface IHealthModifier
    {
        int HealthDelta { get; }
        void UseOn(ILivingCharacter character)
        {
            int currentHealth = character.Health += this.HealthDelta;
        }
        string ToString();
    }
}
