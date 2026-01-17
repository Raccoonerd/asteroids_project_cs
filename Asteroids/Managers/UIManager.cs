using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Asteroids
{
    public static class UIManager
    {
        private static SpriteFont _font;
        private static Texture2D _titleTexture;
        private static Texture2D _gameOverTexture;
        private static Vector2 _screenCenter;
        private static Vector2 _screenSize;

        private static int _selectIndex = 0;

        public static SoundEffect _uiChangeSound;

        public static void Initialize(SpriteFont font, Viewport viewport, Texture2D titleTexture, Texture2D gameOverTexture)
        {
            _font = font;
            _titleTexture = titleTexture;
            _gameOverTexture = gameOverTexture;
            _screenSize = new Vector2(viewport.Width, viewport.Height);
            _screenCenter = _screenSize / 2f;
            _selectIndex = 0;
        }

        public static int UpdateMenu(KeyboardState currKState, KeyboardState prevKState)
        {
            if(prevKState.IsKeyUp(Keys.S) &&
               currKState.IsKeyDown(Keys.S))
            {
                _selectIndex = (_selectIndex + 1) % 2;
                _uiChangeSound.Play();
            }

            if(prevKState.IsKeyUp(Keys.W) &&
               currKState.IsKeyDown(Keys.W))
            {
                _selectIndex = (_selectIndex - 1 + 2) % 2;
                _uiChangeSound.Play();
            }

            if(prevKState.IsKeyUp(Keys.Enter) &&
               currKState.IsKeyDown(Keys.Enter))
            {
                if(_selectIndex == 0)
                {
                    return 0;
                }
                else if(_selectIndex == 1)
                {
                    return 1;
                }
            }

            return -1;
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

            // Draw menu options
            Color playColor = (_selectIndex == 0) ? Color.Gold : Color.MediumPurple;
            string playText = (_selectIndex == 0) ? "> PLAY <" : "PLAY";
            DrawCenteredText(spriteBatch, playText, 50f, playColor, 1.5f);

            Color exitColor = (_selectIndex == 1) ? Color.Gold : Color.MediumPurple;
            string exitText = (_selectIndex == 1) ? "> EXIT <" : "EXIT";
            DrawCenteredText(spriteBatch, exitText, 100f, exitColor, 1.5f);

            DrawCenteredText(spriteBatch, "Use W/S to Navigate, ENTER to Select", 200f, Color.DarkOrchid, 0.7f);
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
            if(_gameOverTexture != null)
            {
                Vector2 origin = new Vector2(_gameOverTexture.Width / 2f, _gameOverTexture.Height / 2f);
                Vector2 pos = new Vector2(_screenCenter.X, _screenCenter.Y - 100f);

                spriteBatch.Draw(
                    _gameOverTexture,
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

            DrawCenteredText(spriteBatch, $"Your score: {GameManager._score}", 0f, Color.DarkOrchid, 1.5f);
            DrawCenteredText(spriteBatch, "Press ENTER to Continue", 50f, Color.MediumPurple);
        }

        private static void DrawCenteredText(
            SpriteBatch spriteBatch, 
            string text, 
            float offsetY, 
            Color color, 
            float scale = 1.0f)
        {
            if(_font == null) return;

            // Measure text size
            Vector2 textSize = _font.MeasureString(text) * scale;

            // Calculate position to center the text
            Vector2 position = new Vector2(
                _screenCenter.X - textSize.X / 2f,
                _screenCenter.Y - textSize.Y / 2f + offsetY
            );

            // Draw the text
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