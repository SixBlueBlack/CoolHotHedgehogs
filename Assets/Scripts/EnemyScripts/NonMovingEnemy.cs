using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class NonMovingEnemy : Enemy
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Shoot();
    }

    private void Rotate()
    {
        var playerPosition = player.position;
        var angle = Vector2.Angle(Vector2.right, playerPosition - transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < playerPosition.y ? angle : -angle);
    }

    void Shoot()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer > 0) return;
        cooldownTimer = EnemyModel.WeaponModel.FireDelay;
        var bullet = Instantiate(Bullet, transform.position, transform.rotation);
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * EnemyModel.WeaponModel.BulletForce, ForceMode2D.Impulse);
        bullet.GetComponent<Bullet>().BulletModel = EnemyModel.WeaponModel.BulletModel;
        bullet.GetComponent<Bullet>().IsEnemyBullet = true;
    }
}
