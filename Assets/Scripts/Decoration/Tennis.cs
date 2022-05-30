using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tennis : RoomType
    {
        public Tennis(Room room) : base(room) { }

        public override void Fill(List<(int, int)> availableTiles)
        {
            Decorations.Add(new Decoration(new Vector3(Room.Columns / 2, Room.Rows / 2, 0) + Room.Offset,
                Decoration.DecorationType.TennisTable));
            availableTiles.Remove((Room.Columns / 2, Room.Rows / 2));
        }
    }
}
