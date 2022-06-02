using Assets.Scripts;
using UnityEngine;

public class StalkerEnemy : Enemy
{
    internal float toPlayerDistThreshold = 0.7f;

    new void Start()
    {
        base.Start();
    }

    public virtual void Update()
    {
        var distToPlayer = Vector2.Distance(transform.position, Player.position);
        if (distToPlayer <= toPlayerDistThreshold)
            Attack();
        Move(distToPlayer);
    }

    private void Attack()
    {
        CooldownTimer -= Time.deltaTime;
        if (CooldownTimer > 0) return;
        CooldownTimer = Cooldown;
        Player.GetComponent<Player>().TakeDamage(EnemyModel.Damage);
    }

    private void Move(float distToPlayer)
    {
        if (distToPlayer < EnemyModel.DistanceForAgr) ShowAggression(Player.position, transform.position);
        else LoseInterest();
    }

    private void ShowAggression(Vector3 playerPos, Vector3 enemyPos)
    {
        float xVelocity = 0;
        float yVelocity = 0;

        if (playerPos.x < enemyPos.x) xVelocity = -EnemyModel.Speed;
        else if (playerPos.x > enemyPos.x) xVelocity = EnemyModel.Speed;

        if (playerPos.y < enemyPos.y) yVelocity = -EnemyModel.Speed;
        else if (playerPos.y > enemyPos.y) yVelocity = EnemyModel.Speed;

        Physic.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void LoseInterest()
    {
        Physic.velocity = new Vector2(0, 0);
    }
}
