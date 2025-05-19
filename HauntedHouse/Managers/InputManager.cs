namespace HauntedHouse;

public static class InputManager
{
    private static MouseState _lastMouseState;
    private static KeyboardState _lastKeyboardState;
    public static KeyboardState LastKeyboardState => _lastKeyboardState;
    public static MouseState LastMouseState => _lastMouseState;
    private static Vector2 _direction;
    public static Vector2 Direction => _direction;
    public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
    public static bool MouseClicked { get; private set; } // Флаг клика ЛКМ
    public static bool ReloadKeyPressed { get; private set; } // Флаг нажатия R
    public static bool MouseLeftDown { get; private set; } // Зажатие ЛКМ
    public static bool SpacePressed { get; private set; } // Обработка прожатия пробела

    public static void Update()
    {
        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        _direction = Vector2.Zero; // Обнуляем вектор перемещения
        
        foreach (var key in keyboardState.GetPressedKeys())
        {
            switch (key) // Обработка перемещения
            {
                case Keys.W:
                    _direction.Y--; // Движение вверх
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

        MouseLeftDown = mouseState.LeftButton == ButtonState.Pressed; // Обработка зажатия ЛКМ
        MouseClicked = MouseLeftDown && (_lastMouseState.LeftButton == ButtonState.Released); // Обработка клика ЛКМ
        ReloadKeyPressed = keyboardState.IsKeyDown(Keys.R) && _lastKeyboardState.IsKeyUp(Keys.R); // Обработка клика R для перезарядки

        SpacePressed = _lastKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space); // Обработка нажатия пробела

        _lastMouseState = mouseState;
        _lastKeyboardState = keyboardState;
    }
}