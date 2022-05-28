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
        public GameObject[] TennisTablePrefabs;
        public GameObject[] PlantPrefabs;

        public void GenerateDecoration(Room room)
        {
            if (room.Type == Room.RoomType.Tennis)
                GenerateTennisSet(room);
            GenerateGeneralDecoration(room);
        }

        public void GeneratePlants(Room room)
        {
            Instantiate(PlantPrefabs[RandomGenerator.Range(0, 1)],
                    room.Offset, Quaternion.identity).
                    GetComponent<SpriteRenderer>().sortingLayerName = "DecorationAbovePlayer";
            Instantiate(PlantPrefabs[RandomGenerator.Range(0, 1)],
                room.Offset + new Vector3(room.Columns - 1, 0, 0), Quaternion.identity).
                GetComponent<SpriteRenderer>().sortingLayerName = "DecorationAbovePlayer";
            Instantiate(PlantPrefabs[RandomGenerator.Range(0, 1)],
                room.Offset + new Vector3(0, room.Rows - 2, 0), Quaternion.identity);
            Instantiate(PlantPrefabs[RandomGenerator.Range(0, 1)],
                room.Offset + new Vector3(room.Columns - 1, room.Rows - 2, 0), Quaternion.identity);
        }

        public void GenerateGeneralDecoration(Room room)
        {
            if (RandomGenerator.value >= 0.8)
                GeneratePlants(room);
        }

        public void GenerateTennisSet(Room room)
        {
            var ind = RandomGenerator.Range(0, TennisTablePrefabs.Length);
            Instantiate(TennisTablePrefabs[ind],
                new Vector3(room.Columns / 2, room.Rows / 2, 0) + room.Offset, Quaternion.identity);
        }
    }
}
