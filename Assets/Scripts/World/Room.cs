using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RandomGenerator = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Room
    {
        public Wall BottomWall { get; }
        public Wall UpperWall { get; }
        public Wall RightWall { get; }
        public Wall LeftWall { get; }

        public int Rows { get; }
        public int Columns { get; }

        public int Difficulty { get; }
        public EnemyModel[] Enemies { get; }
        public bool AllEnemiesDead { get => Enemies.All(enemy => enemy.IsDead || !enemy.IsSpawned); }

        public List<Decoration> Decorations { get; } = new List<Decoration>();

        public RoomType.TypeName TypeName { get; set; } = Utils.GetRandomFromEnum<RoomType.TypeName>();
        public RoomType Type { get; }

        public Vector3 Offset { get; }

        public Room(int rows, int columns, Vector3 offset, int difficulty,
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

            if (TypeName == RoomType.TypeName.Classroom)
                Type = new Classroom(this, Utils.GetRandomFromEnum<Classroom.AllColors>(),
                    Utils.GetRandomFromEnum<Orientation.Position>());
            if (TypeName == RoomType.TypeName.Tennis)
                Type = new Tennis(this);
        }

        public List<Decoration> GetAllDecorationsOfType(Decoration.DecorationType type)
        {
            var result = new List<Decoration>();
            foreach (var decor in Decorations)
            {
                if (decor.Type == type)
                    result.Add(decor);
            }
            return result;
        }

        public void Fill()
        {
            var availableTiles = new List<(int, int)>();
            for (var i = 0; i < Columns; i++)
                for (var j = 0; j < Rows; j++)
                    availableTiles.Add((i, j));

            FillWithGeneralDecors(availableTiles);
            Type.Fill(availableTiles);
            FillWithEnemies(availableTiles);
        }

        private void FillWithEnemies(List<(int, int)> availableTiles)
        {
            for (var i = 0; i < Difficulty; i++)
            {
                var ind = RandomGenerator.Range(0, availableTiles.Count);
                var (col, row) = availableTiles[ind];
                availableTiles.RemoveAt(ind);

                Enemies[i] = new EnemyModel(row, col, null, 100, 20, 1.5f);
            }
        }

        private void FillWithOtherDecors(List<(int, int)> availableTiles)
        {
            var leftYIntervals = LeftWall.GetTwoPointsNotTouchingPassage(1, Rows - 3);
            var leftY = leftYIntervals[RandomGenerator.Range(0, 2)];
            Decorations.Add(new Decoration(Offset + new Vector3(0, leftY), Decoration.DecorationType.Other));
            availableTiles.Remove((0, leftY));

            var rightYIntervals = RightWall.GetTwoPointsNotTouchingPassage(1, Rows - 3);
            var rightY = rightYIntervals[RandomGenerator.Range(0, 2)];
            Decorations.Add(new Decoration(Offset + new Vector3(Columns - 1, rightY), Decoration.DecorationType.Other));
            availableTiles.Remove((0, rightY));

            var bottomXIntervals = BottomWall.GetTwoPointsNotTouchingPassage(1, Columns - 3);
            var bottomX = bottomXIntervals[RandomGenerator.Range(0, 2)];
            Decorations.Add(new Decoration(Offset + new Vector3(bottomX, 0), Decoration.DecorationType.Other));
            availableTiles.Remove((bottomX, 0));
        }

        private void FillWithPlants(List<(int, int)> availableTiles)
        {
            Decorations.Add(new Decoration(Offset,
                Decoration.DecorationType.Plant, true));
            Decorations.Add(new Decoration(Offset + new Vector3(Columns - 1, 0, 0),
                Decoration.DecorationType.Plant, true));
            Decorations.Add(new Decoration(Offset + new Vector3(0, (float)(Rows - 1.5), 0),
                Decoration.DecorationType.Plant));
            Decorations.Add(new Decoration(Offset + new Vector3(Columns - 1, (float)(Rows - 1.5), 0),
                Decoration.DecorationType.Plant));
            availableTiles.Remove((0, 0));
            availableTiles.Remove((0, Rows - 1));
            availableTiles.Remove((Columns - 1, 0));
            availableTiles.Remove((Columns - 1, Rows - 1));
        }

        private void FillWithVendingMachines(List<(int, int)> availiableTiles)
        {
            var xs = UpperWall.GetTwoPointsNotTouchingPassage(1, Columns - 2);
            Decorations.Add(new Decoration(Offset + new Vector3(xs[0], (float)(Rows - 1.5), 0),
                Decoration.DecorationType.VendingMachine));
            Decorations.Add(new Decoration(Offset + new Vector3(xs[1], (float)(Rows - 1.5), 0),
                Decoration.DecorationType.VendingMachine));
            availiableTiles.Remove((xs[0], Rows - 1));
            availiableTiles.Remove((xs[1], Rows - 1));
        }

        private void FillWithGeneralDecors(List<(int, int)> availableTiles)
        {
            FillWithPlants(availableTiles);
            FillWithVendingMachines(availableTiles);
            FillWithOtherDecors(availableTiles);
        }
    }
}
