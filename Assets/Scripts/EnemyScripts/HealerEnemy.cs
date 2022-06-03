using System;
using Assets.Scripts;
using UnityEngine;

public class HealerEnemy : Enemy
{
    new void Start()
    {
        base.Start();
    }

    void Update()
    {
        Heal();
    }

    private bool CanHeal()
    {
        CooldownTimer -= Time.deltaTime;
        if (CooldownTimer > 0) return false;
        CooldownTimer = Cooldown;
        return true;
    }

    public void Heal()
    {
        if (!CanHeal()) return;
        var enemies = EnemyModel.AttachedToRoom.Enemies;
        var minDistance = float.MaxValue;
        var index = 0;
        for (var i = 0; i < enemies.Length; i++)
        {
            try
            {
                var distToEnemy = Vector2.Distance(transform.position, enemies[i].Enemy.transform.position);
                if (Math.Abs(enemies[i].Health - enemies[i].Enemy.HealthBar.Slider.maxValue) < 0.1) continue;
                if (!(distToEnemy < minDistance)) continue;
                minDistance = distToEnemy;
                index = i;
            }
            catch (Exception)
            {
                // ignored
            }
        }
        enemies[index].Enemy.Heal(10);
    }
}