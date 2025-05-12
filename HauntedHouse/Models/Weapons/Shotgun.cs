namespace HauntedHouse;

public class Shotgun : Weapon
{
    private const float AngleOfRotation = (float)(Math.PI / 24); // Угол между патронами дробовика при выстреле

    public Shotgun()
    {
        cooldown = 0.75f;
        maxAmmo = 8;
        Ammo = maxAmmo;
        reloadTime = 3f;
    }

    protected override void CreateProjectiles(Player player) // Создание снаряда для дробовика
    {
        ProjectileData projectileData = new()
        {
            Position = player.Position,
            Rotation = player.Rotation - (3 * AngleOfRotation), // Максимальный угол (22.5 градуса)
            Lifespan = 0.5f,
            Speed = 800,
            Damage = 2
        };

        for (int i = 0; i < 5; i++)
        {
            projectileData.Rotation += AngleOfRotation; // Паттерн стрельбы из дробовика (веер 22.5 градуса)
            ProjectileManager.AddProjectile(projectileData);
        }
    }
}