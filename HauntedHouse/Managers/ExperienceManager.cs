namespace HauntedHouse;

public static class ExperienceManager
{
    private static Texture2D _texture;
    public static List<Experience> Experience { get; } = [];
    private static SpriteFont _font;
    private static Vector2 _position;
    private static Vector2 _textPosition; 
    private static string _playerExperience; // Текущий опыт игрока

    public static void Init(Texture2D texture) // Инициализация для статических классов
    {
        _texture = texture;
        _font = Globals.Content.Load<SpriteFont>("Score");
        _position = new(Globals.Bounds.X - (2 * _texture.Width), 0); // Позиция отрисовки текстуры опыта (эктоплазмы)
    }

    public static void Reset()
    {
        Experience.Clear();
    }

    public static void AddExperience(Vector2 position)
    {
        Experience.Add(new(_texture, position));
    }

    public static void Update(Player player)
    {
        foreach (var e in Experience)
        {
            e.Update();

            if ((e.Position - player.Position).Length() < 50) // Сбор очков опыта при достаточно близком расстоянии
            {
                e.CollectExperience();
                player.GetExperience(1);
            }
        }

        Experience.RemoveAll((e) => e.Lifespan <= 0);

        _playerExperience = player.Experience.ToString();
        var x = _font.MeasureString(_playerExperience).X / 2; // Преобразование длины строки с увеличением счёта
        _textPosition = new(Globals.Bounds.X - x - 32, 14);
    }

    public static void Draw()
    {
        Globals.SpriteBatch.Draw(_texture, _position, null, Color.White * 0.75f, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f); // Отрисовка текстуры опыта (эктоплазмы) 
        Globals.SpriteBatch.DrawString(_font, _playerExperience, _textPosition, Color.White); // Отрисовка счётчика опыта

        foreach (var e in Experience)
        {
            e.Draw(); // Отрисовка опыта на карте
        }
    }
}