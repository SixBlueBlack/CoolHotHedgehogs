using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Completed
{
    public class Room
    {
        public OuterWall BottomWall { get; }
        public OuterWall UpperWall { get; }
        public OuterWall RightWall { get; }
        public OuterWall LeftWall { get; }

        public int Rows { get; }
        public int Columns { get; }

        public Room(int rows, int columns,
            OuterWall bottomWall=null, OuterWall upperWall=null, OuterWall rightWall=null, OuterWall leftWall=null)
        {
            BottomWall = bottomWall ?? new OuterWall(columns, OuterWall.Orientations.Bottom, false);
            UpperWall = upperWall ?? new OuterWall(columns, OuterWall.Orientations.Upper, false);
            RightWall = rightWall ?? new OuterWall(rows, OuterWall.Orientations.Right, false);
            LeftWall = leftWall ?? new OuterWall(rows, OuterWall.Orientations.Left, false);

            Rows = rows;
            Columns = columns;
        }
    }
}
