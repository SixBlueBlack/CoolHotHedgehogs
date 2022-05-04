using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class EnemyModel
    {
        public WeaponModel WeaponModel { get; }

        public float Health { get; set; }
        public float Damage { get; }
        public float Speed { get; }
        public float DistanceForAgr { get; } = 10;

        public int Row { get; }
        public int Column { get; }

        //public GameObject DeathEffect { get; }

        public EnemyModel(int row, int col, WeaponModel weaponModel, float hp, float damage, float speed) // Pass type of an enemy instead of params
        {
            Health = hp;
            WeaponModel = weaponModel;
            Damage = damage;
            Speed = speed;
            Row = row;
            Column = col;
        }
    }
}
