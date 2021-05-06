using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampieri
{
    interface IHealthModifier
    {
        int GetHealthDelta();
        void UseOn(ILivingCharacter character)
        {
            int currentHealth = character.GetCurrentHealth();
            currentHealth += this.GetHealthDelta();
            character.SetHealth(currentHealth);
        }
        string ToString();
    }
}
