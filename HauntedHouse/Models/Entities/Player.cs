namespace HauntedHouse;

public class Player : MovingSprite
{
    public Weapon Weapon { get; set; }
    private Weapon _weapon1;
    private Weapon _weapon2;
    public bool Dead { get; private set; } // Флаг смерти игрока

    public Player(Texture2D tex) : base(tex, GetStartPosition())
    {
        Reset();
    }

    private static Vector2 GetStartPosition() // Установка начального положения на экране (посередине)
    {
        return new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2);
    }

    public void Reset() // Перезапуск
    {
        _weapon1 = new Rifle();
        _weapon2 = new Shotgun();
        Dead = false;
        Weapon = _weapon1;
        Position = GetStartPosition();
    }

    public void SwapWeapon() // Метод смены оружия
    {
        Weapon = (Weapon == _weapon1) ? _weapon2 : _weapon1;
    }

    private void CheckDeath(List<Ghost> ghosts) // Проверка состояния игрока
    {
        foreach (var g in ghosts)
        {
            if (g.healthPoints <= 0)
            {
                continue;
            }

            if ((Position - g.Position).Length() < 30)
            {
                Dead = true;
                break;
            }
        }
    }

    public void Update(List<Ghost> ghosts)
    {
        if (InputManager.Direction != Vector2.Zero)
        {
            var direction = Vector2.Normalize(InputManager.Direction); // Нормализация вектора перемещения
            Position = new(
                MathHelper.Clamp(Position.X + (direction.X * Speed * Globals.TotalSeconds), 0, Globals.Bounds.X), // Ограничение по X
                MathHelper.Clamp(Position.Y + (direction.Y * Speed * Globals.TotalSeconds), 0, Globals.Bounds.Y) // Ограничение по Y
            );
        }

        var toMouse = InputManager.MousePosition - Position;
        Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X); // Поворот в сторону мышки

        Weapon.Update();

        if (InputManager.SpacePressed) // Смена оружия на пробел
        {
            SwapWeapon();
        }

        if (InputManager.MouseLeftDown) // Стрельба на ЛКМ
        {
            Weapon.Fire(this);
        }

        if (InputManager.ReloadKeyPressed) // Перезарядка на R
        {
            Weapon.Reload();
        }

        CheckDeath(ghosts);
    }
}