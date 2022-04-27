using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletModel BulletModel;
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        BulletModel = PlayerWeaponScript.Weapons[PlayerWeaponScript.CurrentWeaponIndex].BulletModel;
        rb.velocity = transform.right * BulletModel.Speed;
        transform.localScale = BulletModel.Size;
        //SpriteRenderer.sprite = Resources.Load<Sprite>("Assets/sprites/Bullets/8x8_red_border_bullet.png");
        //SpriteRenderer.sortingLayerName = "Bullet";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
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