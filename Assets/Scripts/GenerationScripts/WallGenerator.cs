using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class WallGenerator : MonoBehaviour
    {
        public GameObject upperWallTile;
        public GameObject bottomWallTile;
        public GameObject verticalWallTIle;
        public GameObject floorTile;

        public CornerGenerator CornerGeneratorScript;

        public void GenerateCorridor(Passage corridor, Vector3 offset)
        {
            CornerGeneratorScript.GenerateCorridorCorners(corridor, offset);
            for (var i = 0; i < corridor.Length; i++)
                if (corridor.Direction == Orientation.Direction.Horizontal)
                {
                    Instantiate(upperWallTile, new Vector3(i, 1, 0) + offset, Quaternion.identity)
                        .GetComponent<SpriteRenderer>().sortingOrder++;
                    Instantiate(bottomWallTile, new Vector3(i, 0, 0) + offset, Quaternion.identity);
                    Instantiate(floorTile, new Vector3(i, 0, 0) + offset, Quaternion.identity);
                }
                else
                {
                    Instantiate(verticalWallTIle, new Vector3(1, i, 0) + offset, Quaternion.identity)
                        .GetComponent<SpriteRenderer>().sortingOrder--;
                    Instantiate(verticalWallTIle, new Vector3(-1, i, 0) + offset, Quaternion.Euler(0, 0, 180)).
                        GetComponent<SpriteRenderer>().sortingOrder--;
                    Instantiate(floorTile, new Vector3(0, i, 0) + offset, Quaternion.identity);
                }
        }

        public void GenerateRoomWalls(Wall wall, Vector3 passageStartVector, Vector3 offset)
        {
            for (var i = 0; i < wall.Length; i++)
            {
                var wallVector = new Vector3(0, i, 0);
                if (Orientation.PositionToDirection(wall.Position) == Orientation.Direction.Horizontal)
                    wallVector = new Vector3(i, 0, 0);

                if (wall.HasPath && wallVector == passageStartVector)
                    continue;

                if (wall.Position == Orientation.Position.Bottom)
                    Instantiate(bottomWallTile, wallVector + offset, Quaternion.identity);
                else if (wall.Position == Orientation.Position.Upper)
                    Instantiate(upperWallTile, wallVector + offset, Quaternion.identity);
                else if (wall.Position == Orientation.Position.Left)
                    Instantiate(verticalWallTIle, wallVector + offset + new Vector3(-1, 0, 0), Quaternion.Euler(0, 0, 180));
                else
                    Instantiate(verticalWallTIle, wallVector + offset, Quaternion.identity);
            }
        }
    }
}
