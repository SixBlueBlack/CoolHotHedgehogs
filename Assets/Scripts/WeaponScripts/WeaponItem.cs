using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class WeaponItem : MonoBehaviour
    {
        private WeaponModel weaponModel;

        private bool isCollided;

        private OutlineScript outlineScript;

        // Start is called before the first frame update
        void Start()
        {
            weaponModel = new WeaponModel(new BulletModel(7, 35), 1f, 20f, Weapon.TypeName.Rifle, null);
            outlineScript = transform.GetComponent<OutlineScript>();
        }

        void Update()
        {
            AddWeapon();
        }

        private void AddWeapon()
        {
            if (!isCollided || !Input.GetKeyDown(KeyCode.E)) return;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            PlayerWeaponScript.AddWeaponScripts(new[] {gameObject}, new List<WeaponModel> {weaponModel}, false);
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (!hitInfo.CompareTag("Player")) return;
            isCollided = true;
            outlineScript.IsOutlined = true;
        }

        void OnTriggerExit2D(Collider2D hitInfo)
        {
            isCollided = false;
            outlineScript.IsOutlined = false;
        }
    }
}
