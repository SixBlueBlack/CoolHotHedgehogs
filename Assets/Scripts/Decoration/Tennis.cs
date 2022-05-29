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
        public Tennis(Room room)
        {
            Decorations.Add(new Decoration(new Vector3(room.Columns / 2, room.Rows / 2, 0) + room.Offset,
                Decoration.DecorationType.TennisTable));
        }
    }
}
