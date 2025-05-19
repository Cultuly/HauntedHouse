namespace HauntedHouse;

public enum GameState // Состояния игры
{
    MainMenu,
    InGame,
    Paused
}

public static class MenuManager
{
    public static GameState CurrentState { get; private set; } = GameState.MainMenu; // Состояние игры по умолчанию (главное меню)
    private static SpriteFont _font;
    private static Button _easyButton;
    private static Button _mediumButton;
    private static Button _hardButton;
    private static Button _startButton;
    private static DifficultyLevel _selectedDifficulty = DifficultyLevel.Medium; // Сложность игры по умолчанию (средний)

    public static void Init() // Метод инициализации для статических классов
    {
        _font = Globals.Content.Load<SpriteFont>("Score");
        int centerX = Globals.Bounds.X / 2; 
        
        // Параметры для отрисовки кнопок сложности в главном меню
        _easyButton = new Button("Легкий", new Vector2(centerX, 250), _font, SelectEasy);
        _mediumButton = new Button("Средний", new Vector2(centerX, 300), _font, SelectMedium);
        _hardButton = new Button("Сложный", new Vector2(centerX, 350), _font, SelectHard);
        _startButton = new Button("Начать игру", new Vector2(centerX, 450), _font, StartGame);
        
        //_mediumButton.IsSelected = true;
    }
    
    private static void SelectEasy() // Метод выбора лёгкой сложности
    {
        _selectedDifficulty = DifficultyLevel.Easy;
        _easyButton.IsSelected = true;
        _mediumButton.IsSelected = false;
        _hardButton.IsSelected = false;
    }
    
    private static void SelectMedium() // Метод выбора средней сложности
    {
        _selectedDifficulty = DifficultyLevel.Medium;
        _easyButton.IsSelected = false;
        _mediumButton.IsSelected = true;
        _hardButton.IsSelected = false;
    }
    
    private static void SelectHard() // Метод выбора тяжёлой сложности
    {
        _selectedDifficulty = DifficultyLevel.Hard;
        _easyButton.IsSelected = false;
        _mediumButton.IsSelected = false;
        _hardButton.IsSelected = true;
    }
    
    private static void StartGame() // Переход к самой игре из главного меню
    {
        DifficultySettings.SetDifficulty(_selectedDifficulty);
        CurrentState = GameState.InGame;
    }
    
    public static void TogglePause() // Метод переключения паузы в игре
    {
        if (CurrentState == GameState.InGame)
            CurrentState = GameState.Paused;
        else if (CurrentState == GameState.Paused)
            CurrentState = GameState.InGame;
    }
    
    public static void ReturnToMainMenu()
    {
        CurrentState = GameState.MainMenu;
    }
    
    public static void Update()
    {
        if (CurrentState == GameState.MainMenu)
        {
            _easyButton.Update();
            _mediumButton.Update();
            _hardButton.Update();
            _startButton.Update();
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !InputManager.LastKeyboardState.IsKeyDown(Keys.Escape))
        {
            if (CurrentState == GameState.InGame || CurrentState == GameState.Paused)
                TogglePause(); // При нажатии ESC открывается меню паузы
        }
    }
    
    public static void Draw()
    {
        if (CurrentState == GameState.MainMenu)
        {
            // Отрисовка названия в главном меню
            string title = "Haunted House";
            Vector2 titleSize = _font.MeasureString(title) * 1.5f;
            Globals.SpriteBatch.DrawString(_font, title, new Vector2(Globals.Bounds.X / 2 - titleSize.X / 2, 100), Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
                
            // Отрисовка меню выбора сложности
            string subtitle = "Выберите уровень сложности:";
            Vector2 subtitleSize = _font.MeasureString(subtitle);
            Globals.SpriteBatch.DrawString(_font, subtitle, new Vector2(Globals.Bounds.X / 2 - subtitleSize.X / 2, 175), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            
            // Отрисовка кнопок сложности
            _easyButton.Draw();
            _mediumButton.Draw();
            _hardButton.Draw();
            _startButton.Draw();
        }
        else if (CurrentState == GameState.Paused)
        {
            // Отрисовка экрана паузы (если нажат ESC)
            string pauseText = "ПАУЗА";
            Vector2 pauseSize = _font.MeasureString(pauseText) * 2f;
            Globals.SpriteBatch.DrawString(_font, pauseText, new Vector2(Globals.Bounds.X / 2 - pauseSize.X / 2, Globals.Bounds.Y / 2 - pauseSize.Y / 2), Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
            
            // Отрисовка надписи продолжить (если игра в состоянии паузы)
            string resumeText = "Нажмите ESC для продолжения";
            Vector2 resumeSize = _font.MeasureString(resumeText);
            Globals.SpriteBatch.DrawString(_font, resumeText, new Vector2(Globals.Bounds.X / 2 - resumeSize.X / 2, Globals.Bounds.Y / 2 + 50), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
    }
} 