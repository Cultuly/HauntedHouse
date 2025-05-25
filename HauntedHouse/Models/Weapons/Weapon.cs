namespace HauntedHouse;

public abstract class Weapon
{
    protected float cooldown; // Время между выстрелами
    protected float cooldownLeft;
    protected int maxAmmo; // Максимальное число патрон
    public int MaxAmmo => maxAmmo;
    public int Ammo { get; protected set; } // Текущее число патрон
    protected float reloadTime; // Время перезарядки
    public bool Reloading { get; protected set; } // Флаг перезарядки
    protected int baseDamage;

    protected Weapon()
    {
        cooldownLeft = 0f;
        Reloading = false;
    }

    public virtual void Reload() // Перезарядка
    {
        if (Reloading || (Ammo == maxAmmo)) return;
        cooldownLeft = reloadTime;
        Reloading = true;
        Ammo = maxAmmo;
    }

    protected abstract void CreateProjectiles(Player player); // Создание перезарядки

    protected int GetDamageWithLevelMultiplier() // Множитель урона для оружия от уровня
    {
        return (int)(baseDamage * LevelSystem.GetDamageMultiplier());
    }

    public virtual void Fire(Player player) // Обработка стрельбы
    {
        if (cooldownLeft > 0 || Reloading) return;

        Ammo--;
        if (Ammo > 0)
        {
            cooldownLeft = cooldown;
        }
        else
        {
            Reload();
        }

        CreateProjectiles(player);
    }

    public virtual void Update()
    {
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Globals.TotalSeconds;
        }
        else if (Reloading)
        {
            Reloading = false;
        }
    }
}