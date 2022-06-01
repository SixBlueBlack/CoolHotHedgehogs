using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Classroom : RoomType
    {
        public enum AllColors
        {
            Brown,
            Green
        }
        public AllColors Color { get; }

        public Orientation.Position Position { get; }

        public Classroom(Room room, AllColors color, Orientation.Position position) : base(room)
        {
            Color = color;
            Position = position;
        }

        public override void Fill(List<(int, int)> availableTiles)
        {
            for (var x = Room.Columns / 4; x < Room.Columns / 4 + 6; x += 2)
            {
                for (var y = Room.Rows / 5; y < Room.Rows / 5 + 6; y += 2)
                {
                    Decorations.Add(new Decoration(Room.Offset + new Vector3(x, y),
                        Decoration.DecorationType.Desk));
                    availableTiles.Remove((x, y));
                }
            }

            if (Position != Orientation.Position.Up) return;
            Decorations.Add(new Decoration(Room.Offset + new Vector3(Room.Columns / 4 + 2, (float)(Room.Rows / 5 + 5.5)),
                Decoration.DecorationType.BlackBoard));
            availableTiles.Remove((Room.Columns / 4 + 2, Room.Rows / 5 + 5));
        }
    }
}
