using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Completed
{
    public class BoardManager : MonoBehaviour
    {
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
        public int roomRows = 8;
        public int roomColumns = 8;
        public GameObject floorTile;     //Array of floor prefabs.
        public GameObject horizontalWallTile;
        public GameObject verticalWallTIle;

        //A list of possible locations to place tiles.
        //private List<Vector3> gridPositions = new List<Vector3>();


        //Vector3 RandomPosition()
        //{
        //    int randomIndex = Random.Range(0, gridPositions.Count);
        //    var randomPosition = gridPositions[randomIndex];
        //    gridPositions.RemoveAt(randomIndex);

        //    return randomPosition;
        //}


        //void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        //{
        //    int objectCount = Random.Range(minimum, maximum + 1);

        //    for (int i = 0; i < objectCount; i++)
        //    {
        //        var randomPosition = RandomPosition();
        //        var tileChoice = tileArray[Random.Range(0, tileArray.Length)];
        //        Instantiate(tileChoice, randomPosition, Quaternion.identity);
        //    }
        //}

        private void GenerateOuterWall(OuterWall wall, Vector3 offset)
        {
            for (var i = 0; i < wall.Length; i++)
                if (!wall.HasPath || (i != 4))
                    if (wall.Orientation == OuterWall.Orientations.Horizontal)
                        Instantiate(horizontalWallTile,
                            new Vector3(i, 0, 0f) + offset, Quaternion.identity);
                    else
                        Instantiate(verticalWallTIle,
                            new Vector3(0, i, 0f) + offset, Quaternion.identity);
        }

        private void GenerateRoom(Room room, Vector3 offset)
        {
            GenerateOuterWall(room.BottomWall, offset);
            GenerateOuterWall(room.LeftWall, offset);
            GenerateOuterWall(room.RightWall, offset + new Vector3(room.Columns, 0, 0));
            GenerateOuterWall(room.UpperWall, offset + new Vector3(0, room.Rows, 0));

            for (var x = 0; x < room.Columns; x++)
                for (var y = 0; y < room.Rows; y++)
                    Instantiate(floorTile,
                        new Vector3(x, y, 0f) + offset, Quaternion.identity);
        }

        private void GenerateBoard(Board board)
        {
            for (var i = 0; i < board.Rows; i++)
                for (var j = 0; j < board.Columns; j++)
                    GenerateRoom(board.Field[i, j], new Vector3(j * board.RoomColumns + j, i * board.RoomRows + i, 0f));
        }

        public void SetupScene()
        {
            GenerateBoard(new Board(rows, columns, roomRows, roomColumns));
            // LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        }
    }
}