namespace HauntedHouse;

public class Sprite (Texture2D texture, Vector2 position)
{
    protected readonly Texture2D Texture = texture;
    protected readonly Vector2 Origin = new( texture.Width/2, texture.Height/2 );
    public Vector2 Position { get; set; } = position;
    public float Rotation { get; set; }
    public float Scale { get; set; } = 1f;
    public Color Color { get; set; }= Color.White;

    public virtual void Update()
    {
        Globals.SpriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 1);
    }
}   