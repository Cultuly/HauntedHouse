namespace HauntedHouse;

public class ExperienceOrb
{
    private static Texture2D _texture;
    private Vector2 _position;
    private Vector2 _velocity;
    private const float Speed = 400f; // Скорость притяжения сферы с опытом
    private const float AttractionRadius = 150f; // Радиус притяжения сферы
    private const int ExperienceValue = 1; // Количество получаемого опыта при подборе сферы
    private const float Size = 10f; // Размер сферы
    private bool _isCollected; // Флаг подбора сферы

    public bool IsCollected => _isCollected;

    public static void LoadContent()
    {
        _texture = new Texture2D(Globals.GraphicsDevice, 1, 1);
        _texture.SetData(new[] { Color.White });
    }

    public ExperienceOrb(Vector2 position)
    {
        _position = position;
        _velocity = Vector2.Zero;
        _isCollected = false;
    }

    public void Update(Player player)
    {
        if (_isCollected) return;

        // Вычисление расстояния от сферы до игрока
        float distanceToPlayer = Vector2.Distance(_position, player.Position);

        // Притяжение сферы к игроку (если игрок близко)
        if (distanceToPlayer <= AttractionRadius)
        {
            // Направление к игроку
            Vector2 direction = Vector2.Normalize(player.Position - _position);
            
            // Увеличивает скорость притяжения сферы (если игрок очень близко)
            float speedMultiplier = 1f - (distanceToPlayer / AttractionRadius);
            _velocity = direction * Speed * speedMultiplier;
        }
        else
        {
            // Уменьшает скорость притяжения (если игрок далеко)
            _velocity *= 0.95f;
        }

        _position += _velocity * Globals.TotalSeconds;

        // Игрок получает опыт
        if (distanceToPlayer <= Size)
        {
            _isCollected = true;
            player.GetExperience(ExperienceValue);
        }
    }

    public void Draw()
    {
        if (_isCollected) return;

        // Отрисовка сферы с опытом
        Globals.SpriteBatch.Draw(
            _texture,
            _position,
            null,
            Color.Yellow,
            0f,
            new Vector2(0.5f),
            Size,
            SpriteEffects.None,
            0f
        );
    }
}