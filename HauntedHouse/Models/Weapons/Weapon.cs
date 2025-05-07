namespace HauntedHouse;

public abstract class Weapon
{
    protected float cooldown; // Время отката
    protected float cooldownLeft; 
    protected int maxAmmo; // Максимально доступное число патрон
    public int Ammo {get; protected set;} // Число патрон
    protected float reloadTime; // Время перезарядки
    public bool Reloading {get; protected set;} // Состояние перезарядки в данный момент

    protected Weapon()
    {
        cooldownLeft = 0f;
        Reloading = false;
    }

    public virtual void Reload() // Метод перезарядки
    {
        if (Reloading || Ammo == maxAmmo) return;
        cooldownLeft = reloadTime;
        Reloading = true;
        Ammo = maxAmmo;
    }

    protected abstract void CreateProjectiles(Player player); // Метод создания снаряда

    public virtual void Fire(Player player)
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