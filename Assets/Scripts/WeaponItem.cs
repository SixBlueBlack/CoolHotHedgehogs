using UnityEngine;

namespace Assets.Scripts
{
    public class WeaponItem : MonoBehaviour
    {
        private WeaponModel weaponModel;

        public Sprite BulletSprite;

        public Sprite WeaponsSprite;

        private bool isCollided;

        private OutlineScript outlineScript;

        // Start is called before the first frame update
        void Start()
        {
            weaponModel = new WeaponModel(new BulletModel(7, 35, new Vector2(0.05f, 0.05f), BulletSprite), 1f, 20f, WeaponsSprite);
            outlineScript = transform.GetComponent<OutlineScript>();
        }

        void Update()
        {
            if (!isCollided || !Input.GetKeyDown(KeyCode.E)) return;
            PlayerWeaponScript.Weapons.Add(weaponModel);
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
