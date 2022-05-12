using Assets.Scripts;
using UnityEngine;

public class StalkerEnemy : Enemy
{

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    void Update()
    {
        var distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer <= 0.7f)
            Attack();
        Move(distToPlayer);
    }

    private void Attack()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer > 0) return;
        cooldownTimer = cooldown;
        player.GetComponent<Player>().TakeDamage(EnemyModel.Damage);
    }

    private void Move(float distToPlayer)
    {
        if (distToPlayer < EnemyModel.DistanceForAgr) ShowAggression(player.position, transform.position);
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

    public void LoseInterest()
    {
        Physic.velocity = new Vector2(0, 0);
    }
}
