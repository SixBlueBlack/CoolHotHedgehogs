using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerWeaponScript : MonoBehaviour
    {
        public GameObject Bullet;

        public Coroutine FireFrequency;

        public static List<WeaponModel> Weapons;

        public static int CurrentWeaponIndex;

        public Sprite[] BulletSprites;

        public Sprite[] WeaponsSprites;

        void Start()
        {
            Weapons = new List<WeaponModel>
        {
            new WeaponModel(new BulletModel(5, 20, new Vector2(1.4f, 1.4f), BulletSprites[0]), 0.3f, 20f, WeaponsSprites[0]),
            new WeaponModel(new BulletModel(3, 50, new Vector2(1.7f, 1.7f), BulletSprites[1]), 0.5f, 20f, WeaponsSprites[1])
        };
            CurrentWeaponIndex = 0;
        }

        void Update()
        {
            RotateFirePosition();
            ChangeWeapon();
            ManageCoroutines();
        }

        private void ChangeWeapon()
        {
            ChangeWeaponByButton();
            ChangeWeaponByScrollWheel();
        }

        private void ChangeWeaponByButton()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                CurrentWeaponIndex = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2))
                CurrentWeaponIndex = 1;
        }

        private void ChangeWeaponByScrollWheel()
        {
            var mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw > 0 && CurrentWeaponIndex < Weapons.Count - 1) CurrentWeaponIndex += 1;
            else if (mw > 0) CurrentWeaponIndex = 0;

            if (mw < 0 && CurrentWeaponIndex > 0) CurrentWeaponIndex -= 1;
            else if (mw < 0) CurrentWeaponIndex = Weapons.Count - 1;
        }

        private void ManageCoroutines()
        {
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
    }
}