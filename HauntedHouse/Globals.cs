using Microsoft.Xna.Framework.Content;

namespace HauntedHouse;

public static class Globals
{
    public static float TotalSeconds { get; set; } // Время между кадрами
    public static ContentManager Content { get; set; } 
    public static SpriteBatch SpriteBatch { get; set; }
    public static Point Bounds { get; set; } // Размеры игрового окна

    public static void Update(GameTime GameTime)
    {
        TotalSeconds = (float)GameTime.ElapsedGameTime.TotalSeconds;
    }
}