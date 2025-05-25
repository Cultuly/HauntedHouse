namespace HauntedHouse;

public class GameManager
{
    private readonly Player _player;
    private readonly Map _map;
    private static List<PoisonBottle> _poisonBottles = new();
    private static Texture2D _poisonBottleTexture;
    private static Texture2D _poisonAreaTexture;
    private static float _poisonBottleCooldown = 0f;
    private const float PoisonBottleCooldownTime = 5f; // Кулдаун для бутылки с ядом (в секундах)

    public GameManager()
    {
        //Загрузка текстур
        _map = new();
        var bulletTexture = Globals.Content.Load<Texture2D>("Bullet");
        var ghostBulletTexture = Globals.Content.Load<Texture2D>("GhostBullet");
        var experienceTexture = Globals.Content.Load<Texture2D>("Ectoplasm");
        _poisonBottleTexture = Globals.Content.Load<Texture2D>("PoisonBottle");
        _poisonAreaTexture = Globals.Content.Load<Texture2D>("PoisonArea");
        _player = new(Globals.Content.Load<Texture2D>("Player"));

        //Инициализация статических классов
        ProjectileManager.Init(bulletTexture, ghostBulletTexture);
        PoisonBottle.Init(_poisonAreaTexture);
        ExperienceManager.Init(experienceTexture);
        ScoreManager.Init();
        GhostManager.Init();
        MenuManager.Init();
    }

    public void Restart() // Перезапуск
    {
        ProjectileManager.Reset();
        GhostManager.Reset();
        ExperienceManager.Reset();
        ScoreManager.Reset();
        PoisonAreaManager.Reset();
        _poisonBottles.Clear();
        _poisonBottleCooldown = 0f;
        _player.Reset();
    }

    public void Update()
    {
        InputManager.Update();
        MenuManager.Update();
        
        if (MenuManager.CurrentState == GameState.InGame)
        {
            ExperienceManager.Update(_player);
            _player.Update(GhostManager.Ghosts);

            // Обновляет кулдаун для каждой бутылки с ядом
            if (_poisonBottleCooldown > 0)
            {
                _poisonBottleCooldown -= Globals.TotalSeconds;
            }

            // Обработка логики бутылки с ядом
            for (int i = _poisonBottles.Count - 1; i >= 0; i--)
            {
                _poisonBottles[i].Update();
                if (_poisonBottles[i].HasLanded)
                {
                    _poisonBottles.RemoveAt(i);
                }
            }

            // После броска бутылки с ядом (сбрасывает кулдаун)
            if (InputManager.ThrowPoisonBottlePressed && _poisonBottleCooldown <= 0)
            {
                _poisonBottles.Add(new PoisonBottle(_poisonBottleTexture, _player.Position, _player.Rotation));
                _poisonBottleCooldown = PoisonBottleCooldownTime;
            }

            GhostManager.Update(_player);
            ProjectileManager.Update(_player, GhostManager.Ghosts);
            PoisonAreaManager.Update(GhostManager.Ghosts);

            // Обработка смерти игрока
            if (_player.Dead)
            {
                Restart();
                MenuManager.ReturnToMainMenu();
            }
        }
    }

    public void Draw()
    {
        if (MenuManager.CurrentState == GameState.MainMenu)
        {
            _map.Draw();
            MenuManager.Draw();
        }
        else
        {
            _map.Draw();
            ExperienceManager.Draw();
            ScoreManager.Draw();
            PoisonAreaManager.Draw();
            ProjectileManager.Draw();
            foreach (var bottle in _poisonBottles)
            {
                bottle.Draw();
            }
            _player.Draw();
            GhostManager.Draw();
            
            if (MenuManager.CurrentState == GameState.Paused)
            {
                MenuManager.Draw();
            }
        }
    }
}