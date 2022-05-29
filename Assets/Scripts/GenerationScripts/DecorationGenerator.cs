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
        private double TennisSetThreshold { get; set; } = 0.2;
        private double OtherDecorThreshold { get; set; } = 0.5;

        public GameObject[] TennisTablePrefabs;

        public GameObject[] PlantPrefabs;
        public GameObject[] VendingMachinePrefabs;
        public GameObject[] OtherPrefabs;
        public GameObject[] OnWallPrefabs;

        public void GenerateDecoration(Room room)
        {
            if (room.Type == Room.RoomType.Tennis)
                GenerateTennisSet(room);
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

        private void GenerateTennisSet(Room room)
        {
            var ind = RandomGenerator.Range(0, TennisTablePrefabs.Length);
            if (RandomGenerator.value >= TennisSetThreshold)
                Instantiate(TennisTablePrefabs[ind],
                    new Vector3(room.Columns / 2, room.Rows / 2, 0) + room.Offset, Quaternion.identity);
        }
    }
}
