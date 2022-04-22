using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyModel
{
    public WeaponModel WeaponModel { get; }

    public float Health { get; set; }

    public float Damage { get; }
    public float Speed { get; }

    //public GameObject DeathEffect { get; }

    public EnemyModel(WeaponModel weaponModel, float hp, float damage, float speed)
    {
        Health = hp;
        WeaponModel = weaponModel;
        Damage = damage;
        Speed = speed;
    }
}

public class EnemyScript : MonoBehaviour
{
    public static Rigidbody2D Physic;
    public Transform Player;
    public float DistanceForAgr = 5;
    public EnemyModel[] Enemies;

    // Start is called before the first frame update
    void Start()
    {
        Enemies = new[]
        {
            new EnemyModel(null, 100, 20, 1.5f) ,
            new EnemyModel(null, 100, 5, 1.5f)
        };
        Physic = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var distToPlayer = Vector2.Distance(transform.position, Player.position);

        if (distToPlayer < DistanceForAgr) ShowAggression(Player.position, transform.position);
        else LoseInterest();
    }

    public void ShowAggression(Vector3 playerPos, Vector3 enemyPos)
    {
        float xVelocity = 0;
        float yVelocity = 0;

        if (playerPos.x < enemyPos.x) xVelocity = -Enemies[0].Speed;
        else if (playerPos.x > enemyPos.x) xVelocity = Enemies[0].Speed;

        if (playerPos.y < enemyPos.y) yVelocity = -Enemies[0].Speed;
        else if (playerPos.y > enemyPos.y) yVelocity = Enemies[0].Speed;

        Physic.velocity = new Vector2(xVelocity, yVelocity);
    }

    public void LoseInterest()
    {
        Physic.velocity = new Vector2(0, 0);
    }

    public void TakeDamage(float damage)
    {
        Enemies[0].Health -= Math.Max(damage, 0);
        if (Enemies[0].Health == 0) Die();
    }

    public void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
