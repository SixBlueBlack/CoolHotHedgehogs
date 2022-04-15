using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Completed
{
    public class BoardManager : MonoBehaviour
    {
        public class OuterWall
        {
            public enum Orientations
            {
                Horizontal,
                Vertical
            }
            public int Length { get; }
            public Orientations Orientation { get; }
            public GameObject[] Tiles { get; }
            public bool HasPath { get; }

            public OuterWall(int length, Orientations orientation, bool hasPath, GameObject[] tiles)
            {
                Length = length;
                HasPath = hasPath;
                Orientation = orientation;
                Tiles = tiles;
            }

            public void Generate(Vector3 offset)
            {
                for (var i = 0; i < Length; i++)
                    if (!HasPath || (i != 4 && i != 5))
                        if (Orientation == Orientations.Horizontal)
                            Instantiate(Tiles[Random.Range(0, Tiles.Length)],
                                new Vector3(i, 0, 0f) + offset, Quaternion.identity);
                        else
                            Instantiate(Tiles[Random.Range(0, Tiles.Length)],
                                new Vector3(0, i, 0f) + offset, Quaternion.identity);
            }
        }

        public class Board
        {
            public Room[,] Field { get; }
            public GameObject[] FloorTiles { get; }
            public GameObject[] OuterWallTiles { get; }
            public GameObject[] WallTiles { get; }
            public int Rows { get; }
            public int Columns { get; }
            public int RoomRows { get; }
            public int RoomColumns { get; }

            public Board(int rows, int columns, int roomRows, int roomColumns,
                GameObject[] floorTiles, GameObject[] outerWallTiles, GameObject[] wallTiles)
            {
                Rows = rows;
                Columns = columns;
                RoomRows = roomRows;
                RoomColumns = roomColumns;
                FloorTiles = floorTiles;
                OuterWallTiles = outerWallTiles;
                WallTiles = wallTiles;

                Field = new Room[rows, columns];
                var possiblePathsNumber = new int[rows, columns];

                for (var i = 0; i < rows; i++)
                    for (var j = 0; j < columns; j++)
                    {
                        var paths = 4;
                        if (j == 0 || j == rows - 1)
                            paths--;
                        if (i == 0 || i == columns - 1)
                            paths--;
                        possiblePathsNumber[i, j] = paths;
                    }

                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        var upperWall = new OuterWall(roomColumns, OuterWall.Orientations.Horizontal,
                            RandomizePath(i, j, i + 1, j, possiblePathsNumber), OuterWallTiles);
                        var rightWall = new OuterWall(roomColumns, OuterWall.Orientations.Vertical,
                            RandomizePath(i, j, i, j + 1, possiblePathsNumber), OuterWallTiles);
                        OuterWall bottomWall;
                        if (i != 0)
                            bottomWall = Field[i - 1, j].UpperWall;
                        else
                            bottomWall = new OuterWall(roomColumns, OuterWall.Orientations.Horizontal,
                                RandomizePath(i, j, i - 1, j, possiblePathsNumber), OuterWallTiles);
                        OuterWall leftWall;
                        if (j != 0)
                            leftWall = Field[i, j - 1].RightWall;
                        else
                            leftWall = new OuterWall(roomColumns, OuterWall.Orientations.Vertical,
                            RandomizePath(i, j, i, j - 1, possiblePathsNumber), OuterWallTiles);

                        Field[i, j] = new Room(roomRows, roomColumns, floorTiles, outerWallTiles,
                            bottomWall, upperWall, rightWall, leftWall);
                    }
                }
            }

            private bool RandomizePath(int iFrom, int jFrom, int iTo, int jTo, int[,] possiblePathsNumber)
            {
                if (iTo >= Rows || jTo >= Columns || iTo < 0 || jTo < 0)
                    return false;

                var randomCoef = 1.0 / Math.Min(possiblePathsNumber[iTo, jTo], possiblePathsNumber[iFrom, jFrom]);
                if (Math.Abs(1 - randomCoef) > 1e-5)
                    randomCoef *= 0.5;
                var hasPath = Random.value <= randomCoef;

                possiblePathsNumber[iTo, jTo]--;
                possiblePathsNumber[iFrom, jFrom]--;

                return hasPath;
            }

            public void Generate()
            {
                for (var i = 0; i < Rows; i++)
                    for (var j = 0; j < Columns; j++)
                        Field[i, j].Generate(new Vector3(j * RoomColumns, i * RoomRows, 0f));
            }
        }

        public class Room
        {
            public OuterWall BottomWall { get; }
            public OuterWall UpperWall { get; }
            public OuterWall RightWall { get; }
            public OuterWall LeftWall { get; }

            public int Rows { get; }
            public int Columns { get; }
            GameObject[] FloorTiles { get; }
            GameObject[] WallTiles { get; }

            public Room(int rows, int columns, GameObject[] floorTiles, GameObject[] wallTiles,
                OuterWall bottomWall, OuterWall upperWall, OuterWall rightWall, OuterWall leftWall)
            {
                FloorTiles = floorTiles;
                WallTiles = wallTiles;

                BottomWall = bottomWall;
                UpperWall = upperWall;
                RightWall = rightWall;
                LeftWall = leftWall;

                Rows = rows;
                Columns = columns;
            }

            public void Generate(Vector3 offset)
            {
                BottomWall.Generate(offset);
                LeftWall.Generate(offset + new Vector3(0, 1, 0));
                RightWall.Generate(offset + new Vector3(Columns, 0, 0));
                UpperWall.Generate(offset + new Vector3(1, Rows, 0));

                for (var x = 0; x < Columns; x++)
                    for (var y = 0; y < Rows; y++)
                        Instantiate(FloorTiles[Random.Range(0, FloorTiles.Length)],
                            new Vector3(x, y, 0f) + offset, Quaternion.identity);
            }
        }

        // Using Serializable allows us to embed a class with sub properties in the inspector.
        [Serializable]
        public class Count
        {
            public int minimum;
            public int maximum;


            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }


        public int columns = 8;             //Number of columns in our game board.
        public int rows = 8;                //Number of rows in our game board.
        public Count wallCount = new Count(5, 9);
        public GameObject[] floorTiles;     //Array of floor prefabs.
        public GameObject[] wallTiles;      //Array of wall prefabs.
        public GameObject[] outerWallTiles; //Array of outer tile prefabs.

        //A list of possible locations to place tiles.
        private List<Vector3> gridPositions = new List<Vector3>();


        Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);
            var randomPosition = gridPositions[randomIndex];
            gridPositions.RemoveAt(randomIndex);

            return randomPosition;
        }


        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            int objectCount = Random.Range(minimum, maximum + 1);

            for (int i = 0; i < objectCount; i++)
            {
                var randomPosition = RandomPosition();
                var tileChoice = tileArray[Random.Range(0, tileArray.Length)];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }


        public void SetupScene()
        {
            var board = new Board(rows, columns, 8, 8, floorTiles, outerWallTiles, wallTiles);
            board.Generate();
            // LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        }
    }
}