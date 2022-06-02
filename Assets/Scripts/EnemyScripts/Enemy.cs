using UnityEngine;
using System;


namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Rigidbody2D Physic;
        public HealthBar HealthBar;
        public GameObject WeaponObject;

        internal Transform Player { get; set; }
        public EnemyModel EnemyModel { get; set; }
        internal float Cooldown { get; set; } = 1;
        internal float CooldownTimer { get; set; } = 1;

        public void Start()
        {
            if (WeaponObject != null)
            {
                var inst = Instantiate(WeaponObject);
                inst.transform.SetParent(transform);
                Weapon weapon = EnemyModel.WeaponModel.WeaponType switch
                {
                    Weapon.TypeName.Rifle => inst.GetComponent<RifleWeapon>(),
                    Weapon.TypeName.Shotgun => inst.GetComponent<ShotgunWeapon>(),
                    _ => null
                };
                weapon!.weaponModel = EnemyModel.WeaponModel;
                EnemyModel.WeaponModel.Weapon = weapon;
            }

            Physic = GetComponent<Rigidbody2D>();
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            HealthBar.SetMaxHealth(EnemyModel.Health);
        }

        public void TakeDamage(int damage)
        {
            EnemyModel.Health = Math.Max(EnemyModel.Health - damage, 0);
            HealthBar.SetHealth(EnemyModel.Health);
            if (EnemyModel.Health == 0) Die();
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        public virtual void Shoot() { }
    }
}
