using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampieri
{
    interface ILivingCharacter
    {
        int Health { get; set; }
        bool IsAlive()
        {
            return this.Health > 0;
        }
    }
}
