namespace HauntedHouse;

public abstract class Weapon
{
    protected float cooldown; // Время между выстрелами
    protected float cooldownLeft;
    protected int maxAmmo; // Максимально число патрон
    public int Ammo { get; protected set; } // Текущее число патрон
    protected float reloadTime; // Время перезарядки
    public bool Reloading { get; protected set; } // Флаг перезарядки

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

    public virtual void Fire(Player player) // Стрельба
    {
        if (cooldownLeft > 0 || Reloading) return;

        Ammo--;
        if (Ammo > 0)
        {
            cooldownLeft = cooldown; // Сброс таймера
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