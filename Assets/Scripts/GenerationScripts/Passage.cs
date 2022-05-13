namespace Assets.Scripts
{
    public class Passage
    {
        public Orientation.Direction Direction { get; }

        public Wall StartWall { get; }
        public Wall EndWall { get; set; }

        public DoorModel door1 { get; }
        public DoorModel door2 { get; }

        public int Length { get; }

        public Passage(Wall startWall, Orientation.Direction direction, Range passageLength)
        {
            StartWall = startWall;
            Direction = direction;
            Length = passageLength.Random;

            door1 = new DoorModel(this);
            door2 = new DoorModel(this);
        }

        public Wall GetAnotherWall(Wall wall)
        {
            return wall == StartWall ? EndWall : StartWall;
        }

        public DoorModel GetAnotherDoor(DoorModel door)
        {
            return door == door1 ? door2 : door1;
        }
    }
}
