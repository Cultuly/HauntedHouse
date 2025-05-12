namespace HauntedHouse;

public class Rifle : Weapon
{
    public Rifle()
    {
        cooldown = 0.1f;
        maxAmmo = 30;
        Ammo = maxAmmo;
        reloadTime = 2f;
    }

    protected override void CreateProjectiles(Player player) // Создание снарядов для винтовки
    {
        ProjectileData projectileData = new()
        {
            Position = player.Position,
            Rotation = player.Rotation, // Направление выстрела совпадает с направлением игрока
            Lifespan = 2f,
            Speed = 600,
            Damage = 1
        };

        ProjectileManager.AddProjectile(projectileData);
    }
}