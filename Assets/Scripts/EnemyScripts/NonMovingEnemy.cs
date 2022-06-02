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
        var playerPosition = Player.position;
        var angle = Vector2.Angle(Vector2.right, playerPosition - transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < playerPosition.y ? angle : -angle);
    }

    private bool CanAttack()
    {
        CooldownTimer -= Time.deltaTime;
        if (CooldownTimer > 0) return false;
        CooldownTimer = EnemyModel.WeaponModel.FireDelay;
        return true;
    }

    public override void Shoot()
    {
        if (!CanAttack()) return;
        EnemyModel.WeaponModel.Weapon.Shoot(transform.position, transform.rotation, true);
    }
}
