using UnityEngine;
using System;


namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Rigidbody2D Physic;
        private Transform player;
        public EnemyModel EnemyModel { get; set; }
        public HealthBar HealthBar;
        private float cooldown = 1;
        private float cooldownTimer;
        private bool isAttacking;

        // Start is called before the first frame update
        void Start()
        {
            Physic = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            HealthBar.SetMaxHealth(EnemyModel.Health);
        }

        // Update is called once per frame
        void Update()
        {
            var distToPlayer = Vector2.Distance(transform.position, player.position);
            if (isAttacking)
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer > 0) return;
                cooldownTimer = cooldown;
                player.GetComponent<Player>().TakeDamage(EnemyModel.Damage);
            }

            if (distToPlayer < EnemyModel.DistanceForAgr) ShowAggression(player.position, transform.position);
            else LoseInterest();
        }

        public void ShowAggression(Vector3 playerPos, Vector3 enemyPos)
        {
            float xVelocity = 0;
            float yVelocity = 0;

            if (playerPos.x < enemyPos.x) xVelocity = -EnemyModel.Speed;
            else if (playerPos.x > enemyPos.x) xVelocity = EnemyModel.Speed;

            if (playerPos.y < enemyPos.y) yVelocity = -EnemyModel.Speed;
            else if (playerPos.y > enemyPos.y) yVelocity = EnemyModel.Speed;

            Physic.velocity = new Vector2(xVelocity, yVelocity);
        }

        public void LoseInterest()
        {
            Physic.velocity = new Vector2(0, 0);
        }

        public void TakeDamage(int damage)
        {
            EnemyModel.Health -= Math.Max(damage, 0);
            HealthBar.SetHealth(EnemyModel.Health);
            if (EnemyModel.Health == 0) Die();
        }

        public void Die()
        {
            //Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (hitInfo.tag == "Player")
                isAttacking = true;
        }

        void OnTriggerExit2D(Collider2D hitInfo)
        {
            if (hitInfo.tag == "Player")
                isAttacking = false;
        }
    }
}
