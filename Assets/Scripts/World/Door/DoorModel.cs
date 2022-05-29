namespace Assets.Scripts
{
    public class DoorModel
    {
        public bool Passed { get; set; } = false;
        public bool Disabled { get; set; } = false;
        public bool IsVertical { get; set; }
        public Passage AttachedToPassage { get; set; }
        public Room AttachedToRoom { get; set; }

        public DoorModel(Passage attachedToPassage, Room attachedToRoom)
        {
            AttachedToPassage = attachedToPassage;
            AttachedToRoom = attachedToRoom;
        }
    }
}
