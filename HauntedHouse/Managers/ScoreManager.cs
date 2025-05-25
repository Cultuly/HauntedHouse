namespace HauntedHouse;

public static class ScoreManager
{
    private static SpriteFont _font;
    private static Vector2 _position;
    private static string _scoreText;
    private static int _score;

    public static void Init() // Инициализация (для статических классов)
    {
        _font = Globals.Content.Load<SpriteFont>("Score");
        _position = new(Globals.Bounds.X - 200, 20);
        _score = 0;
        UpdateScoreText();
    }

    public static void Reset() // Перезапуск
    {
        _score = 0; // Счёт сбрасывается
        UpdateScoreText();
    }

    public static void AddScore(int points) // Добавляет очки к счёту
    {
        _score += points;
        UpdateScoreText();
    }

    private static void UpdateScoreText() // Обработка надписи счёта
    {
        _scoreText = $"Score: {_score}";
    }

    public static void Draw()
    {
        Globals.SpriteBatch.DrawString(_font, _scoreText, _position, Color.White); // Отрисовка надписи счёт (правый верхний угол)
    }
}