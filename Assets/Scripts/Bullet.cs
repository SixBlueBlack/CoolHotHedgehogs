using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletModel BulletModel;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        BulletModel = PlayerWeaponScript.Weapons[PlayerWeaponScript.CurrentWeaponIndex].BulletModel;
        rb.velocity = transform.right * BulletModel.Speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "HorizontalWall(Clone)" || hitInfo.name == "VerticalWall(Clone)")
            Destroy(gameObject);
        if (hitInfo.name == "Enemy")
        {
            var enemy = hitInfo.GetComponent<EnemyScript>();
            enemy.TakeDamage(BulletModel.Damage);
            Destroy(gameObject);
        }
    }
}