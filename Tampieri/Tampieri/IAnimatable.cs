using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampieri
{
    interface IAnimatable
    {
        bool CanPerform(Action a);
    }
}
