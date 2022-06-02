namespace Assets.Scripts
{
    public class EnemyModel
    {
        public WeaponModel WeaponModel;

        public int Health { get; set; }
        public int Damage { get; }
        public float Speed { get; }
        public bool IsDead => Health <= 0;
        public bool IsSpawned { get; set; } = false;
        public float DistanceForAgr = 10;

        public enum EnemyType { 
            Warrior,
            Tower,
            Boss
        }
        public EnemyType Type { get; }

        public int Row { get; }
        public int Column { get; }

        public EnemyModel(int row, int col, EnemyType type)
        {

            Row = row;
            Column = col;
            Type = type;

            if (type == EnemyType.Tower)
            {
                Health = 100;
                WeaponModel = new WeaponModel(new BulletModel(10, 20), 1f, 20f, Weapon.TypeName.Rifle, null);
                Damage = 20;
                Speed = 1.5f;
            }
            if (type == EnemyType.Warrior)
            {
                Health = 100;
                WeaponModel = null;
                Damage = 20;
                Speed = 1.5f;
            }
        }
    }
}
