namespace HauntedHouse;
public class Player : MovingSprite
{
    public Player(Texture2D texture) : base (texture, GetStartPosition())
    {

    }

    private static Vector2 GetStartPosition()  // Начальное положение игрока на экране
    {
        return new (Globals.Bounds.X/2, Globals.Bounds.Y/2);
    }
}

