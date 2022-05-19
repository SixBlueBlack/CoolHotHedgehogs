using UnityEngine;
using System;


namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Rigidbody2D Physic;
        internal Transform player;
        public EnemyModel EnemyModel { get; set; }
        public HealthBar HealthBar;
        internal float cooldown = 1;
        internal float cooldownTimer;
        public GameObject Bullet;

        public void Start()
        {
            Physic = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            HealthBar.SetMaxHealth(EnemyModel.Health);
        }
        
        public void TakeDamage(int damage)
        {
            EnemyModel.Health = Math.Max(EnemyModel.Health - damage, 0);
            HealthBar.SetHealth(EnemyModel.Health);
            if (EnemyModel.Health == 0) Die();
        }

        public void Die()
        {
            //Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
