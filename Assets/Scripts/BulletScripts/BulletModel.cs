using UnityEngine;


namespace Assets.Scripts
{
    public class BulletModel
    {
        public float Speed { get; }

        public int Damage { get; }

        public Vector2 Size { get; }

        public Sprite Sprite { get; }

        public BulletModel(float speed, int damage, Vector2 size, Sprite sprite)
        {
            Speed = speed;
            Damage = damage;
            Size = size;
            Sprite = sprite;
        }
    }
}