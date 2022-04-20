using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletForce = 20f;
    public Coroutine FireFrequency;
    public float fireDelay = 0.3f;

    void Update()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);

        if (Input.GetButtonDown("Fire1"))
            FireFrequency = StartCoroutine(FireDelay());
        if (Input.GetButtonUp("Fire1"))
            StopCoroutine(FireFrequency);

    }

    private IEnumerator FireDelay()
    {
        while (true)
        {
            var bullet = Instantiate(Bullet, transform.position, transform.rotation);
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * BulletForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(fireDelay);
        }
    }

    //void Shoot()
    //{
    //var bullet = Instantiate(Bullet, transform.position, transform.rotation);
    //var rb = bullet.GetComponent<Rigidbody2D>();
    //rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    //}
}

