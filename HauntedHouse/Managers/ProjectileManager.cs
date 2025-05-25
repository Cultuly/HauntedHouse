namespace HauntedHouse;

public static class ProjectileManager
{
    private static Texture2D _playerProjectileTexture;
    private static Texture2D _ghostProjectileTexture;
    public static List<Projectile> Projectiles { get; } = [];
    private const float CollisionRadius = 20f; // Радиус столкновения для снарядов

    public static void Init(Texture2D playerProjectileTexture, Texture2D ghostProjectileTexture) // Инициализация (для статических классов)
    {
        _playerProjectileTexture = playerProjectileTexture;
        _ghostProjectileTexture = ghostProjectileTexture;
    }

    public static void Reset() // Перезапуск
    {
        Projectiles.Clear(); // Убирает все снаряды
    }

    public static void AddProjectile(ProjectileData data) // Создаёт снаряды
    {
        var texture = data.IsEnemyProjectile ? _ghostProjectileTexture : _playerProjectileTexture;
        Projectiles.Add(new(texture, data));
    }

    public static void Update(Player player, List<Ghost> ghosts)
    {
        for (int i = Projectiles.Count - 1; i >= 0; i--)
        {
            var projectile = Projectiles[i];
            projectile.Update();

            // Обработка получения урона (игроком от противников)
            if (projectile.IsEnemyProjectile)
            {
                // Обработка расстояния от снаряда до игрока
                if ((projectile.Position - player.Position).Length() < CollisionRadius)
                {
                    player.TakeDamage(projectile.Damage); 
                    projectile.Destroy();
                }
            }
            else
            {
                // Обработка получения урона противниками
                foreach (var ghost in ghosts)
                {
                    if (ghost.healthPoints <= 0) continue;

                    if ((projectile.Position - ghost.Position).Length() < CollisionRadius)
                    {
                        ghost.TakeDamage(projectile.Damage);
                        projectile.Destroy();
                        break;
                    }
                }
            }

            // Убирает попавшие или промахнувшиеся снаряды 
            if (projectile.Lifespan <= 0)
            {
                Projectiles.RemoveAt(i);
            }
        }
    }

    public static void Draw()
    {
        foreach (var projectile in Projectiles)
        {
            projectile.Draw(); // Отрисовка снарядов
        }
    }
}