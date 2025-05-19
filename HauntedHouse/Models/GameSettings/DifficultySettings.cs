namespace HauntedHouse;

public enum DifficultyLevel // Уровни сложности
{
    Easy,
    Medium,
    Hard
}

public class DifficultySettings
{
    public static DifficultyLevel CurrentDifficulty { get; private set; } = DifficultyLevel.Medium; // По умолчанию стоит средний уровень сложности
    
    private static readonly Dictionary<DifficultyLevel, float> GhostSpeedMultipliers = new() // Модификаторы для скорости для каждой сложности
    {
        { DifficultyLevel.Easy, 0.75f },
        { DifficultyLevel.Medium, 1.0f },
        { DifficultyLevel.Hard, 1.5f }
    };
    
    private static readonly Dictionary<DifficultyLevel, int> GhostHealthMultipliers = new() // Модификаторы для здоровья для каждой сложности 
    {
        { DifficultyLevel.Easy, 1 },
        { DifficultyLevel.Medium, 2 },
        { DifficultyLevel.Hard, 3 }
    };
    

    private static readonly Dictionary<DifficultyLevel, float> GhostSpawnRateMultipliers = new() // Модификаторы для спавна призраков для разных сложностей 
    {
        { DifficultyLevel.Easy, 1.5f },
        { DifficultyLevel.Medium, 1.0f },
        { DifficultyLevel.Hard, 0.6f }
    };
    
    public static void SetDifficulty(DifficultyLevel difficulty) // Метод установки сложности
    {
        CurrentDifficulty = difficulty;
    }
    
    public static float GetGhostSpeedMultiplier() // Метод для задания модификатора скорости
    {
        return GhostSpeedMultipliers[CurrentDifficulty];
    }
    
    public static int GetGhostHealthPoints() // Метод для задания модификатора здоровья призраков 
    {
        return GhostHealthMultipliers[CurrentDifficulty];
    }
    
    public static float GetGhostSpawnRateMultiplier() // Метод для задания модификатора спавна призраков
    {
        return GhostSpawnRateMultipliers[CurrentDifficulty];
    }
} 