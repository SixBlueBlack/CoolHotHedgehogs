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
        public Count wallCount = new Count(5, 9);
        public GameObject[] floorTiles;     //Array of floor prefabs.
        public GameObject[] wallTiles;      //Array of wall prefabs.
        public GameObject[] outerWallTiles; //Array of outer tile prefabs.

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
                if (!wall.HasPath || (i != 3 && i != 4))
                    if (wall.Orientation == OuterWall.Orientations.Horizontal)
                        Instantiate(outerWallTiles[Random.Range(0, outerWallTiles.Length)],
                            new Vector3(i, 0, 0f) + offset, Quaternion.identity);
                    else
                        Instantiate(outerWallTiles[Random.Range(0, outerWallTiles.Length)],
                            new Vector3(0, i, 0f) + offset, Quaternion.identity);
        }

        private void GenerateRoom(Room room, Vector3 offset)
        {
            GenerateOuterWall(room.BottomWall, offset);
            GenerateOuterWall(room.LeftWall, offset + new Vector3(0, 1, 0));
            GenerateOuterWall(room.RightWall, offset + new Vector3(room.Columns, 0, 0));
            GenerateOuterWall(room.UpperWall, offset + new Vector3(1, room.Rows, 0));

            for (var x = 0; x < room.Columns; x++)
                for (var y = 0; y < room.Rows; y++)
                    Instantiate(floorTiles[Random.Range(0, floorTiles.Length)],
                        new Vector3(x, y, 0f) + offset, Quaternion.identity);
        }

        private void GenerateBoard(Board board)
        {
            for (var i = 0; i < board.Rows; i++)
                for (var j = 0; j < board.Columns; j++)
                    GenerateRoom(board.Field[i, j], new Vector3(j * board.RoomColumns, i * board.RoomRows, 0f));
        }

        public void SetupScene()
        {
            GenerateBoard(new Board(rows, columns, 8, 8));
            // LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        }
    }
}