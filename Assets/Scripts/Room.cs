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

        public Vector3 Offset { get; set; }

        public Room(int rows, int columns, Vector3 offset,
            OuterWall bottomWall, OuterWall upperWall, OuterWall rightWall, OuterWall leftWall)
        {
            BottomWall = bottomWall;
            BottomWall.AttachedTo = this;
            UpperWall = upperWall;
            UpperWall.AttachedTo = this;
            RightWall = rightWall;
            RightWall.AttachedTo = this;
            LeftWall = leftWall;
            LeftWall.AttachedTo = this;

            Offset = offset;
            Rows = rows;
            Columns = columns;
        }
    }
}
