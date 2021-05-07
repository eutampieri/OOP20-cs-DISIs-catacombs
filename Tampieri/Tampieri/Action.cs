using System;
using System.Collections.Generic;

namespace Tampieri.Model
{
    /// <summary>The actions a character can perform</summary>
    public enum Action
    {
        /// <summary>When the caracter changes its position, i.e. when it walks</summary>
        Move,
        /// <summary>When a character initiates an hostile action</summary>
        Attack,
        /// <summary>Do nothing</summary>
        Idle,
        /// <summary>This entity is no more! He has ceased to be! 'E's expired and gone to meet 'is maker! 'E's a
        ///stiff! Bereft of life, 'e rests in peace! If you hadn't nailed 'im to the perch 'e'd be
        /// pushing up the daisies! 'Is metabolic processes are now 'istory! 'E's off the twig! 'E's kicked
        /// the bucket, 'e's shuffled off 'is mortal coil, run down the curtain and joined the bleedin' choir
        /// invisibile!! THIS IS AN EX-ENTITY!!</summary>
        Die,

    }
    public static class ActionExtension
    {
        /// <summary>Convert an entity to string, useful for file representation</summary>
        public static string ToString(this Action a)
        {
            return a.ToString().ToLower();
        }

        /// <summary>Get the corresponding action for a string. It can be null if the string is invalid</summary>
        public static Action? ToAction(this String s)
        {
            return s switch
            {
                "move" => Action.Move,
                "attack" => Action.Attack,
                "idle" => Action.Idle,
                "die" => Action.Die,
                _ => null,
            };
        }

        /// <summary>Get possible directions for an action</summary>
        /// <returns>A list of <c>Action</c>s</returns>
        public static List<Direction> GetPossibleDirections(this Action a)
        {
            return a switch
            {
                Action.Attack => new List<Direction>() { Direction.Down, Direction.Left, Direction.Right, Direction.Up },
                Action.Move => new List<Direction>() { Direction.Down, Direction.Left, Direction.Right, Direction.Up },
                Action.Idle => new List<Direction>() { Direction.Left, Direction.Right },
                _ => new List<Direction>(),
            };
        }
    }
}
