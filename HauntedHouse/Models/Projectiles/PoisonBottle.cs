namespace HauntedHouse;

public class PoisonBottle : MovingSprite
{
    private const float MaxDistance = 300f; // Максимальная дистанция броска от игрока
    private const float Gravity = 800f; // Сила притяжения (падение)
    private Vector2 _startPosition;
    private Vector2 _velocity;
    private bool _hasLanded; // Флаг приземления
    private static Texture2D _poisonAreaTexture;

    public static void Init(Texture2D poisonAreaTexture)
    {
        _poisonAreaTexture = poisonAreaTexture;
    }

    public PoisonBottle(Texture2D texture, Vector2 position, float rotation) : base(texture, position)
    {
        _startPosition = position;
        float speed = 400f; // Скорость броска
        _velocity = new(
            (float)Math.Cos(rotation) * speed,
            (float)Math.Sin(rotation) * speed
        );
        _hasLanded = false;
    }

    public void Update()
    {
        if (_hasLanded) return;

        // Обработка позиции после приземления (гравитация)
        _velocity.Y += Gravity * Globals.TotalSeconds;
        Position += _velocity * Globals.TotalSeconds;

        float distanceTraveled = (Position - _startPosition).Length();
        if (distanceTraveled >= MaxDistance)
        {
            Land();
        }

        // Обработка столкновения
        if (Position.Y >= Globals.Bounds.Y - Texture.Height)
        {
            Land();
        }
    }

    private void Land() // Приземление
    {
        _hasLanded = true;
        PoisonAreaManager.AddPoisonArea(Position, _poisonAreaTexture);
    }

    public bool HasLanded => _hasLanded;

    public override void Draw()
    {
        Globals.SpriteBatch.Draw( // Отрисовка бутылки с ядом
            Texture,
            Position,
            null,
            Color.White,
            Rotation,
            new Vector2(Texture.Width / 2f, Texture.Height / 2f),
            2f,
            SpriteEffects.None,
            0f
        );
    }
}