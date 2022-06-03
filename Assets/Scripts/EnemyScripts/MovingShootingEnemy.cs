using UnityEngine;

public class MovingShootingEnemy : NonMovingEnemy
{
    private readonly float toPlayerDistThreshold = 5f;
    new void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        var distToPlayer = Vector2.Distance(transform.position, Player.position);
        if (distToPlayer <= toPlayerDistThreshold)
            Shoot();
        else
            Move(distToPlayer);
    }


    public override void Move(float distToPlayer)
    {
        if (distToPlayer > toPlayerDistThreshold) ShowAggression(Player.position, transform.position);
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