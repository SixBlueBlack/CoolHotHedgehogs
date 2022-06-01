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

        public int Row { get; }
        public int Column { get; }

        public EnemyModel(int row, int col, WeaponModel weaponModel, int hp, int damage, float speed) // Pass type of an enemy instead of params
        {
            Health = hp;
            WeaponModel = weaponModel;
            Damage = damage;
            Speed = speed;
            Row = row;
            Column = col;
        }
    }
}
