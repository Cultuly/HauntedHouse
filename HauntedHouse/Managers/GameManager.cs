namespace HauntedHouse;

public class GameManager
{
    private readonly Player _player;
    private readonly Map _map;

    public GameManager()
    {
        _map = new();
        var texture = Globals.Content.Load<Texture2D>("Bullet");
        ProjectileManager.Init(texture);
        ExperienceManager.Init(Globals.Content.Load<Texture2D>("Ectoplasm"));

        _player = new(Globals.Content.Load<Texture2D>("Player"));
        GhostManager.Init();
    }

    public void Restart() // Перезапуск
    {
        ProjectileManager.Reset();
        GhostManager.Reset();
        ExperienceManager.Reset();
        _player.Reset();
    }

    public void Update() // Обновляет логику всех элементов
    {
        InputManager.Update();
        ExperienceManager.Update(_player);
        _player.Update(GhostManager.Ghosts);
        GhostManager.Update(_player);
        ProjectileManager.Update(GhostManager.Ghosts);

        if (_player.Dead) Restart();
    }

    public void Draw() // Отрисовывает все элементы в необходимом порядке
    {
        _map.Draw();
        ExperienceManager.Draw();
        ProjectileManager.Draw();
        _player.Draw();
        GhostManager.Draw();
    }
}