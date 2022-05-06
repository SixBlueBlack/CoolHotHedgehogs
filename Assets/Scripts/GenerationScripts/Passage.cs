namespace Assets.Scripts
{
    public class Passage
    {
        public Orientation.Direction Direction { get; }

        public OuterWall StartWall { get; }
        public OuterWall EndWall { get; set; }

        public int Length { get; }

        public Passage(OuterWall startWall, Orientation.Direction direction, Range passageLength)
        {
            StartWall = startWall;
            Direction = direction;
            Length = passageLength.Random;
        }

        public OuterWall GetAnotherWall(OuterWall wall)
        {
            return wall == StartWall ? EndWall : StartWall;
        }
    }
}
