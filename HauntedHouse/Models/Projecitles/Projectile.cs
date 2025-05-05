namespace HauntedHouse;

public class Projectile : MovingSprite
{
    public Vector2 Direction {get; set;}
    public float Lifespan {get; private set;}
    public int Damage {get;}

    public Projectile(Texture2D texture, ProjectileData data) : base(texture, data.Position)
    {
        Speed = data.Speed;
        Lifespan = data.Lifespan;
        Damage = data.Damage;
        Rotation = data.Rotation;
        Direction = new((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
    }

    public void Destroy() // Уничтожение снарядов
    {
        Lifespan = 0;
    }

    public void Update()
    {
        Position = Direction * Speed * Globals.TotalSeconds;
        Lifespan -= Globals.TotalSeconds; 
    }
}
