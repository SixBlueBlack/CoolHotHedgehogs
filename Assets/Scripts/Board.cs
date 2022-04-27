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
                    Field[i, j] = new Room(roomRows, roomColumns,
                        i == 0 ? null : new OuterWall(roomColumns, OuterWall.Orientations.Bottom, v.BottomEdge is { Actual: true }),
                        i == Rows - 1 ? null : new OuterWall(roomColumns, OuterWall.Orientations.Upper, v.UpperEdge is { Actual: true }),
                        j == Columns - 1 ? null : new OuterWall(roomRows, OuterWall.Orientations.Right, v.RightEdge is { Actual: true }),
                        j == 0 ? null : new OuterWall(roomRows, OuterWall.Orientations.Left, v.LeftEdge is { Actual: true }));
                }
        }
    }
}
