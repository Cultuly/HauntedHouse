namespace HauntedHouse;

public static class GhostManager
{
    public static List<Ghost> Ghosts {get;} = [];
    private static Texture2D _texture;
    private static float _spawnCooldown; // Время между спавном призраков
    private static float _spawnTime; // Таймер спавна призраков
    private static Random _rand;
    private static int _padding;

    public static void Init()
    {
        _texture = Globals.Content.Load<Texture2D>("Ghost");
        _spawnCooldown = 0.5f;
        _spawnTime = _spawnCooldown;
        _rand = new();
        _padding = _texture.Width/2;
    }

    public static void Reset()
    {
        Ghosts.Clear();
        _spawnTime = _spawnCooldown; // Откат таймера спавна
    }

    private static Vector2 SetRandomPosition() // Метод появления призраков в рандомной позиции за пределами экрана
    {
        float width = Globals.Bounds.X; // Ширина экрана
        float height = Globals.Bounds.Y; // Высота экрана
        Vector2 position = new();

        if (_rand.NextDouble() <  width / (width + height))
        {
            position.X = (int)(_rand.NextDouble() * width);
            position.Y = (int)(_rand.NextDouble() < 0.5 ? -_padding : height + _padding);
        }
        else
        {
            position.Y = (int)(_rand.NextDouble() * height);
            position.X = (int)(_rand.NextDouble() < 0.5 ? -_padding : width + _padding);
        }

        return position;
    }

    public static void AddGhost() // Метод создания призраков
    {
        Ghosts.Add(new(_texture, SetRandomPosition()));
    }

    public static void Update(Player player)
    {
        _spawnTime -= Globals.TotalSeconds;
        while (_spawnTime <= 0)
        {
            _spawnTime += _spawnCooldown;
            AddGhost();
        }

        foreach (var g in Ghosts)
        {
            g.Update(player);
        }
        Ghosts.RemoveAll((g => g.healthPoints <= 0 )); // Убирает всех призраков чьё хп меньше или равно 0
    }

    public static void Draw() // Отрисовка призраков
    {
        foreach (var g in Ghosts)
        {
            g.Draw();
        }
    }
}