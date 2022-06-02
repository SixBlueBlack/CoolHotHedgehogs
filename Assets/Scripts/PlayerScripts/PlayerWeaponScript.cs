using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerWeaponScript : MonoBehaviour
    {
        public static List<WeaponModel> WeaponModels;

        public static int CurrentWeaponIndex;

        private float cooldownTimer;
        private bool isAttacking;

        public GameObject[] WeaponObjects;

        void Start()
        {
            WeaponModels = new List<WeaponModel>
            {
                new WeaponModel(new BulletModel(5, 20), 0.5f, 20f, Weapon.TypeName.Rifle, null),
                new WeaponModel(new BulletModel(3, 50), 1f, 20f, Weapon.TypeName.Rifle, null)
            };

            CurrentWeaponIndex = 0;

            AddWeaponScripts(WeaponObjects, WeaponModels);
        }

        public static void AddWeaponScripts(GameObject[] newWeaponObjects, List<WeaponModel> newWeaponModels, bool isPresent = true)
        {
            if (!isPresent)
                foreach (var weaponModel in newWeaponModels)
                    WeaponModels.Add(weaponModel);

            for (var i = 0; i < newWeaponModels.Count; i++)
            {
                var inst = Instantiate(newWeaponObjects[i]);
                inst.transform.SetParent(FindObjectOfType<PlayerWeaponScript>().transform);
                Weapon weapon = newWeaponModels[i].WeaponType switch
                {
                    Weapon.TypeName.Rifle => inst.GetComponent<RifleWeapon>(),
                    Weapon.TypeName.Shotgun => inst.GetComponent<ShotgunWeapon>(),
                    Weapon.TypeName.Circle => inst.GetComponent<CircleWeapon>(),
                    _ => null
                };
                weapon!.weaponModel = WeaponModels[i];
                WeaponModels[WeaponModels.Count - newWeaponModels.Count + i].Weapon = weapon;
            }
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
            if (index < WeaponModels.Count) CurrentWeaponIndex = index;
        }

        private void ChangeWeaponByScrollWheel()
        {
            var mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw < 0 && CurrentWeaponIndex < WeaponModels.Count - 1) CurrentWeaponIndex += 1;
            else if (mw < 0) CurrentWeaponIndex = 0;

            if (mw > 0 && CurrentWeaponIndex > 0) CurrentWeaponIndex -= 1;
            else if (mw > 0) CurrentWeaponIndex = WeaponModels.Count - 1;
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

        private bool CanAttack()
        {
            cooldownTimer -= Time.deltaTime;
            if (!isAttacking || cooldownTimer > 0) return false;
            cooldownTimer = WeaponModels[CurrentWeaponIndex].FireDelay;
            return true;
        }

        private void Attack()
        {
            if (!CanAttack()) return;

            WeaponModels[CurrentWeaponIndex].Weapon.Shoot(transform.position, transform.rotation);
        }
    }
}