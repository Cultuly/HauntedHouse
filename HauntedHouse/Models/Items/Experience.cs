namespace HauntedHouse;

public class Experience : Sprite
{
    public float Lifespan { get; private set; } = Life;
    private const float Life = 5f; // Время существования единицы опыта на карте

    public Experience(Texture2D texture, Vector2 position) : base(texture, position)
    {
    }

    public void Update()
    {
        Lifespan -= Globals.TotalSeconds;
    }

    public void CollectExperience() // Метод подбора опыта
    {
        Lifespan = 0;
    }
}