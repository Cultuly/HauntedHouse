namespace HauntedHouse;

public class Shotgun : Weapon
{
    private const float AngleOfRotation = (float)(Math.PI/24); // Угол между патронами дробовика
    public Shotgun()
    {
        cooldown = 0.75f;
        reloadTime = 4f;
        maxAmmo = 4;
        Ammo = maxAmmo;
    }

    protected override void CreateProjectiles(Player player)
    {
        ProjectileData projectData = new()
        {
            Position = player.Position,
            Rotation = player.Rotation - (3 * AngleOfRotation),
            Speed = 600,
            Lifespan = 1f,
            Damage = 4
        };

        for (var i = 0; i < 5; i++)
        {
            projectData.Rotation += AngleOfRotation; // Паттерн выстрела (веер 22.5 градусов)
            ProjectilesManager.AddProjectile(projectData);
        }
    }
}