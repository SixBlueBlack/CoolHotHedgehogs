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
            if (IsEnemyBullet)
            {
                GetComponentInChildren<OutlineScript>().IsOutlined = true;
            }
            
        }

        private void OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (hitInfo.CompareTag("Wall"))
                Destroy(gameObject);

            if (!IsEnemyBullet && hitInfo.CompareTag("Enemy"))
            {
                var enemy = hitInfo.GetComponent<Enemy>();
                enemy.TakeDamage(BulletModel.Damage);
                Destroy(gameObject);
            }

            if (IsEnemyBullet && hitInfo.CompareTag("Player") && hitInfo == hitInfo.GetComponents<Collider2D>()[0])
            {
                var player = hitInfo.GetComponent<Player>();
                player.TakeDamage(BulletModel.Damage);
                Destroy(gameObject);
            }
        }
    }
}