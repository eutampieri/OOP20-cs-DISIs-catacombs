using System;
using Tampieri.Ui;

namespace Tampieri.Model
{
    /// <summary>The player, whose movements are dictated by the gamer</summary>
    sealed public class Player : IAnimatable, ILivingCharacter
    {
        private int _health;
        private string name;
        ///<summary>The caracter's health, ranging from 0 to 100</summary>
        public int Health { get => _health; set => _health = Math.Clamp(value, 0, 100); }

        public Player(string name)
        {
            this.name = name;
            this.Health = 100;
        }
        public bool CanPerform(Action a)
        {
            return a switch
            {
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
