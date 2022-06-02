using UnityEngine;

namespace Assets.Scripts
{
    public class WeaponModel
    {
        public BulletModel BulletModel;

        public float BulletForce { get; }

        public float FireDelay { get; }

        public Weapon.TypeName WeaponType { get; set; }

        public Weapon Weapon { get; set; } = null;

        public WeaponModel(BulletModel bulletModel, float fireDelay, float bulletForce, Weapon.TypeName weaponType)
        {
            BulletModel = bulletModel;
            FireDelay = fireDelay;
            BulletForce = bulletForce;
            WeaponType = weaponType;
        }
    }
}