using Assets.Scripts;
using UnityEngine;

public class CircleWeapon : Weapon
{
    public override void Shoot(Vector3 position, Quaternion rotation, bool isEnemyBullet = false)
    {
        for (var i = 0; i < 6; i++)
        {
            var bullet = Instantiate(Bullet, position, Quaternion.Euler(0, 0, 60 * i));
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * weaponModel.BulletForce, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().BulletModel = weaponModel.BulletModel;
            bullet.GetComponent<Bullet>().IsEnemyBullet = isEnemyBullet;
        }
    }
}
