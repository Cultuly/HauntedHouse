namespace HauntedHouse;

public class GameManager
{
    private readonly Player _player;
    private readonly Map _map;

    public GameManager()
    {
        _map = new(); 
        var texture = Globals.Content.Load<Texture2D>("Bullet");
        _player = new (Globals.Content.Load<Texture2D>("Player"));
        ProjectilesManager.Init(texture);
        GhostManager.Init();
    }

    public void Restart()
    {
        ProjectilesManager.Reset();
        GhostManager.Reset();
        _player.Reset();
    }

    public void Update()
    {
        InputManager.Update();
        ProjectilesManager.Update(GhostManager.Ghosts);
        _player.Update(GhostManager.Ghosts);

        if (_player.Dead)
        {
            Restart();
        }
    }

    public void Draw()
    {
        _map.Draw();
        ProjectilesManager.Draw();
        _player.Draw();
    }
}