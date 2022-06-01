using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour
    {
        public BulletModel BulletModel;
        public Rigidbody2D rb;
        public bool IsEnemyBullet;

        void Start()
        {
            rb.velocity = transform.right * BulletModel.Speed;
        }

        private void OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (hitInfo.tag == "Wall")
                Destroy(gameObject);
            switch (IsEnemyBullet)
            {
                case false when hitInfo.tag == "Enemy":
                {
                    var enemy = hitInfo.GetComponent<Enemy>();
                    enemy.TakeDamage(BulletModel.Damage);
                    Destroy(gameObject);
                    break;
                }
                case true when hitInfo.tag == "Player":
                {
                    var player = hitInfo.GetComponent<Player>();
                    player.TakeDamage(BulletModel.Damage);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}