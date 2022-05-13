using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class DoorGenerator : MonoBehaviour
    {
        public GameObject doorPrefab;

        public void GenerateDoors(Passage corridor, Vector3 offset)
        {
            Instantiate(doorPrefab, offset, Quaternion.identity)
                .GetComponent<Door>().DoorModel = corridor.door1;
            if (corridor.Direction == Orientation.Direction.Horizontal)
                Instantiate(doorPrefab, offset + new Vector3(corridor.Length, 0, 0), Quaternion.identity)
                    .GetComponent<Door>().DoorModel = corridor.door2;
            else
                Instantiate(doorPrefab, offset + new Vector3(0, corridor.Length, 0), Quaternion.identity)
                    .GetComponent<Door>().DoorModel = corridor.door2;
        }
    }
}
