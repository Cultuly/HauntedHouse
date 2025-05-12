namespace HauntedHouse;

public static class ProjectileManager
{
    private static Texture2D _texture;
    public static List<Projectile> Projectiles { get; } = [];

    public static void Init(Texture2D texture) // Инициализация для статических классов
    {
        _texture = texture;
    }

    public static void Reset() 
    {
        Projectiles.Clear();
    }

    public static void AddProjectile(ProjectileData data) // Создание снаряда
    {
        Projectiles.Add(new(_texture, data));
    }

    public static void Update(List<Ghost> ghosts)
    {
        foreach (var p in Projectiles)
        {
            p.Update();
            foreach (var g in ghosts)
            {
                if (g.healthPoints <= 0)
                {
                    continue;
                }

                if ((p.Position - g.Position).Length() < 20)
                {
                    g.TakeDamage(p.Damage);
                    p.Destroy();
                    break;
                }
            }
        }
        Projectiles.RemoveAll((p) => p.Lifespan <= 0); // Уничтожение всех попавших снарядов
    }

    public static void Draw()
    {
        foreach (var p in Projectiles)
        {
            p.Draw(); // Отрисовка снарядов
        }
    }
}