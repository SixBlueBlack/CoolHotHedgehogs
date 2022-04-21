using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Enemy
{
    public float health;
    public GameObject deathEffect;
    public Transform player;
    public Enemy(float hp, Transform player)
    {
        this.health = hp;
        this.player = player;
    }

    //public void TakeDamage(float damage)
    //{
    //    health = Math.Max(damage, 0);

    //    if (health == 0) Die();
    //}

    //public void Die()
    //{
    //    Instantiate(deathEffect, transform.position, Quaternion.identity);
    //    Destroy(gameObject);
    //}
}

public class StalkerEnemy : Enemy
{
    public float speed;
    public Transform q;

    public StalkerEnemy(float enemysSpeed, float hp, Transform player):base(hp, player)
    { 
        this.speed = enemysSpeed;

    }

    public void ShowAgression(Vector3 playerPos, Vector3 enemyPos)
    {
        float xVelocity = 0;
        float yVelocity = 0;

        if (playerPos.x < enemyPos.x) xVelocity = -speed;
        else if (playerPos.x > enemyPos.x) xVelocity = speed;

        if (playerPos.y < enemyPos.y) yVelocity = -speed;
        else if (playerPos.y > enemyPos.y) yVelocity = speed;

        EnemyScript.physic.velocity = new Vector2(xVelocity, yVelocity);
    }

    public void LoseInterest()
    {
        EnemyScript.physic.velocity = new Vector2(0, 0);
    }
}

public class EnemyScript : MonoBehaviour
{
    public static Rigidbody2D physic;
    public Transform player;
    public float speed;
    public float distanceForAgr;
    public float hp;
    public StalkerEnemy stalkerEnemy;

    // Start is called before the first frame update
    void Start()
    {
        physic = GetComponent<Rigidbody2D>();
        stalkerEnemy = new StalkerEnemy(speed, hp, player);
    }

    // Update is called once per frame
    void Update()
    {
        var distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < distanceForAgr) stalkerEnemy.ShowAgression(player.position, transform.position);
        else stalkerEnemy.LoseInterest();
    }
}
