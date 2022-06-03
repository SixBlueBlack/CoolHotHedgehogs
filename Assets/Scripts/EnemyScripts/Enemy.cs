using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Linq;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Rigidbody2D Physic;
        public HealthBar HealthBar;
        public GameObject WeaponObject;
        public AudioSource[] DamageAudios { get; set; }

        internal Transform Player { get; set; }
        public EnemyModel EnemyModel { get; set; }
        internal float Cooldown { get; set; } = 1;
        internal float CooldownTimer { get; set; } = 1;
        public GameObject DropItem;
        private bool IsDestroyed { get; set; }

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
                    Weapon.TypeName.Circle => inst.GetComponent<CircleWeapon>(),
                    _ => null
                };
                weapon!.weaponModel = EnemyModel.WeaponModel;
                EnemyModel.WeaponModel.Weapon = weapon;
            }

            DamageAudios = GetComponents<AudioSource>();
            Physic = GetComponent<Rigidbody2D>();
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            HealthBar.SetMaxHealth(EnemyModel.Health);
        }

        public void TakeDamage(int damage)
        {
            if (DamageAudios.Length != 0 && !DamageAudios.Any(audio => audio.isPlaying))
                DamageAudios[Random.Range(0, DamageAudios.Length)].Play();

            EnemyModel.Health = Math.Max(EnemyModel.Health - damage, 0);
            HealthBar.SetHealth(EnemyModel.Health);
            if (EnemyModel.Health == 0) Die();
        }

        public void Heal(int value)
        {
            TakeDamage(Math.Max(-value, -((int)HealthBar.Slider.maxValue - EnemyModel.Health)));
        }

        public virtual void Die()
        {
            if (IsDestroyed) return;
            if (Random.value >= 0.25)
            {
                var inst = Instantiate(DropItem, transform.position, Quaternion.identity);
                inst.GetComponent<Item>().itemModel = new ItemModel("Health Potion", "Just heal yourself", DropItem.GetComponent<SpriteRenderer>().sprite);
            }
            if (EnemyModel.AttachedToRoom.WithBoss && EnemyModel.AttachedToRoom.AllEnemiesDead)
                global::Player.IsWin = true;
            Destroy(gameObject);
            IsDestroyed = true;
        }

        public virtual void Shoot() { }

        public virtual void Move(float distToPlayer) { }
    }
}
