using System;
using System.Collections.Generic;

namespace Tampieri.Model
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

        public static List<Direction> GetPossibleDirections(this Action a)
        {
            return a switch
            {
                Action.Attack => new List<Direction>() { Direction.Down, Direction.Left, Direction.Right, Direction.Up },
                Action.Move => new List<Direction>() { Direction.Down, Direction.Left, Direction.Right, Direction.Up },
                Action.Idle => new List<Direction>() { Direction.Left, Direction.Right},
                _ => new List<Direction>(),
            };
        }
    }
}
