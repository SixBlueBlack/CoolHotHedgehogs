namespace Assets.Scripts
{
    public class Orientation
    {
        public enum Position
        {
            Up,
            Down,
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
            return pos == Position.Down || pos == Position.Up ? Direction.Horizontal : Direction.Vertical;
        }

        public static Direction ReverseDirection(Direction dir)
        {
            return dir == Direction.Horizontal ? Direction.Vertical : Direction.Horizontal;
        }
    }
}
