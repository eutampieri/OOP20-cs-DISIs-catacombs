using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampieri
{
    class Player : IAnimatable, ILivingCharacter
    {
        private int _health;
        private string name;
        public int Health { get => _health; set => _health = Math.Clamp(value, 0, 100); }

        public Player(string name)
        {
            this.name = name;
            this.Health = 100;
        }
        public bool CanPerform(Action a)
        {
            return a switch {
                Action.Attack => true,
                Action.Die => true,
                Action.Move => true,
                _ => false,
            };
        }
        public override string ToString()
        {
            return name;
        }
    }
}
