namespace HauntedHouse;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager _gameManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Globals.Bounds = new(1024, 720); // Разрешение игрового окна
        _graphics.PreferredBackBufferWidth = Globals.Bounds.X; // Разрешение по X
        _graphics.PreferredBackBufferHeight = Globals.Bounds.Y; // Разрешение по Y
        _graphics.ApplyChanges();

        Globals.Content = Content;
        _gameManager = new();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = _spriteBatch;
    }

    protected override void Update(GameTime gameTime)
    {
        // Выход из игры только если игра в состоянии паузы и нажат ESC
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
            (Keyboard.GetState().IsKeyDown(Keys.Escape) && MenuManager.CurrentState == GameState.MainMenu))
            Exit();

        Globals.Update(gameTime);
        _gameManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _gameManager.Draw();
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}