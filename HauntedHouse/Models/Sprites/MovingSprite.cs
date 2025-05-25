namespace HauntedHouse;

public class MovingSprite(Texture2D texture, Vector2 position) : Sprite(texture, position)
{
    public float Speed { get; set; } = 300f; // Скорость перемещения
}