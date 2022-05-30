using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RandomGenerator = UnityEngine.Random;

namespace Assets.Scripts
{
    public class DecorationGenerator : MonoBehaviour
    {
        private double VendingMachinesThreshold { get; set; } = 0.3;
        private double OtherDecorThreshold { get; set; } = 0.5;

        public GameObject[] TennisTablePrefabs;

        public GameObject[] UpBrownDeskPrefabs;
        public GameObject[] DownBrownDeskPrefabs;
        public GameObject[] LeftBrownDeskPrefabs;
        public GameObject[] RightBrownDeskPrefabs;
        public GameObject[] UpGreenDeskPrefabs;
        public GameObject[] DownGreenDeskPrefabs;
        public GameObject[] LeftGreenDeskPrefabs;
        public GameObject[] RightGreenDeskPrefabs;
        public GameObject BlackboardPrefab;

        public GameObject[] PlantPrefabs;
        public GameObject[] VendingMachinePrefabs;
        public GameObject[] OtherPrefabs;
        public GameObject[] OnWallPrefabs;

        public void GenerateDecoration(Room room)
        {
            if (room.TypeName == RoomType.TypeName.Tennis)
                GenerateTennisSet((Tennis)room.Type);
            if (room.TypeName == RoomType.TypeName.Classroom)
                GenerateClassroom((Classroom)room.Type);
            GenerateGeneralDecoration(room);
        }

        private void GenerateGeneralDecoration(Room room)
        {
            GeneratePlants(room);
            GenerateVendingMachines(room);
            GenerateOtherDecorations(room);
            GenerateUpperWallDecoration(room);
        }

        private void GenerateUpperWallDecoration(Room room)
        {
            var xs = room.UpperWall.GetTwoPointsNotTouchingPassage(1, room.Columns - 2);
            Instantiate(OnWallPrefabs[RandomGenerator.Range(0, OnWallPrefabs.Length)],
                    room.Offset + new Vector3(xs[0], room.Rows - 1), Quaternion.identity);
            Instantiate(OnWallPrefabs[RandomGenerator.Range(0, OnWallPrefabs.Length)],
                    room.Offset + new Vector3(xs[1], room.Rows - 1), Quaternion.identity);
        }

        private void GeneratePlants(Room room)
        {
            foreach (var plant in room.GetAllDecorationsOfType(Decoration.DecorationType.Plant))
            {
                var inst = Instantiate(PlantPrefabs[RandomGenerator.Range(0, PlantPrefabs.Length)],
                    plant.Coordinate, Quaternion.identity);
                if (plant.AbovePlayer)
                    inst.GetComponent<SpriteRenderer>().sortingLayerName = "DecorationAbovePlayer";
            }
        }

        private void GenerateVendingMachines(Room room)
        {
            foreach (var vend in room.GetAllDecorationsOfType(Decoration.DecorationType.VendingMachine))
                if (RandomGenerator.value >= VendingMachinesThreshold)
                    Instantiate(VendingMachinePrefabs[RandomGenerator.Range(0, VendingMachinePrefabs.Length)],
                        vend.Coordinate, Quaternion.identity);
        }

        private void GenerateOtherDecorations(Room room)
        {
            foreach (var decor in room.GetAllDecorationsOfType(Decoration.DecorationType.Other))
                if (RandomGenerator.value >= OtherDecorThreshold)
                    Instantiate(OtherPrefabs[RandomGenerator.Range(0, OtherPrefabs.Length)],
                        decor.Coordinate, Quaternion.identity);
        }

        private void GenerateTennisSet(Tennis tennisSet)
        {
            foreach (var decor in tennisSet.Decorations)
                Instantiate(TennisTablePrefabs[RandomGenerator.Range(0, TennisTablePrefabs.Length)],
                    decor.Coordinate, Quaternion.identity);
        }

        private void GenerateClassroom(Classroom classroom)
        {
            var prefabSet = UpBrownDeskPrefabs;
            if (classroom.Color == Classroom.AllColors.Brown)
            {
                switch (classroom.Position)
                {
                    case Orientation.Position.Up:
                        prefabSet = UpBrownDeskPrefabs;
                        break;
                    case Orientation.Position.Down:
                        prefabSet = DownBrownDeskPrefabs;
                        break;
                    case Orientation.Position.Left:
                        prefabSet = LeftBrownDeskPrefabs;
                        break;
                    case Orientation.Position.Right:
                        prefabSet = RightBrownDeskPrefabs;
                        break;
                }
            }
            if (classroom.Color == Classroom.AllColors.Green)
            {
                switch (classroom.Position)
                {
                    case Orientation.Position.Up:
                        prefabSet = UpGreenDeskPrefabs;
                        break;
                    case Orientation.Position.Down:
                        prefabSet = DownGreenDeskPrefabs;
                        break;
                    case Orientation.Position.Left:
                        prefabSet = LeftGreenDeskPrefabs;
                        break;
                    case Orientation.Position.Right:
                        prefabSet = RightGreenDeskPrefabs;
                        break;
                }
            }

            foreach (var decor in classroom.Decorations)
            {
                if (decor.Type == Decoration.DecorationType.Desk)
                    Instantiate(prefabSet[RandomGenerator.Range(0, prefabSet.Length)], decor.Coordinate,
                        Quaternion.identity);
                if (decor.Type == Decoration.DecorationType.BlackBoard)
                    Instantiate(BlackboardPrefab, decor.Coordinate, Quaternion.identity);
            }
        }
    }
}
