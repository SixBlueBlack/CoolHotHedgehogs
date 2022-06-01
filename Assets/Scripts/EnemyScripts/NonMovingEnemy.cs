using Assets.Scripts;
using UnityEngine;

public class NonMovingEnemy : Enemy
{
    new void Start()
    {
        base.Start();
    }

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

    private bool CanAttack()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer > 0) return false;
        cooldownTimer = EnemyModel.WeaponModel.FireDelay;
        return true;
    }

    void Shoot()
    {
        if (!CanAttack()) return;
        EnemyModel.WeaponModel.Weapon.Shoot(transform.position, transform.rotation, true);
    }
}
