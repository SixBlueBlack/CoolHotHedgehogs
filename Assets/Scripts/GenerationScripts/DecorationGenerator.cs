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

        private static int[] GetXRangesNOtTouchingPassage(int start, int end, Wall wall)
        {
            if (!wall.HasPath)
                return GetRandomRanges(start, end, new int[1] { (start + end) / 2 });

            return GetRandomRanges(1, end, new int[1] { (int)wall.GetPassageStart().x });
        }

        private static int[] GetRandomRanges(int start, int end, int[] restrictions)
        {
            var intervals = new int[restrictions.Length + 1];
            for (var i = 0; i < intervals.Length; i++)
            {
                intervals[i] = RandomGenerator.Range(i == 0 ? start : restrictions[i - 1] + 1,
                    i == intervals.Length - 1 ? end : restrictions[i] - 1);
            }
            return intervals;
        }

        private void GenerateUpperWallDecoration(Room room)
        {
            var xs = GetXRangesNOtTouchingPassage(1, room.Columns - 2, room.UpperWall);
            Instantiate(OnWallPrefabs[RandomGenerator.Range(0, OnWallPrefabs.Length)],
                    room.Offset + new Vector3(xs[0], room.Rows - 1), Quaternion.identity);
            Instantiate(OnWallPrefabs[RandomGenerator.Range(0, OnWallPrefabs.Length)],
                    room.Offset + new Vector3(xs[1], room.Rows - 1), Quaternion.identity);
        }

        private void GeneratePlants(Room room)
        {
            var prefabNum = PlantPrefabs.Length;
            Instantiate(PlantPrefabs[RandomGenerator.Range(0, prefabNum)],
                    room.Offset, Quaternion.identity).
                    GetComponent<SpriteRenderer>().sortingLayerName = "DecorationAbovePlayer";
            Instantiate(PlantPrefabs[RandomGenerator.Range(0, prefabNum)],
                room.Offset + new Vector3(room.Columns - 1, 0, 0), Quaternion.identity).
                GetComponent<SpriteRenderer>().sortingLayerName = "DecorationAbovePlayer";
            Instantiate(PlantPrefabs[RandomGenerator.Range(0, prefabNum)],
                room.Offset + new Vector3(0, room.Rows - 2, 0), Quaternion.identity);
            Instantiate(PlantPrefabs[RandomGenerator.Range(0, prefabNum)],
                room.Offset + new Vector3(room.Columns - 1, room.Rows - 2, 0), Quaternion.identity);
        }

        private void GenerateVendingMachines(Room room)
        {
            var prefab1 = VendingMachinePrefabs[RandomGenerator.Range(0, VendingMachinePrefabs.Length)];
            var prefab2 = VendingMachinePrefabs[RandomGenerator.Range(0, VendingMachinePrefabs.Length)];

            var xs = GetXRangesNOtTouchingPassage(1, room.Columns - 2, room.UpperWall);

            if (RandomGenerator.value >= VendingMachinesThreshold)
                Instantiate(prefab1, room.Offset + new Vector3(xs[0], room.Rows - 2, 0), Quaternion.identity);
            if (RandomGenerator.value >= VendingMachinesThreshold)
                Instantiate(prefab2, room.Offset + new Vector3(xs[1], room.Rows - 2, 0), Quaternion.identity);
        }

        private void GenerateOtherDecorations(Room room)
        {
            var prefabNum = OtherPrefabs.Length;

            if (RandomGenerator.value >= OtherDecorThreshold) // Duplicates (no)
            {
                var leftYIntervals = GetRandomRanges(1, room.Rows - 3,
                    room.LeftWall.HasPath ? new int[1] { (int)room.LeftWall.GetPassageStart().y } : new int[0]);
                var leftY = leftYIntervals[RandomGenerator.Range(0, leftYIntervals.Length)];
                Instantiate(OtherPrefabs[RandomGenerator.Range(0, prefabNum)],
                    room.Offset + new Vector3(0, leftY), Quaternion.identity);
            }
            if (RandomGenerator.value >= OtherDecorThreshold)
            {
                var rightYIntervals = GetRandomRanges(1, room.Rows - 3,
                    room.RightWall.HasPath ? new int[1] { (int)room.RightWall.GetPassageStart().y } : new int[0]);
                var rightY = rightYIntervals[RandomGenerator.Range(0, rightYIntervals.Length)];
                Instantiate(OtherPrefabs[RandomGenerator.Range(0, prefabNum)],
                    room.Offset + new Vector3(room.Columns - 1, rightY), Quaternion.identity);
            }
            if (RandomGenerator.value >= OtherDecorThreshold)
            {
                var bottomXIntervals = GetRandomRanges(1, room.Columns - 2,
                    room.BottomWall.HasPath ? new int[1] { (int)room.BottomWall.GetPassageStart().x } : new int[0]);
                var bottomX = bottomXIntervals[RandomGenerator.Range(0, bottomXIntervals.Length)];
                Instantiate(OtherPrefabs[RandomGenerator.Range(0, prefabNum)],
                    room.Offset + new Vector3(bottomX, 0), Quaternion.identity);
            }
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
