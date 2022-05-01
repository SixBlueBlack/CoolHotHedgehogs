using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Completed
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

    public class BoardManager : MonoBehaviour
    {
        public Count passageLength;
        public int columns = 8;
        public int rows = 8;
        public int roomRows = 8;
        public int roomColumns = 8;
        public GameObject floorTile;
        public GameObject upperWallTile;
        public GameObject bottomWallTile;
        public GameObject verticalWallTIle;
        public GameObject CornerTile;

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

        private void GenerateCorners(Room room)
        {
            Instantiate(CornerTile, room.Offset, Quaternion.identity);
            Instantiate(CornerTile, new Vector3(0, room.Rows - 1, 0) + room.Offset, Quaternion.Euler(0, 0, 270));
            Instantiate(CornerTile, new Vector3(room.Columns, room.Rows - 1, 0) + room.Offset, Quaternion.Euler(0, 0, 270));
            Instantiate(CornerTile, new Vector3(room.Columns, 0, 0) + room.Offset, Quaternion.Euler(0, 0, 0));
        }

        private void GenerateRoomFloor(Room room)
        {
            for (var x = 0; x < room.Columns; x++)
                for (var y = 0; y < room.Rows; y++)
                    Instantiate(floorTile, new Vector3(x, y, 0f) + room.Offset, Quaternion.identity);
        }

        private void GenerateCorridor(Passage corridor, Vector3 offset)
        {
            for (var i = 0; i < corridor.Length; i++)
                if (corridor.Direction == Orientation.Direction.Horizontal)
                {
                    Instantiate(upperWallTile, new Vector3(i, 1, 0) + offset, Quaternion.identity);
                    Instantiate(bottomWallTile, new Vector3(i, 0, 0) + offset, Quaternion.identity);
                    Instantiate(floorTile, new Vector3(i, 0, 0) + offset, Quaternion.identity);
                }
                else
                {
                    Instantiate(verticalWallTIle, new Vector3(1, i, 0) + offset, Quaternion.identity);
                    Instantiate(verticalWallTIle, new Vector3(0, i, 0) + offset, Quaternion.identity);
                    Instantiate(floorTile, new Vector3(0, i, 0) + offset, Quaternion.identity);
                }
        }

        private void GenerateOuterWall(OuterWall wall, Vector3 offset)
        {
            var passageStartVector = new Vector3(-1, 0, 0);
            if (wall.HasPath)
            {
                passageStartVector = wall.GetPassageStart();

                if (wall.Corridor.StartWall == wall)
                    GenerateCorridor(wall.Corridor, passageStartVector + offset);
            }

            for (var i = 0; i < wall.Length; i++)
            {
                var wallVector = new Vector3(0, i, 0);
                if (Orientation.PositionToDirection(wall.Position) == Orientation.Direction.Horizontal)
                    wallVector = new Vector3(i, 0, 0);

                if (wall.HasPath && wallVector == passageStartVector)
                    continue;

                if (wall.Position == Orientation.Position.Bottom)
                    Instantiate(bottomWallTile, wallVector + offset, Quaternion.identity);
                else if (wall.Position == Orientation.Position.Upper)
                    Instantiate(upperWallTile, wallVector + offset, Quaternion.identity);
                else
                    Instantiate(verticalWallTIle, wallVector + offset, Quaternion.identity);
            }
        }

        private void GenerateRoom(Room room)
        {
            GenerateCorners(room);

            GenerateOuterWall(room.BottomWall, room.Offset);
            GenerateOuterWall(room.LeftWall, room.Offset + new Vector3(0, 0, 0));
            GenerateOuterWall(room.RightWall, room.Offset + new Vector3(room.Columns, 0, 0));
            GenerateOuterWall(room.UpperWall, room.Offset + new Vector3(0, room.Rows - 1, 0));

            GenerateRoomFloor(room);
        }

        private void GenerateBoard(Board board)
        {
            for (var i = 0; i < board.Rows; i++)
                for (var j = 0; j < board.Columns; j++)
                    GenerateRoom(board.Field[i, j]);
        }

        public void SetupScene()
        {
            GenerateBoard(new Board(rows, columns, roomRows, roomColumns, passageLength));
            // LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        }
    }
}