using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RandomGenerator = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Room
    {
        public enum RoomType
        {
            Tennis
        }

        public Wall BottomWall { get; }
        public Wall UpperWall { get; }
        public Wall RightWall { get; }
        public Wall LeftWall { get; }

        public int Rows { get; }
        public int Columns { get; }

        public int Difficulty { get; }
        public EnemyModel[] Enemies { get; }
        public bool AllEnemiesDead { get => Enemies.All(enemy => enemy.IsDead || !enemy.IsSpawned); }

        public RoomType Type { get; }

        public Vector3 Offset { get; }

        public Room(int rows, int columns, Vector3 offset, int difficulty, RoomType type,
            Wall bottomWall, Wall upperWall, Wall rightWall, Wall leftWall)
        {
            BottomWall = bottomWall;
            BottomWall.AttachRoom(this);
            UpperWall = upperWall;
            UpperWall.AttachRoom(this);
            RightWall = rightWall;
            RightWall.AttachRoom(this);
            LeftWall = leftWall;
            LeftWall.AttachRoom(this);

            Offset = offset;
            Rows = rows;
            Columns = columns;
            Difficulty = difficulty;
            Enemies = new EnemyModel[Difficulty];
            Type = type;

            Fill();
        }

        public static RoomType GetRandomRoomType()
        {
            var values = Enum.GetValues(typeof(RoomType));
            return (RoomType)values.GetValue(RandomGenerator.Range(0, values.Length));
        }

        public void Fill()
        {
            var availableTiles = new List<(int, int)>();
            for (var i = 1; i < Rows - 1; i++)
                for (var j = 1; j < Columns - 1; j++)
                    availableTiles.Add((i, j));

            for (var i = 0; i < Difficulty; i++)
            {
                var ind = RandomGenerator.Range(0, availableTiles.Count);
                var (row, col) = availableTiles[ind];
                availableTiles.RemoveAt(ind);

                Enemies[i] = new EnemyModel(row, col, null, 100, 20, 1.5f);
            }
        }
    }
}
