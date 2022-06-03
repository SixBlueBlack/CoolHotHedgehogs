using Assets.Scripts;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    internal WeaponModel weaponModel;

    public AudioSource ShootingSound { get; set; }

    public GameObject Bullet;
    public Sprite WeaponSprite;

    public enum TypeName
    {
        Shotgun,
        Rifle,
        Circle
    }

    private void Start()
    {
        ShootingSound = GetComponent<AudioSource>();
    }

    public virtual void Shoot(Vector3 position, Quaternion rotation, bool isEnemyBullet = false) { }
}
