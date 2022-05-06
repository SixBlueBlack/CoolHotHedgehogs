namespace Assets.Scripts
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
            return pos == Position.Bottom || pos == Position.Upper ? Direction.Horizontal : Direction.Vertical;
        }

        public static Direction ReverseDirection(Direction dir)
        {
            return dir == Direction.Horizontal ? Direction.Vertical : Direction.Horizontal;
        }
    }
}
