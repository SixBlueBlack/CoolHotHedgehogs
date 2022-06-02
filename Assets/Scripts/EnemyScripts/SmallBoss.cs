namespace Assets.Scripts
{
    class SmallBoss : Boss
    {
        public override void Die()
        {
            EnemySpawner(EnemyModel.AttachedToRoom.GetEnemiesOfType(EnemyModel.EnemyType.Warrior, 2), transform.position);
            Destroy(gameObject);
        }
    }
}
