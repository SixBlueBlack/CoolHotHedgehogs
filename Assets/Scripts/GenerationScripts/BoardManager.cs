using UnityEngine;
using System;
using System.Collections.Generic;
using RandomGenerator = UnityEngine.Random;

namespace Assets.Scripts
{
    [Serializable]
    public class Range
    {
        public int minimum;
        public int maximum;
        public int Random { get => RandomGenerator.Range(minimum, maximum); }

        public Range(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public class BoardManager : MonoBehaviour
    {
        public Range passageLength;
        public Range roomWallRange;
        public Range boardSizeRange;

        public GameObject floorTile;
        public GameObject upperWallTile;
        public GameObject bottomWallTile;
        public GameObject verticalWallTIle;
        public GameObject cornerTile;

        public GameObject[] EnemyPrefabs;
        public Sprite[] BulletSprites;

        public RoomGenerator RoomGeneratorScript;
        public DoorGenerator DoorGenerator;

        public void Start()
        {
            RoomGeneratorScript = GetComponent<RoomGenerator>();
            DoorGenerator = GetComponent<DoorGenerator>();
            var wallGeneratorScript = GetComponent<WallGenerator>();
            var cornerGeneratorScript = GetComponent<CornerGenerator>();

            RoomGeneratorScript.WallGeneratorScript = wallGeneratorScript;
            RoomGeneratorScript.CornerGeneratorScript = cornerGeneratorScript;
            wallGeneratorScript.CornerGeneratorScript = cornerGeneratorScript;

            GenerateRooms(new Board(boardSizeRange, roomWallRange, passageLength));
        }

        public void SpawnEnemies(IEnumerable<EnemyModel> enemies, Vector3 offset)
        {
            foreach (var enemyModel in enemies)
            {
                var inst = Instantiate(EnemyPrefabs[1], 
                    offset + new Vector3(enemyModel.Column, enemyModel.Row, 0), Quaternion.identity);
                enemyModel.WeaponModel =
                    new WeaponModel(new BulletModel(10, 20, new Vector2(1.7f, 1.7f), BulletSprites[0]), 1f, 20f, null);
                inst.GetComponent<Enemy>().EnemyModel = enemyModel;
            }
        }

        private void GenerateRooms(Board board)
        {
            for (var i = 0; i < board.Rows; i++)
                for (var j = 0; j < board.Columns; j++)
                    RoomGeneratorScript.GenerateRoom(board.Field[i, j]);
        }
    }
}