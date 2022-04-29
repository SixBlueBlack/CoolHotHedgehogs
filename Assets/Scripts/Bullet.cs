using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Wall(Clone)")
            Destroy(gameObject);
        if (hitInfo.name == "Enemy")
        {
            var enemy = hitInfo.GetComponent<EnemyScript>();
            enemy.TakeDamage(BulletModel.Damage);
            Destroy(gameObject);
        }
    }
}