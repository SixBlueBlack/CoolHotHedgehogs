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

        public GameObject[] EnemyPrefabs;
        public Sprite[] BulletSprites;

        internal RoomGenerator RoomGeneratorScript;

        public void Start()
        {
            RoomGeneratorScript = GetComponent<RoomGenerator>();
            var wallGeneratorScript = GetComponent<WallGenerator>();
            var cornerGeneratorScript = GetComponent<CornerGenerator>();

            RoomGeneratorScript.WallGeneratorScript = wallGeneratorScript;
            RoomGeneratorScript.CornerGeneratorScript = cornerGeneratorScript;
            RoomGeneratorScript.DecorationGenerator = GetComponent<DecorationGenerator>(); // Transfer this to the start of RoomGen

            wallGeneratorScript.CornerGeneratorScript = cornerGeneratorScript;
            wallGeneratorScript.DoorGenerator = GetComponent<DoorGenerator>();
            wallGeneratorScript.DoorGenerator.BoardManager = this;

            GenerateRooms(new Board(boardSizeRange, roomWallRange, passageLength));
        }

        public void SpawnEnemies(IEnumerable<EnemyModel> enemies, Vector3 offset)
        {
            foreach (var enemyModel in enemies)
            {
                var enemyIndex = RandomGenerator.Range(0, EnemyPrefabs.Length);
                var enemyPrefab = EnemyPrefabs[enemyIndex];
                var inst = Instantiate(enemyPrefab, 
                    offset + new Vector3(enemyModel.Column, enemyModel.Row, 0), Quaternion.identity);

                enemyModel.IsSpawned = true;
                enemyModel.WeaponModel = new WeaponModel(
                    new BulletModel(10, 20, new Vector2(1.7f, 1.7f), BulletSprites[0]),
                    1f, 20f, null);
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