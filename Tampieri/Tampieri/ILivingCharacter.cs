using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampieri
{
    interface ILivingCharacter
    {
        int GetCurrentHealth();
        void SetHealth(int health);
        bool IsAlive()
        {
            return this.GetCurrentHealth() > 0;
        }
    }
}
