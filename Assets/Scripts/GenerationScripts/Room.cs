using System.Collections.Generic;
using UnityEngine;
using RandomGenerator = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Room
    {
        public OuterWall BottomWall { get; }
        public OuterWall UpperWall { get; }
        public OuterWall RightWall { get; }
        public OuterWall LeftWall { get; }

        public int Rows { get; }
        public int Columns { get; }

        public int Difficulty { get; }
        public EnemyModel[] Enemies { get; }

        public Vector3 Offset { get; set; }

        public Room(int rows, int columns, Vector3 offset, int difficulty,
            OuterWall bottomWall, OuterWall upperWall, OuterWall rightWall, OuterWall leftWall)
        {
            BottomWall = bottomWall;
            BottomWall.AttachedTo = this;
            UpperWall = upperWall;
            UpperWall.AttachedTo = this;
            RightWall = rightWall;
            RightWall.AttachedTo = this;
            LeftWall = leftWall;
            LeftWall.AttachedTo = this;

            Offset = offset;
            Rows = rows;
            Columns = columns;
            Difficulty = difficulty;
            Enemies = new EnemyModel[Difficulty];

            Fill();
        }

        public void Fill()
        {
            var availiableTiles = new List<(int, int)>();
            for (var i = 1; i < Rows - 1; i++)
                for (var j = 1; j < Columns - 1; j++)
                    availiableTiles.Add((i, j));

            for (var i = 0; i < Difficulty; i++)
            {
                var ind = RandomGenerator.Range(0, availiableTiles.Count);
                var (row, col) = availiableTiles[ind];
                availiableTiles.RemoveAt(ind);

                Enemies[i] = new EnemyModel(row, col, null, 100, 20, 1.5f);
            }
        }
    }
}
