using System.Linq;
using UnityEngine;


namespace Assets.Scripts
{
    public class Board
    {
        public Room[,] Field { get; }
        public int Rows { get; }
        public int Columns { get; }
        public Range PassageLength { get; }
        public int DefaultGap { get => (PassageLength.maximum + PassageLength.minimum) / 2; }

        public Board(Range boardSizeRange, Range roomWallRange, Range passageLength)
        {
            Rows = boardSizeRange.Random;
            Columns = boardSizeRange.Random;
            PassageLength = passageLength;

            Field = new Room[Rows, Columns];
            var vertices = RimmsGeneration.InitGraph(Rows, Columns);
            RimmsGeneration.ConnectRandomSpanningTree(vertices.Values.ToArray());

            var offset = new Vector3(0, 0, 0);
            for (var i = 0; i < Rows; i++)
            {
                offset.x = 0;
                for (var j = 0; j < Columns; j++)
                {
                    offset = UpdateOffset(offset, i, j);
                    var roomRows = roomWallRange.Random;
                    var roomColumns = roomWallRange.Random;

                    var v = vertices[(i, j)];

                    var leftWall = j == 0 ?
                        CreateWall(roomRows, Orientation.Position.Left, v.LeftEdge, null): 
                        CreateWall(roomRows, Orientation.Position.Left, v.LeftEdge, Field[i, j - 1].RightWall.Corridor);
                    var bottomWall = i == 0 ?
                        CreateWall(roomColumns, Orientation.Position.Bottom, v.BottomEdge, null): 
                        CreateWall(roomColumns, Orientation.Position.Bottom, v.BottomEdge, Field[i - 1, j].UpperWall.Corridor);
                    var upperWall = CreateWall(roomColumns, Orientation.Position.Upper, v.UpperEdge, null);
                    var rightWall = CreateWall(roomRows, Orientation.Position.Right, v.RightEdge, null);

                    var room = new Room(roomRows, roomColumns, offset, i + j,
                        bottomWall, upperWall, rightWall, leftWall);

                    Field[i, j] = room;
                }
            }

            for (var i = 0; i < Rows; i++)
                for (var j = 0; j < Columns; j++)
                    Field[i, j].Fill();
        }

        private Wall CreateWall(int length, Orientation.Position pos, Edge edge, Passage corridor)
        {
            return corridor == null ?
                new Wall(length, pos, edge is {Actual : true }, PassageLength) :
                new Wall(length, pos, corridor);
        }

        private Vector3 UpdateOffset(Vector3 offset, int i, int j)
        {
            if (j != 0)
                offset.x += Field[i, j - 1].Columns +
                    (Field[i, j - 1].RightWall.HasPath ? Field[i, j - 1].RightWall.Corridor.Length : DefaultGap);
            if (i != 0)
                offset.y = -1 + Field[i - 1, j].Offset.y + Field[i - 1, j].Rows +
                    (Field[i - 1, j].UpperWall.HasPath ? Field[i - 1, j].UpperWall.Corridor.Length : DefaultGap);
            return offset;
        }
    }
}
