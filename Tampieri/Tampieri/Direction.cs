using System;

namespace Tampieri.Model
{
    /// <summary>Useful for representing directions</summary>
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    public static class DirectionExtentions
    {
        /// <summary>Convert a <c>Direction</c> to string, useful for file representation</summary>
        public static string ToString(this Direction d)
        {
            return d switch
            {
                Direction.Up => "up",
                Direction.Right => "right",
                Direction.Down => "down",
                Direction.Left => "left",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
