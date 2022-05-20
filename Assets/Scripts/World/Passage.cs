namespace Assets.Scripts
{
    public class Passage
    {
        public Orientation.Direction Direction { get; }

        public Wall StartWall { get; set; }
        public Wall EndWall { get; set; }

        public DoorModel Door1 { get; set; }
        public DoorModel Door2 { get; set; }

        public int Length { get; }

        public Passage(Wall startWall, Orientation.Direction direction, Range passageLength)
        {
            StartWall = startWall;
            Direction = direction;
            Length = passageLength.Random;
        }

        public void AttachEndWall(Wall endWall)
        {
            EndWall = endWall;
            Door1 = new DoorModel(this, StartWall.AttachedTo);
            Door2 = new DoorModel(this, EndWall.AttachedTo);
        }

        public Wall GetAnotherWall(Wall wall)
        {
            return wall == StartWall ? EndWall : StartWall;
        }

        public DoorModel GetAnotherDoor(DoorModel door)
        {
            return door == Door1 ? Door2 : Door1;
        }
    }
}
