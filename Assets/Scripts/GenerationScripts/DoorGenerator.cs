using UnityEngine;

namespace Assets.Scripts
{
    public class DoorGenerator : MonoBehaviour
    {
        public BoardManager BoardManager;
        public GameObject DoorTile;

        public void GenerateDoors(Passage corridor, Vector3 offset)
        {
            var isVertical = true;
            var position = offset + new Vector3(0, corridor.Length - 1, 0);
            if (corridor.Direction == Orientation.Direction.Horizontal)
            {
                isVertical = false;
                position = offset + new Vector3(corridor.Length - 1, 0, 0);
            }

            var door = Instantiate(DoorTile, offset, Quaternion.identity).GetComponent<Door>();
            door.DoorModel = corridor.Door1;
            door.DoorModel.IsVertical = isVertical;
            door.EnemySpawner = BoardManager.SpawnEnemies;

            door = Instantiate(DoorTile, position, Quaternion.identity).GetComponent<Door>();
            door.DoorModel = corridor.Door2;
            door.DoorModel.IsVertical = isVertical;
            door.EnemySpawner = BoardManager.SpawnEnemies;
        }
    }
}
