namespace HauntedHouse;

public static class InputManager
{
    private static KeyboardState _lastKeyboardState;
    private static MouseState _lastMouseState;
    private static Vector2 _direction;
    public static Vector2 Direction => _direction;
    public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2(); // Позиция мышки
    public static bool MouseClicked { get; private set; } // Флаг клика ЛКМ
    public static bool ReloadKeyPressed {get; private set;} // Флаг прожатия R
    public static bool MouseLeftDown {get; private set;}
    public static bool SpacePressed {get; private set;} // Флаг клика пробела


    public static void Update()
    {
        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        _direction = Vector2.Zero; // Вектор направления сбрасываем до нуля
        
        foreach (var key in keyboardState.GetPressedKeys())
        {
            switch (key) // Обработка перемещения
            {
                case Keys.W:
                    _direction.Y--; // Двжиение вверх
                    break;
                case Keys.S:
                    _direction.Y++; // Движение вниз
                    break;
                case Keys.A:
                    _direction.X--; // Движение влево
                    break;
                case Keys.D:
                    _direction.X++; // Движение вправо
                    break;
            }
        }

        MouseLeftDown = mouseState.LeftButton == ButtonState.Pressed; // Обработка состояния ЛКМ
        MouseClicked = MouseLeftDown && (_lastMouseState.LeftButton == ButtonState.Released); // Обработка клика ЛКМ
        ReloadKeyPressed = keyboardState.IsKeyDown(Keys.R) && _lastKeyboardState.IsKeyUp(Keys.R); // Обработка клика R для перезарядки

        SpacePressed = _lastKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space); // Обработка нажатия пробела

        _lastKeyboardState = keyboardState;
        _lastMouseState = mouseState;
    }
}