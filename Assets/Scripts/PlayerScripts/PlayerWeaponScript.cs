using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerWeaponScript : MonoBehaviour
    {
        public GameObject Bullet;

        public static List<WeaponModel> Weapons;

        public static int CurrentWeaponIndex;

        public Sprite[] BulletSprites;

        public Sprite[] WeaponsSprites;

        public static Sprite CommonSprite;

        private float cooldownTimer;
        private bool isAttacking;

        void Start()
        {
            CommonSprite = BulletSprites[0];
            Weapons = new List<WeaponModel>
        {
            new WeaponModel(new BulletModel(5, 20, new Vector2(1.4f, 1.4f), BulletSprites[0]), 0.5f, 20f, WeaponsSprites[0]),
            new WeaponModel(new BulletModel(3, 50, new Vector2(1.7f, 1.7f), BulletSprites[1]), 1f, 20f, WeaponsSprites[1])
        };
            CurrentWeaponIndex = 0;
        }

        void Update()
        {
            RotateFirePosition();
            ChangeWeapon();
            SetIsAttackingBool();
            Attack();
        }

        private void ChangeWeapon()
        {
            ChangeWeaponByButton();
            ChangeWeaponByScrollWheel();
        }

        private void ChangeWeaponByButton()
        {
            var index = CurrentWeaponIndex;
            for (var i = 0; i < 9; i++)
                if (Input.GetKeyDown((KeyCode)(i + 49))) index = i;
            if (index < Weapons.Count) CurrentWeaponIndex = index;
        }

        private void ChangeWeaponByScrollWheel()
        {
            var mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw < 0 && CurrentWeaponIndex < Weapons.Count - 1) CurrentWeaponIndex += 1;
            else if (mw < 0) CurrentWeaponIndex = 0;

            if (mw > 0 && CurrentWeaponIndex > 0) CurrentWeaponIndex -= 1;
            else if (mw > 0) CurrentWeaponIndex = Weapons.Count - 1;
        }

        private void SetIsAttackingBool()
        {
            if (Input.GetButtonDown("Fire1"))
                isAttacking = true;
            if (Input.GetButtonUp("Fire1"))
                isAttacking = false;
        }

        private void RotateFirePosition()
        {
            var mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
            transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);
        }

        private void Attack()
        {
            cooldownTimer -= Time.deltaTime;
            if (!isAttacking) return;
            if (cooldownTimer > 0) return;
            cooldownTimer = Weapons[CurrentWeaponIndex].FireDelay;
            var bullet = Instantiate(Bullet, transform.position, transform.rotation);
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * Weapons[CurrentWeaponIndex].BulletForce, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().BulletModel = Weapons[CurrentWeaponIndex].BulletModel;
        }
    }
}