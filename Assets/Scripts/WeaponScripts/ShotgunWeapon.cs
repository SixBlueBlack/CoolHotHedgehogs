using Assets.Scripts;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    public override void Shoot(Vector3 position, Quaternion rotation, bool isEnemyBullet = false)
    {
        ShootingSound.Play();

        for (var i = 0; i < 5; i++)
        {
            var bullet = Instantiate(Bullet, position, rotation * Quaternion.Euler(0, 0, 7.5f * i - 15f));
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * weaponModel.BulletForce, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().BulletModel = weaponModel.BulletModel;
            bullet.GetComponent<Bullet>().IsEnemyBullet = isEnemyBullet;
        }
    }
}
