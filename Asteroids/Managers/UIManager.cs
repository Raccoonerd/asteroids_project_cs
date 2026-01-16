using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public static class UIManager
    {
        private static SpriteFont _font;
        private static Texture2D _titleTexture;
        private static Vector2 _screenCenter;
        private static Vector2 _screenSize;

        public static void Initialize(SpriteFont font, Viewport viewport, Texture2D titleTexture)
        {
            _font = font;
            _titleTexture = titleTexture;
            _screenSize = new Vector2(viewport.Width, viewport.Height);
            _screenCenter = _screenSize / 2f;
        }

        public static void DrawMenu(SpriteBatch spriteBatch)
        {
            if(_titleTexture != null)
            {
                Vector2 origin = new Vector2(_titleTexture.Width / 2f, _titleTexture.Height / 2f);
                Vector2 pos = new Vector2(_screenCenter.X, _screenCenter.Y - 100f);

                spriteBatch.Draw(
                    _titleTexture,
                    pos,
                    null,
                    Color.White,
                    0f,
                    origin,
                    1.0f,
                    SpriteEffects.None,
                    0f
                );
            }

            DrawCenteredText(spriteBatch, "Press ENTER to Start", 100f, Color.MediumPurple);
        }

        public static void DrawGameplay(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                _font,
                $"WAVE: {GameManager._level}",
                new Vector2(20, 20),
                Color.BlueViolet
            );

            spriteBatch.DrawString(
                _font,
                $"SCORE: {GameManager._score}",
                new Vector2(20, 50),
                Color.BlueViolet,
                0f,
                Vector2.Zero,
                1.5f,
                SpriteEffects.None,
                0f
            );

            
        }

        public static void DrawGameOver(SpriteBatch spriteBatch)
        {
            DrawCenteredText(spriteBatch, "GAME OVER", -100f, Color.Maroon, 2.0f);
            DrawCenteredText(spriteBatch, $"Your score: {GameManager._score}", 0f, Color.DarkOrchid, 1.5f);
            DrawCenteredText(spriteBatch, "Press ENTER to Start", 50f, Color.MediumPurple);
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