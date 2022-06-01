using Assets.Scripts;
using UnityEngine;

public class RifleWeapon : Weapon
{
    public override void Shoot(Vector3 position, Quaternion rotation, bool isEnemyBullet = false)
    {
        var bullet = Instantiate(Bullet, position, rotation);
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * weaponModel.BulletForce, ForceMode2D.Impulse);
        bullet.GetComponent<Bullet>().BulletModel = weaponModel.BulletModel;
        bullet.GetComponent<Bullet>().IsEnemyBullet = isEnemyBullet;
    }
}
