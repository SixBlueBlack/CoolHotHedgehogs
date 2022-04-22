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

        public Board(int rows, int columns, int roomRows, int roomColumns)
        {
            Rows = rows;
            Columns = columns;
            RoomRows = roomRows;
            RoomColumns = roomColumns;

            Field = new Room[rows, columns];
            var vertices = RimmsGeneration.InitSquareGraph(rows, columns);
            RimmsGeneration.ConnectRandomSpanningTree(vertices.Values.ToArray());

            for (var i = 0; i < Rows; i++)
                for (var j = 0; j < Columns; j++)
                {
                    var v = vertices[(i, j)];
                    var upperWall = new OuterWall(roomColumns, OuterWall.Orientations.Horizontal,
                        v.UpperEdge != null && v.UpperEdge.Actual);
                    var rightWall = new OuterWall(roomRows, OuterWall.Orientations.Vertical, 
                        v.RightEdge != null && v.RightEdge.Actual);
                    Field[i, j] = new Room(roomRows, roomColumns,
                        i == 0 ? null : Field[i - 1, j].UpperWall,
                        i == Rows - 1 ? null : upperWall,
                        j == Columns - 1 ? null : rightWall,
                        j == 0 ? null : Field[i, j - 1].RightWall);
                }
        }
    }
}
