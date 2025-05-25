namespace HauntedHouse;

public static class PoisonAreaManager
{
    private static readonly List<PoisonArea> _poisonAreas = new();

    public static void AddPoisonArea(Vector2 position, Texture2D texture)
    {
        _poisonAreas.Add(new PoisonArea(position, texture));
    }

    public static void Update(List<Ghost> ghosts)
    {
        // Убирает области с ядом (когда таймер истекает)
        for (int i = _poisonAreas.Count - 1; i >= 0; i--)
        {
            _poisonAreas[i].Update(ghosts);
            if (_poisonAreas[i].IsExpired)
            {
                _poisonAreas.RemoveAt(i);
            }
        }
    }

    public static void Draw()
    {
        foreach (var area in _poisonAreas)
        {
            area.Draw(); // Отрисовывает области с ядом
        }
    }

    public static void Reset() // Перезапуск
    {
        _poisonAreas.Clear(); // Убирает все области с ядом
    }
}