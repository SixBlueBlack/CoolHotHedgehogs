using Assets.Scripts;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    internal WeaponModel weaponModel;

    public GameObject Bullet;
    public Sprite WeaponSprite;

    public enum TypeName
    {
        Shotgun,
        Rifle,
        Circle
    }

    public virtual void Shoot(Vector3 position, Quaternion rotation, bool isEnemyBullet = false) { }
}
