using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class WeaponPanelScript : MonoBehaviour
    {
        public Image IconImage;

        public int LastWeaponIndex;

        // Start is called before the first frame update
        void Start()
        {
            LastWeaponIndex = 0;
        }

        // Update is called once per frame
        void Update()
        {
            SetWeapon();
        }

        private void SetWeapon()
        {
            if (LastWeaponIndex != PlayerWeaponScript.CurrentWeaponIndex)
                IconImage.sprite = PlayerWeaponScript.WeaponModels[PlayerWeaponScript.CurrentWeaponIndex].Weapon.WeaponSprite;
            LastWeaponIndex = PlayerWeaponScript.CurrentWeaponIndex;
        }
    }
}