namespace HauntedHouse;
public class Player : MovingSprite
{   
    public Weapon Weapon {get; set;}
    public bool Dead {get; private set;} // Флаг смерти игрока
    private Weapon _shotgun;
    public Player(Texture2D texture) : base (texture, GetStartPosition())
    {
        Reset();
    }

    private static Vector2 GetStartPosition()  // Начальное положение игрока на экране
    {
        return new (Globals.Bounds.X/2, Globals.Bounds.Y/2);
    }

    public void Reset()
    {
        Dead = false;
        _shotgun = new Shotgun();
        Position = GetStartPosition();
    }

    private void CheckState(List<Ghost> ghosts) // Метод проверки состояния игрока
    {
        foreach (var g in ghosts)
        {
            if (g.healthPoints <= 0)
            {
                continue;
            }

            if ((Position - g.Position).Length() < 40)
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
            var direction = Vector2.Normalize(InputManager.Direction); // Нормализация вектора, чтобы скорость при перемещении по диагонали не складывалась
            Position = new(
                MathHelper.Clamp(Position.X + (direction.X * Speed * Globals.TotalSeconds), 0, Globals.Bounds.X), // Ограничение по X
                MathHelper.Clamp(Position.Y + (direction.Y * Speed * Globals.TotalSeconds), 0, Globals.Bounds.Y)  // Ограничение по Y
            );
        }

        Weapon.Update();

        if (InputManager.MouseLeftDown)
        {
            Weapon.Fire(this);
        }

        if (InputManager.ReloadKeyPressed)
        {
            Weapon.Reload();
        }

        CheckState(ghosts);
    }   
}

