using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class DecorationGenerator : MonoBehaviour
    {
        public GameObject TennisTablePrefab;

        public void GenerateDecoration(Room room)
        {
            if (room.Type == Room.RoomType.Tennis)
                GenerateTennisSet(room);
        }

        public void GenerateTennisSet(Room room)
        {
            Instantiate(TennisTablePrefab, new Vector3(room.Columns / 2, room.Rows / 2, 0) + room.Offset, Quaternion.identity);
        }
    }
}
