namespace HauntedHouse;

public class PoisonArea
{
    private const float Duration = 5f; // Время существования области с ядом
    private const float DamageInterval = 0.5f; // Промежуток между нанесением урона
    private const int DamagePerTick = 1; // Урон от области с ядом
    private const float Radius = 200f; // Радиус действия области с ядом

    private Vector2 _position;
    private Texture2D _texture;
    private float _timeLeft;
    private float _damageTimer;
    private Color _tint;
    public bool IsExpired => _timeLeft <= 0; // Флаг истечения таймера

    public PoisonArea(Vector2 position, Texture2D texture)
    {
        _position = position;
        _texture = texture;
        _timeLeft = Duration;
        _damageTimer = 0f;
        _tint = new Color(0.5f, 1f, 0.5f, 0.7f); // Цвет перекраски врагов от яда
    }

    public void Update(List<Ghost> ghosts)
    {
        if (_timeLeft <= 0) return;

        // Сброс таймеров
        _timeLeft -= Globals.TotalSeconds;
        _damageTimer -= Globals.TotalSeconds;

        // По истечении таймера наносит урон
        if (_damageTimer <= 0)
        {
            _damageTimer = DamageInterval;
            ApplyDamage(ghosts);
        }
    }

    private void ApplyDamage(List<Ghost> ghosts) // Обработка нанесения урона (в радиусе действия области с ядом)
    {
        foreach (var ghost in ghosts)
        {
            if (ghost.healthPoints <= 0) continue;

            float distance = (ghost.Position - _position).Length();
            if (distance <= Radius)
            {
                ghost.TakePoisonDamage(DamagePerTick); // Враг получает урон
            }
        }
    }

    public void Draw()
    {
        if (_timeLeft <= 0) return;

        // Прозрачность меняется со временем (урон от яда)
        float alpha = _timeLeft / Duration;
        Color drawColor = new Color(_tint.R, _tint.G, _tint.B, _tint.A * alpha);

        // Отрисовка области с ядом
        Globals.SpriteBatch.Draw(
            _texture,
            _position,
            null,
            drawColor,
            0f,
            new Vector2(_texture.Width / 2f, _texture.Height / 2f),
            4f,
            SpriteEffects.None,
            0f
        );
    }

    
}