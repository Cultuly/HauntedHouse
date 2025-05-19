namespace HauntedHouse;

public class Ghost : MovingSprite
{
    public int healthPoints { get; private set; } // Очки здоровья призрака

    public Ghost(Texture2D texture, Vector2 position) : base(texture, position)
    {
        Speed = 100 * DifficultySettings.GetGhostSpeedMultiplier(); // Скорость меняется от модификатора в зависимости от уровня сложности
        healthPoints = DifficultySettings.GetGhostHealthPoints(); // Здоровье призрака меняется от модификатора в зависимости от уровня сложности
    }

    public void TakeDamage(int damage) // Метод получения урона призраком
    {
        healthPoints -= damage;
        if (healthPoints <= 0) ExperienceManager.AddExperience(Position); // После смерти призраки оставляют очки опыта (эктоплазма)
    }

    public void Update(Player player)
    {
        var toPlayer = player.Position - Position;

        if (toPlayer.Length() > 4)
        {
            var direction = Vector2.Normalize(toPlayer);
            Position += direction * Speed * Globals.TotalSeconds;
        }
    }
}