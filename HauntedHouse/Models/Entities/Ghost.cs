namespace HauntedHouse;

public class Ghost : MovingSprite
{
    public int healthPoints {get; private set;} // Счётчик здоровья

    public Ghost(Texture2D texture, Vector2 position) : base (texture, position)
    {
        healthPoints = 8;
        Speed = 100;
    }

    public void GetDamage( int damage) // Метод получения урона
    {
        healthPoints -= damage;
    }

    public void Update(Player player)
    {
        var toPlayer = player.Position - Position; // Направление к игроку
        Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

        if (toPlayer.Length() > 4)
        {
            var direction = Vector2.Normalize(toPlayer);
            Position = direction * Speed * Globals.TotalSeconds;
        }
    }

    
}