namespace HauntedHouse;

public sealed class ProjectileData
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; } 
    public float Lifespan { get; set; } // Время жизни снаряда
    public int Speed { get; set; }
    public int Damage { get; set; }
    public bool IsEnemyProjectile { get; set; } // Флаг определения снаряда (игрока или врага)
}