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
        public BoardManager BoardManager;
        public GameObject DoorTile;

        public void GenerateDoors(Passage corridor, Vector3 offset)
        {
            var angle = Quaternion.identity;
            var position = offset + new Vector3(0, corridor.Length - 1, 0);
            if (corridor.Direction == Orientation.Direction.Horizontal)
            {
                angle = Quaternion.Euler(new Vector3(0, 0, 270));
                position = offset + new Vector3(corridor.Length - 1, 0, 0);
            }

            var door = Instantiate(DoorTile, offset, angle).GetComponent<Door>();
            door.DoorModel = corridor.Door1;
            door.EnemySpawner = BoardManager.SpawnEnemies;

            door = Instantiate(DoorTile, position, angle).GetComponent<Door>();
            door.DoorModel = corridor.Door2;
            door.EnemySpawner = BoardManager.SpawnEnemies;
        }
    }
}
