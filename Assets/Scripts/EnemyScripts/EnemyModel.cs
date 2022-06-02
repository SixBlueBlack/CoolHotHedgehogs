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
        public Room AttachedToRoom { get; }
        public float DistanceForAgr = 10;

        public enum EnemyType { 
            Warrior,
            Tower,
            Boss,
            SmallBoss,
            Shotgun
        }
        public EnemyType Type { get; }

        public int Row { get; }
        public int Column { get; }

        public EnemyModel(int row, int col, EnemyType type, Room inRoom)
        {
            Row = row;
            Column = col;
            Type = type;
            AttachedToRoom = inRoom;

            if (type == EnemyType.Tower)
            {
                Health = 100;
                WeaponModel = new WeaponModel(new BulletModel(10, 20), 1f, 20f, Weapon.TypeName.Rifle);
                Damage = 0;
                Speed = 0;
            }
            if (type == EnemyType.Warrior)
            {
                Health = 100;
                WeaponModel = null;
                Damage = 20;
                Speed = 1.5f;
            }
            if (type == EnemyType.Boss)
            {
                Health = 300;
                WeaponModel = new WeaponModel(new BulletModel(10, 10), 1f, 20f, Weapon.TypeName.Circle);
                Damage = 20;
                Speed = 0.7f;
            }
            if (type == EnemyType.SmallBoss)
            {
                Health = 100;
                WeaponModel = new WeaponModel(new BulletModel(20, 20), 1f, 20f, Weapon.TypeName.Rifle);
                Damage = 20;
                Speed = 1f;
            }
            if (type == EnemyType.Shotgun)
            {
                Health = 100;
                WeaponModel = new WeaponModel(new BulletModel(20, 20), 1f, 20f, Weapon.TypeName.Shotgun);
                Damage = 20;
                Speed = 1f;
            }
        }
    }
}
