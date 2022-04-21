public class WeaponModel
{
    public BulletModel BulletModel;

    public float BulletForce { get; }

    public float FireDelay { get; }

    public WeaponModel(BulletModel bulletModel, float fireDelay, float bulletForce)
    {
        BulletModel = bulletModel;
        FireDelay = fireDelay;
        BulletForce = bulletForce;
    }
}