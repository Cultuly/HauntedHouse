namespace HauntedHouse;

public class Button
{
    public bool IsSelected { get; set; } // Флаг клика по кнопке
    private readonly string _text;
    private readonly Vector2 _position;
    private readonly SpriteFont _font;
    private readonly Action _onClick;
    private Rectangle _hitbox;
    private bool _isHovered; // Флаг наведения на кнопку
    
    public Button(string text, Vector2 position, SpriteFont font, Action onClick)
    {
        _text = text;
        _position = position;
        _font = font;
        _onClick = onClick;
        
        Vector2 size = _font.MeasureString(text);
        Vector2 origin = size * 0.5f; // Отрисовка текста по центру
        _hitbox = new Rectangle((int)(_position.X - origin.X), (int)(_position.Y - origin.Y), (int)size.X, (int)size.Y);
    }
    
    public void Update()
    {
        MouseState mouse = Mouse.GetState();
        Point mousePosition = new(mouse.X, mouse.Y);
        
        _isHovered = _hitbox.Contains(mousePosition);
        
        if (_isHovered && InputManager.MouseClicked)
        {
            _onClick?.Invoke();
        }
    }
    
    public void Draw()
    {
        Color textColor = Color.White; // Цвет кнопки по умолчанию
        float scale = 1.0f; // Размер шрифта кнопки по умолчанию
        
        if (IsSelected) // Изменение цвета кнопки по клику на неё
        {
            textColor = Color.Yellow;
            scale = 1.0f;
        }
        else if (_isHovered) // Изменение цвета кнопки при наведении на неё
        {
            textColor = Color.LightGray;
            scale = 1.0f;
        }
        
        Vector2 size = _font.MeasureString(_text) * scale;
        Vector2 origin = size * 0.5f;
        
        Globals.SpriteBatch.DrawString(_font, _text, _position, textColor, 0f, origin, scale, SpriteEffects.None, 0f); // Отрисовка самой кнопки
    }
} 