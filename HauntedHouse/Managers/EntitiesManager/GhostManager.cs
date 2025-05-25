namespace HauntedHouse;

public static class GhostManager
{
    public static List<Ghost> Ghosts { get; } = [];
    private static Texture2D _texture;
    private static float _spawnCooldown; // Откат таймера спавна
    private static float _spawnTime; // Таймер спавна призраков
    private static Random _random;
    private static int _padding;

    public static void Init() // Инициализация (для статических классов)
    {
        _texture = Globals.Content.Load<Texture2D>("Ghost");
        _spawnCooldown = 0.5f; // Спавн по 2 призрака в секунду (по умолчанию)
        UpdateSpawnRate(); // Спавнрейт призраков (привязанный к сложности)
        _spawnTime = _spawnCooldown;
        _random = new();
        _padding = _texture.Width / 2;
    }

    public static void Reset() // Перезапуск
    {
        Ghosts.Clear();
        UpdateSpawnRate();
        _spawnTime = _spawnCooldown;
    }
    private static void UpdateSpawnRate() // Изменение спавнрейта (в зависимости от выбраной сложности)
    {
        _spawnCooldown = 0.5f * DifficultySettings.GetGhostSpawnRateMultiplier();
    }

    private static Vector2 SetRandomPosition() // Спавн призраков в разных местах (за игровым окном)
    {
        float width = Globals.Bounds.X; // Ширина экрана
        float height = Globals.Bounds.Y; // Высота экрана
        Vector2 position = new();

        if (_random.NextDouble() <  width / (width + height))
        {
            position.X = (int)(_random.NextDouble() * width);
            position.Y = (int)(_random.NextDouble() < 0.5 ? -_padding : height + _padding);
        }

        else
        {
            position.Y = (int)(_random.NextDouble() * height);
            position.X = (int)(_random.NextDouble() < 0.5 ? -_padding : width + _padding);
        }

        return position;
    }

    public static void AddGhost() // Создание призраков
    {
        Ghosts.Add(new(_texture, SetRandomPosition()));
    }

    public static void Update(Player player)
    {
        _spawnTime -= Globals.TotalSeconds;
        while (_spawnTime <= 0)
        {
            _spawnTime += _spawnCooldown; // Сброс таймера
            AddGhost();
        }

        foreach (var g in Ghosts)
        {
            g.Update(player);
        }
        Ghosts.RemoveAll((g) => g.healthPoints <= 0); // Удаление убитых призраков
    }

    public static void Draw()
    {
        foreach (var g in Ghosts)
        {
            g.Draw(); // Отрисовка призраков
        }
    }
}