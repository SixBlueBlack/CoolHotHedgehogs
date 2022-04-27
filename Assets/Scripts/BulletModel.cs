using UnityEngine;

public class BulletModel
{
    public float Speed { get; }

    public float Damage { get; }

    public Vector2 Size { get; }

    public BulletModel(float speed, float damage, Vector2 size)
    {
        Speed = speed;
        Damage = damage;
        Size = size;
    }
}