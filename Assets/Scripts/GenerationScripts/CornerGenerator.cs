using UnityEngine;

namespace Assets.Scripts
{
    public class CornerGenerator : MonoBehaviour
    {
        public GameObject cornerTile;

        public void GenerateCorridorCorners(Passage corridor, Vector3 offset)
        {
            if (corridor.Direction == Orientation.Direction.Horizontal)
            {
                Instantiate(cornerTile, offset, Quaternion.identity);
                Instantiate(cornerTile, new Vector3(0, 1, 0) + offset, Quaternion.Euler(0, 0, 270));
                Instantiate(cornerTile, new Vector3(corridor.Length - 1, 0, 0) + offset, Quaternion.Euler(0, 0, 90));
                Instantiate(cornerTile, new Vector3(corridor.Length - 1, 1, 0) + offset, Quaternion.Euler(0, 0, 180));
            }
            else
            {
                Instantiate(cornerTile, new Vector3(-1, 0, 0) + offset, Quaternion.Euler(0, 0, 180));
                Instantiate(cornerTile, new Vector3(1, 0, 0) + offset, Quaternion.Euler(0, 0, 270));
                Instantiate(cornerTile, new Vector3(-1, corridor.Length, 0) + offset, Quaternion.Euler(0, 0, 90));
                Instantiate(cornerTile, new Vector3(1, corridor.Length, 0) + offset, Quaternion.identity);
            }
        }

        public void GenerateRoomCorners(Room room)
        {
            Instantiate(cornerTile, room.Offset + new Vector3(-1, 0, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(cornerTile, new Vector3(-1, room.Rows - 1, 0) + room.Offset, Quaternion.Euler(0, 0, 180));
            Instantiate(cornerTile, new Vector3(room.Columns, room.Rows - 1, 0) + room.Offset, Quaternion.Euler(0, 0, 270));
            Instantiate(cornerTile, new Vector3(room.Columns, 0, 0) + room.Offset, Quaternion.Euler(0, 0, 0));
        }

    }
}
