using UnityEngine;


namespace Assets.Scripts
{
    public class BulletModel
    {
        public float Speed { get; }

        public float Damage { get; }

        public Vector2 Size { get; }

        public Sprite Sprite { get; }

        public BulletModel(float speed, float damage, Vector2 size, Sprite sprite)
        {
            Speed = speed;
            Damage = damage;
            Size = size;
            Sprite = sprite;
        }
    }
}