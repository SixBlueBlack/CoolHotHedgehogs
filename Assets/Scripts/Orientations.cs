using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Completed
{
    public class Orientation
    {
        public enum Position
        {
            Upper,
            Bottom,
            Right,
            Left
        }
        public enum Direction
        {
            Horizontal,
            Vertical
        }

        public static Direction PositionToDirection(Position pos) 
        {
            if (pos == Position.Bottom || pos == Position.Upper)
                return Direction.Horizontal;
            return Direction.Vertical;
        }

        public static Direction ReverseDirection(Direction dir)
        {
            if (dir == Direction.Horizontal)
                return Direction.Vertical;
            return Direction.Horizontal;
        }
    }
}
