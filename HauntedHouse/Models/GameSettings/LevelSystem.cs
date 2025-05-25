namespace HauntedHouse;

public static class LevelSystem
{
    public static int CurrentLevel { get; private set; } = 1;
    public static int ExperienceToNextLevel { get; private set; }
    private const int BaseExperienceRequired = 3; // Количество опыта до 1 уровня
    private const float ExperienceMultiplier = 1.2f; // Множитель опыта для повышения до следующего уровня
    private const float HealthPerLevel = 1f; // Увеличивает количество здоровья игрока при повышении уровня
    private const float DamagePerLevel = 0.5f; // Увеличивает урон игрока при повышении уровня

    public static void Init() // Инициализация (для статических классов)
    {
        UpdateExperienceRequired();
    }

    public static void Reset() // Перезапуск
    {
        // При перезапуске сбрасывает параметры к изначальным
        CurrentLevel = 1;
        UpdateExperienceRequired();
    }

    private static void UpdateExperienceRequired()
    {
        ExperienceToNextLevel = (int)(BaseExperienceRequired * Math.Pow(ExperienceMultiplier, CurrentLevel - 1));
    }

    public static bool TryLevelUp(int currentExperience) // Обработка повышения уровня
    {
        if (currentExperience >= ExperienceToNextLevel)
        {
            CurrentLevel++; // Увеличивает уровень игрока
            UpdateExperienceRequired(); // Обновляет счётчик опыта до следующего уровня
            return true;
        }
        return false;
    }

    public static float GetHealthMultiplier() // Увеличивает здоровье при повышении уровня
    {
        return 1f + (CurrentLevel - 1) * HealthPerLevel;
    }

    public static float GetDamageMultiplier() // Увеличивает урон при повышении уровня
    {
        return 1f + (CurrentLevel - 1) * DamagePerLevel;
    }

    public static int GetMaxHealth() // Изначальное здоровье
    {
        return (int)(3 * GetHealthMultiplier());
    }
}