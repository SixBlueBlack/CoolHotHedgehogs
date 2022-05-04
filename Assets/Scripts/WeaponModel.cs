using UnityEngine;

namespace Assets.Scripts
{
    public class WeaponModel
    {
        public BulletModel BulletModel;

        public float BulletForce { get; }

        public float FireDelay { get; }

        public Sprite Sprite { get; }

        public WeaponModel(BulletModel bulletModel, float fireDelay, float bulletForce, Sprite sprite)
        {
            BulletModel = bulletModel;
            FireDelay = fireDelay;
            BulletForce = bulletForce;
            Sprite = sprite;
        }
    }
}