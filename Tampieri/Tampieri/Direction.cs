using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampieri
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    public static class DirectionExtentions
    {
        public static string ToString(this Direction d)
        {
            return d switch
            {
                Direction.Up => "up",
                Direction.Right => "right",
                Direction.Down => "down",
                Direction.Left => "left",
            };
        }
    }
}
