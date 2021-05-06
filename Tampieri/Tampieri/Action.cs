using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampieri
{
    public enum Action
    {
        Move,
        Attack,
        Idle,
        Die,
        
    }
    public static class ActionExtension
    {
        public static string ToString(this Action a)
        {
            return a.ToString().ToLower();
        }

        public static Action ToAction(this String s)
        {
            return s switch
            {
                "move" => Action.Move,
                "attack" => Action.Attack,
                "idle" => Action.Idle,
                "die" => Action.Die,
                _ => throw new InvalidCastException(),
            };
        }
    }
}
