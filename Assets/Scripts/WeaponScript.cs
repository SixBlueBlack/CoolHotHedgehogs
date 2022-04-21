using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    public GameObject Bullet;
    public Coroutine FireFrequency;
    public static WeaponModel[] Weapons;
    public static int CurrentWeaponIndex;

    void Start()
    {
        Weapons = new[]
        {
            new WeaponModel(new BulletModel(5, 5), 0.3f, 20f),
            new WeaponModel(new BulletModel(3, 20), 0.5f, 20f)
        };
        CurrentWeaponIndex = 0;
    }

    void Update()
    {
        RotateFirePosition();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CurrentWeaponIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            CurrentWeaponIndex = 1;
        if (Input.GetButtonDown("Fire1"))
            FireFrequency = StartCoroutine(FireDelay());
        if (Input.GetButtonUp("Fire1"))
            StopCoroutine(FireFrequency);
    }

    private void RotateFirePosition()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);
    }

    private IEnumerator FireDelay()
    {
        while (true)
        {
            var bullet = Instantiate(Bullet, transform.position, transform.rotation);
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * Weapons[CurrentWeaponIndex].BulletForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(Weapons[CurrentWeaponIndex].FireDelay);
        }
    }

    //void Shoot()
    //{
    //var bullet = Instantiate(Bullet, transform.position, transform.rotation);
    //var rb = bullet.GetComponent<Rigidbody2D>();
    //rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    //}
}