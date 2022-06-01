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
        public int Random => RandomGenerator.Range(minimum, maximum);

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
        public GameObject[] WeaponPrefabs;

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
                enemyModel.WeaponModel = new WeaponModel(new BulletModel(10, 20), 1f, 20f, Weapon.TypeName.Rifle, null);
                //var weaponInst = Instantiate(WeaponPrefabs[0])
                inst.GetComponent<Enemy>().EnemyModel = enemyModel;
            }
        }

        //public static void AddWeaponScripts(GameObject[] newWeaponObjects, List<WeaponModel> newWeaponModels, bool isPresent = true)
        //{
        //    if (!isPresent)
        //        foreach (var weaponModel in newWeaponModels)
        //            WeaponModels.Add(weaponModel);

        //    for (var i = 0; i < newWeaponModels.Count; i++)
        //    {
        //        var inst = Instantiate(newWeaponObjects[i]);
        //        inst.transform.SetParent(FindObjectOfType<PlayerWeaponScript>().transform);
        //        Weapon weapon = newWeaponModels[i].WeaponType switch
        //        {
        //            Weapon.TypeName.Rifle => inst.GetComponent<RifleWeapon>(),
        //            Weapon.TypeName.Shotgun => inst.GetComponent<ShotgunWeapon>(),
        //            _ => null
        //        };
        //        weapon!.weaponModel = WeaponModels[i];
        //        WeaponModels[WeaponModels.Count - newWeaponModels.Count + i].Weapon = weapon;
        //    }
        //}

        private void GenerateRooms(Board board)
        {
            for (var i = 0; i < board.Rows; i++)
                for (var j = 0; j < board.Columns; j++)
                    RoomGeneratorScript.GenerateRoom(board.Field[i, j]);
        }
    }
}