namespace HauntedHouse;

public class MovingSprite(Texture2D texture, Vector2 position) : Sprite(texture, position)
{
    public int Speed { get; set; } = 300; // Стандартная скорость перемещения
}