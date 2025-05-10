namespace HauntedHouse;

public static class ProjectilesManager
{
    private static Texture2D _texture;
    public static List<Projectiles> Projectiles {get;} = [];

    public static void Init(Texture2D texture) // Метод инициализации для статических классов
    {
        _texture = texture;
    }

    public static void Reset() // Метод удаления снарядов
    {
        Projectiles.Clear(); 
    }

    public static void AddProjectile(ProjectileData projectileData)
    {
        Projectiles.Add(new (_texture, projectileData));
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

                if ((p.Position - g.Position).Length() < 15) // Проверка столкновения призрака и снаряда
                {
                    g.GetDamage(p.Damage);
                    p.Destroy();
                    break;
                }
            }
        }
        Projectiles.RemoveAll((p) => p.Lifespan <= 0); // Стирает снаряды время жизни которых <= 0
    }

    public static void Draw()
    {
        foreach (var p in Projectiles)
        {
            p.Draw(); // Отрисовка снарядов
        }
    }
}