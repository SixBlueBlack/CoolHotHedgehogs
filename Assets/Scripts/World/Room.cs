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
        public bool AllEnemiesDead => Enemies.All(enemy => enemy.IsDead || !enemy.IsSpawned);

        public List<Decoration> Decorations { get; } = new List<Decoration>();

        public RoomType.TypeName TypeName { get; set; } = Utils.GetRandomFromEnum<RoomType.TypeName>();
        public bool WithBoss { get; set; }
        public RoomType Type { get; }

        public Vector3 Offset { get; }

        public Room(int rows, int columns, Vector3 offset, int difficulty,
            Wall bottomWall, Wall upperWall, Wall rightWall, Wall leftWall, bool withBoss=false)
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
            Enemies = withBoss ? new EnemyModel[7] : new EnemyModel[Difficulty];

            WithBoss = withBoss;

            Type = TypeName switch
            {
                RoomType.TypeName.Classroom => new Classroom(this, Utils.GetRandomFromEnum<Classroom.AllColors>(),
                    Utils.GetRandomFromEnum<Orientation.Position>()),
                RoomType.TypeName.Tennis => new Tennis(this),
                _ => Type
            };
        }

        public List<Decoration> GetDecorationsOfType(Decoration.DecorationType type)
        {
            return Decorations.Where(decor => decor.Type == type).ToList();
        }

        public void Fill()
        {
            var availableTiles = new List<(int, int)>();
            for (var i = 0; i < Columns; i++)
                for (var j = 0; j < Rows; j++)
                    availableTiles.Add((i, j));

            FillWithGeneralDecors(availableTiles);

            if (WithBoss)
                FillWithBoss(availableTiles);
            else
            {
                if (Type != null)
                    Type.Fill(availableTiles);
                FillWithEnemies(availableTiles);
            }
        }

        public List<EnemyModel> GetEnemiesOfType(EnemyModel.EnemyType type, int? limit=null)
        {
            if (limit == null)
                limit = Enemies.Length;

            var res = new List<EnemyModel>();
            foreach (var enemyModel in Enemies)
                if (enemyModel.Type == type)
                {
                    res.Add(enemyModel);
                    if (limit == res.Count)
                        return res;
                }

            return res;
        }

        private void FillWithBoss(ICollection<(int, int)> availableTiles)
        {
            var row = Rows / 2;
            var col = Columns / 2;

            availableTiles.Remove((row, col));

            Enemies[0] = new EnemyModel(row, col, EnemyModel.EnemyType.Boss, this);
            for (var i = 1; i <= 2; i++)
                Enemies[i] = new EnemyModel(0, 0, EnemyModel.EnemyType.SmallBoss, this);
            for (var i = 3; i < Enemies.Length; i++)
                Enemies[i] = new EnemyModel(0, 0, EnemyModel.EnemyType.Warrior, this);
        }

        private void FillWithEnemies(ICollection<(int, int)> availableTiles)
        {
            for (var i = 0; i < Difficulty; i++)
            {
                var row = RandomGenerator.Range(2, Rows - 2);
                var col = RandomGenerator.Range(2, Columns - 2);
                while (!availableTiles.Contains((col, row)))
                {
                    row = RandomGenerator.Range(2, Rows - 2);
                    col = RandomGenerator.Range(2, Columns - 2);
                }
                availableTiles.Remove((col, row));

                Enemies[i] = new EnemyModel(row, col, Utils.GetRandomFromEnum<EnemyModel.EnemyType>(
                    new HashSet<EnemyModel.EnemyType>() { EnemyModel.EnemyType.Boss, EnemyModel.EnemyType.SmallBoss }), this);
            }
        }

        private void FillWithOtherDecors(ICollection<(int, int)> availableTiles)
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

        private void FillWithPlants(ICollection<(int, int)> availableTiles)
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

        private void FillWithVendingMachines(ICollection<(int, int)> availableTiles)
        {
            var xs = UpperWall.GetTwoPointsNotTouchingPassage(1, Columns - 2);
            Decorations.Add(new Decoration(Offset + new Vector3(xs[0], (float)(Rows - 1.5), 0),
                Decoration.DecorationType.VendingMachine));
            Decorations.Add(new Decoration(Offset + new Vector3(xs[1], (float)(Rows - 1.5), 0),
                Decoration.DecorationType.VendingMachine));
            availableTiles.Remove((xs[0], Rows - 1));
            availableTiles.Remove((xs[1], Rows - 1));
        }

        private void FillWithGeneralDecors(ICollection<(int, int)> availableTiles)
        {
            FillWithPlants(availableTiles);
            FillWithVendingMachines(availableTiles);
            FillWithOtherDecors(availableTiles);
        }
    }
}
