namespace HauntedHouse;

public class Player : MovingSprite
{
    public Weapon Weapon { get; set; }
    private Weapon _weapon1;
    private Weapon _weapon2;
    public int Experience { get; private set; }
    public bool Dead { get; private set; } // Флаг смерти игрока
    public int Health { get; private set; } // Очки здоровья игрока
    private static SpriteFont _font;
    private static readonly Vector2 HealthPosition = new(20, 20); // Позиция отрисовки здоровья игрока на экране
    private static readonly Vector2 AmmoPosition;
    private static readonly Vector2 LevelPosition = new(20, 50); // Позиция отрисовки уровня на экране
    private static readonly Vector2 ExperiencePosition = new(20, 80); // Позиция отрисовки опыта на экране

    static Player()
    {
        _font = Globals.Content.Load<SpriteFont>("Score");
        AmmoPosition = new(Globals.Bounds.X - 100, Globals.Bounds.Y - 40); // Позиция отрисовки количества патронов на экране
    }

    public Player(Texture2D tex) : base(tex, GetStartPosition())
    {
        Reset();
    }

    private static Vector2 GetStartPosition()
    {
        return new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2);
    }

    public void GetExperience(int experience) // Обработка получения опыта игроком
    {
        Experience += experience;
        if (LevelSystem.TryLevelUp(Experience))
        {
            // Восстанавливает очки здоровья при повышении уровня
            Health = LevelSystem.GetMaxHealth();
        }
    }

    public void Reset() // Перезапуск
    {
        // При перезапуске возвращает все параметры к изначальным
        _weapon1 = new Rifle();
        _weapon2 = new Shotgun();
        Dead = false;
        Experience = 0;
        LevelSystem.Reset();
        Health = LevelSystem.GetMaxHealth();
        Weapon = _weapon1;
        Position = GetStartPosition();
    }

    public void SwapWeapon() // Смена оружия
    {
        Weapon = (Weapon == _weapon1) ? _weapon2 : _weapon1;
    }

    public void TakeDamage(int damage) // Получение урона игроком
    {
        Health -= damage;
        if (Health <= 0)
        {
            Dead = true;
        }
    }

    public void Update(List<Ghost> ghosts)
    {
        if (Dead) return;

        if (InputManager.Direction != Vector2.Zero)
        {
            var direction = Vector2.Normalize(InputManager.Direction);
            Position = new(
                MathHelper.Clamp(Position.X + (direction.X * Speed * Globals.TotalSeconds), 0, Globals.Bounds.X),
                MathHelper.Clamp(Position.Y + (direction.Y * Speed * Globals.TotalSeconds), 0, Globals.Bounds.Y)
            );
        }

        var toMouse = InputManager.MousePosition - Position;
        Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

        Weapon.Update();

        if (InputManager.Weapon1Pressed) // Смена оружия
        {
            Weapon = _weapon1;
        }
        else if (InputManager.Weapon2Pressed) // Смена оружия
        {
            Weapon = _weapon2;
        }

        if (InputManager.MouseLeftDown) // Обработка стрельбы при зажатии ЛКМ
        {
            Weapon.Fire(this);
        }

        if (InputManager.ReloadKeyPressed) // Обработка перезарядки при нажатии R
        {
            Weapon.Reload();
        }
    }

    public void Draw()
    {
        base.Draw();

        // Отрисовка здоровья
        string healthText = $"Здоровье: {Health}/{LevelSystem.GetMaxHealth()}";
        Globals.SpriteBatch.DrawString(_font, healthText, HealthPosition, Color.White);

        // Отрисовка уровня
        string levelText = $"Уровень: {LevelSystem.CurrentLevel}";
        Globals.SpriteBatch.DrawString(_font, levelText, LevelPosition, Color.White);

        // Отрисовка опыта
        string expText = $"Опыт: {Experience}/{LevelSystem.ExperienceToNextLevel}";
        Globals.SpriteBatch.DrawString(_font, expText, ExperiencePosition, Color.White);

        // Отрисовка патронов
        string ammoText = $"Патроны: {Weapon.Ammo}/{Weapon.MaxAmmo}";
        Vector2 ammoSize = _font.MeasureString(ammoText);
        Vector2 ammoDrawPosition = new(AmmoPosition.X - ammoSize.X, AmmoPosition.Y);
        Globals.SpriteBatch.DrawString(_font, ammoText, ammoDrawPosition, Color.White);
    }
}