using UnityEngine;

namespace Assets.Scripts
{
    class Boss : StalkerEnemy
    {
        new void Start()
        {
            toPlayerDistThreshold = 1.5f;
            base.Start();
        }

        public override void Die()
        {
            base.Die();
        }

        public override void Update()
        {
            base.Update();
            Shoot();
        }

        private bool CanAttack()
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer > 0) return false;
            cooldownTimer = EnemyModel.WeaponModel.FireDelay;
            return true;
        }

        public override void Shoot()
        {
            if (!CanAttack()) return;

            var playerPosition = player.position;
            var angle = Vector2.Angle(Vector2.right, playerPosition - transform.position);

            EnemyModel.WeaponModel.Weapon.Shoot(transform.position,
                Quaternion.Euler(new Vector3(0f, 0f, transform.position.y < playerPosition.y ? angle : -angle)), true);
        }
    }
}
