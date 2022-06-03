namespace Assets.Scripts
{
    public class DoorModel
    {
        public bool Passed { get; set; } = false;
        public bool Disabled { get; set; } = false;
        public Orientation.Direction Direction { get; }
        public Passage AttachedToPassage { get; set; }
        public Room AttachedToRoom { get; set; }

        public DoorModel(Passage attachedToPassage, Room attachedToRoom)
        {
            Direction = attachedToPassage.Direction;
            AttachedToPassage = attachedToPassage;
            AttachedToRoom = attachedToRoom;
        }
    }
}
