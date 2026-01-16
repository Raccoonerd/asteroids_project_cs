using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public static class UIManager
    {
        private static SpriteFont _font;
        private static Vector2 _screenCenter;
        private static Vector2 _screenSize;

        public static void Initialize(SpriteFont font, Viewport viewport)
        {
            _font = font;
            _screenSize = new Vector2(viewport.Width, viewport.Height);
            _screenCenter = _screenSize / 2f;
        }

        public static void DrawMenu(SpriteBatch spriteBatch)
        {
            
        }

        public static void DrawGameplay(SpriteBatch spriteBatch)
        {
            
        }

        public static void DrawGameOver(SpriteBatch spriteBatch)
        {
 
        }

        private static void DrawCenteredText(SpriteBatch spriteBatch, string text, float offsetY, Color color, float scale = 1.0f)
        {
            if(_font == null) return;

            Vector2 textSize = _font.MeasureString(text) * scale;

            Vector2 position = new Vector2(
                _screenCenter.X - textSize.X / 2f,
                _screenCenter.Y - textSize.Y / 2f + offsetY
            );

            spriteBatch.DrawString(
                _font,
                text,
                position,
                color,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}