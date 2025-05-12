namespace HauntedHouse;

public class Sprite(Texture2D texture, Vector2 position)
{
    protected readonly Texture2D Texture = texture;
    protected readonly Vector2 origin = new(texture.Width / 2, texture.Height / 2); // Точка отрисовки текстуры
    public Vector2 Position { get; set; } = position; // Позиция
    public float Rotation { get; set; } // Поворот
    public float Scale { get; set; } = 2f; // Масштабирование
    public Color Color { get; set; } = Color.White;

    public virtual void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color, Rotation, origin, Scale, SpriteEffects.None, 1);
    }
}