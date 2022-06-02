using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class Boss : StalkerEnemy
    {
        public Action<IEnumerable<EnemyModel>, Vector3> EnemySpawner;

        new void Start()
        {
            toPlayerDistThreshold = 1.5f;
            base.Start();
        }

        public override void Die()
        {
            EnemySpawner(EnemyModel.AttachedToRoom.GetEnemiesOfType(EnemyModel.EnemyType.SmallBoss), transform.position);
            base.Die();
        }

        public override void Update()
        {
            base.Update();
            Shoot();
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

            var playerPosition = Player.position;
            var angle = Vector2.Angle(Vector2.right, playerPosition - transform.position);

            EnemyModel.WeaponModel.Weapon.Shoot(transform.position,
                Quaternion.Euler(new Vector3(0f, 0f, transform.position.y < playerPosition.y ? angle : -angle)), true);
        }
    }
}
