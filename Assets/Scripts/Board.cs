using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Completed
{
    public class Board
    {
        public Room[,] Field { get; }
        public int Rows { get; }
        public int Columns { get; }
        public int RoomRows { get; }
        public int RoomColumns { get; }
        public Count PassageLength { get; }

        public Board(int rows, int columns, int roomRows, int roomColumns, Count passageLength)
        {
            Rows = rows;
            Columns = columns;
            RoomRows = roomRows;
            RoomColumns = roomColumns;
            PassageLength = passageLength;

            Field = new Room[rows, columns];
            var vertices = RimmsGeneration.InitSquareGraph(rows, columns);
            RimmsGeneration.ConnectRandomSpanningTree(vertices.Values.ToArray());

            var offset = new Vector3(0, 0, 0);
            for (var i = 0; i < Rows; i++)
            {
                offset.x = 0;
                for (var j = 0; j < Columns; j++)
                {
                    offset = UpdateOffset(offset, i, j);

                    var v = vertices[(i, j)];

                    var leftWall = j == 0 ?
                        GetWall(RoomRows, Orientation.Position.Left, v.LeftEdge, null): 
                        GetWall(RoomRows, Orientation.Position.Left, v.LeftEdge, Field[i, j - 1].RightWall.Corridor);
                    var bottomWall = i == 0 ?
                        GetWall(roomColumns, Orientation.Position.Bottom, v.BottomEdge, null): 
                        GetWall(RoomColumns, Orientation.Position.Bottom, v.BottomEdge, Field[i - 1, j].UpperWall.Corridor);
                    var upperWall = GetWall(RoomColumns, Orientation.Position.Upper, v.UpperEdge, null);
                    var rightWall = GetWall(RoomRows, Orientation.Position.Right, v.RightEdge, null);

                    var room = new Room(roomRows, roomColumns, offset, 
                        bottomWall, upperWall, rightWall, leftWall);

                    Field[i, j] = room;
                }
            }
        }

        private OuterWall GetWall(int length, Orientation.Position pos, Edge edge, Passage corridor)
        {
            if (corridor == null)
                return new OuterWall(length, pos, edge is {Actual : true }, PassageLength);
            return new OuterWall(length, pos, corridor);
        }

        private Vector3 UpdateOffset(Vector3 offset, int i, int j)
        {
            var defaultGap = (PassageLength.maximum + PassageLength.minimum) / 2;
            if (j != 0)
                offset.x += Field[i, j - 1].Columns +
                    (Field[i, j - 1].RightWall.HasPath ? Field[i, j - 1].RightWall.Corridor.Length : defaultGap);
            if (i != 0)
                offset.y = -1 + Field[i - 1, j].Offset.y + Field[i - 1, j].Rows +
                    (Field[i - 1, j].UpperWall.HasPath ? Field[i - 1, j].UpperWall.Corridor.Length : defaultGap);
            return offset;
        }
    }
}
