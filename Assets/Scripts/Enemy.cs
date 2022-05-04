using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Rigidbody2D Physic;
        public Transform Player;
        public EnemyModel EnemyModel { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            Physic = GetComponent<Rigidbody2D>();
            Player = GameObject.Find("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            var distToPlayer = Vector2.Distance(transform.position, Player.position);

            if (distToPlayer < EnemyModel.DistanceForAgr) ShowAggression(Player.position, transform.position);
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

        public void TakeDamage(float damage)
        {
            EnemyModel.Health -= Math.Max(damage, 0);
            if (EnemyModel.Health == 0) Die();
        }

        public void Die()
        {
            //Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
