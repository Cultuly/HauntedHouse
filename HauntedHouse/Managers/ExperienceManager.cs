namespace HauntedHouse;

public static class ExperienceManager
{
    private static Texture2D _texture;
    public static List<Experience> Experience { get; } = [];
    private const float AttractionRadius = 150f; // Радиус притяжения сферы с опытом
    private const float AttractionSpeed = 400f; // Скорость притяжения сферы с опытом
    private const float CollectionRadius = 10f; // Радиус сбора сфер опыта 

    public static void Init(Texture2D texture) // Инициализация (для статических классов)
    {
        _texture = texture;
    }

    public static void Reset() // Перезапуск
    {
        Experience.Clear();
    }

    public static void AddExperience(Vector2 position) // Создание сферы опыта (на месте призрака)
    {
        Experience.Add(new(_texture, position));
    }

    public static void Update(Player player)
    {
        foreach (var e in Experience)
        {
            float distanceToPlayer = (e.Position - player.Position).Length();

            // Притяжение сферы с опытом к игроку (при достаточном расстоянии)
            if (distanceToPlayer <= AttractionRadius)
            {
                Vector2 direction = Vector2.Normalize(player.Position - e.Position);
                float speedMultiplier = 1f - (distanceToPlayer / AttractionRadius);
                Vector2 movement = direction * AttractionSpeed * speedMultiplier * Globals.TotalSeconds;
                e.Position += movement;
            }

            // Игрок собирает опыт
            if (distanceToPlayer <= CollectionRadius)
            {
                e.CollectExperience();
                player.GetExperience(1);
            }
            else
            {
                e.Update();
            }
        }

        Experience.RemoveAll((e) => e.Lifespan <= 0); // Убирает все подобранные или истёкшие сферы опыта 
    }

    public static void Draw()
    {
        foreach (var e in Experience)
        {
            e.Draw(); // Отрисовка сфер опыта
        }
    }
}