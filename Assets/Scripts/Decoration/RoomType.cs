using System.Collections.Generic;

namespace Assets.Scripts
{
    public abstract class RoomType
    {
        public List<Decoration> Decorations { get; }
        public Room Room { get; }

        public enum TypeName
        {
            Tennis,
            Classroom
        }

        protected RoomType(Room room)
        {
            Room = room;
            Decorations = new List<Decoration>();
        }

        public abstract void Fill(List<(int, int)> availableTiles);
    }
}
