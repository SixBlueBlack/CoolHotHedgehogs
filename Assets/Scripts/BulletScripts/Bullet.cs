using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour
    {
        public BulletModel BulletModel;
        public SpriteRenderer SpriteRenderer;
        public Rigidbody2D rb;

        void Start()
        {
            BulletModel = PlayerWeaponScript.Weapons[PlayerWeaponScript.CurrentWeaponIndex].BulletModel;
            rb.velocity = transform.right * BulletModel.Speed;
            transform.localScale = BulletModel.Size;

            SpriteRenderer.sprite = BulletModel.Sprite;
        }

        private void OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (hitInfo.name == "UpperWall(Clone)" || hitInfo.name == "VerticalWall(Clone)" || hitInfo.name == "BottomWall(Clone)")
                Destroy(gameObject);
            if (hitInfo.name == "Enemy(Clone)")
            {
                var enemy = hitInfo.GetComponent<Enemy>();
                enemy.TakeDamage(BulletModel.Damage);
                Destroy(gameObject);
            }
        }
    }
}