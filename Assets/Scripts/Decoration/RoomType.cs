using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGenerator = UnityEngine.Random;

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

        public RoomType(Room room)
        {
            Room = room;
            Decorations = new List<Decoration>();
        }

        public abstract void Fill(List<(int, int)> availableTiles);
    }
}
