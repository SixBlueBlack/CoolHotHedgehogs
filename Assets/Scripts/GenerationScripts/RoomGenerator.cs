﻿using UnityEngine;

namespace Assets.Scripts
{
    public class RoomGenerator : MonoBehaviour
    {
        public GameObject floorTile;

        public WallGenerator WallGeneratorScript; // Internal
        public CornerGenerator CornerGeneratorScript;
        public DecorationGenerator DecorationGenerator;

        public void GenerateRoomFloor(Room room)
        {
            for (var x = 0; x < room.Columns; x++)
                for (var y = 0; y < room.Rows; y++)
                    Instantiate(floorTile, new Vector3(x, y, 0f) + room.Offset, Quaternion.identity);
        }

        public void GenerateOuterWall(Wall wall, Vector3 offset)
        {
            var passageStartVector = new Vector3(-1, 0, 0);
            if (wall.HasPath)
            {
                passageStartVector = wall.GetPassageStart();

                if (wall.Corridor.StartWall == wall)
                    WallGeneratorScript.GenerateCorridor(wall.Corridor, passageStartVector + offset);
            }

            WallGeneratorScript.GenerateRoomWalls(wall, passageStartVector, offset);
        }

        public void GenerateRoom(Room room)
        {
            CornerGeneratorScript.GenerateRoomCorners(room);

            GenerateOuterWall(room.BottomWall, room.Offset);
            GenerateOuterWall(room.LeftWall, room.Offset + new Vector3(0, 0, 0));
            GenerateOuterWall(room.RightWall, room.Offset + new Vector3(room.Columns, 0, 0));
            GenerateOuterWall(room.UpperWall, room.Offset + new Vector3(0, room.Rows - 1, 0));

            DecorationGenerator.GenerateDecoration(room);

            GenerateRoomFloor(room);
        }
    }
}
