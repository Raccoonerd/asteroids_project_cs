using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public class Explosion : Entity
    {
        private float _timer;
        private float _interval = 0.05f;

        private int _currentFrame = 1;
        private int _totalFrames = 7;

        private int _frameWidth = 128;

        public Explosion(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _origin = new Vector2(_frameWidth / 2f, _frameWidth / 2f);

            _scale = 2f;
        }

        public override void Update(GameTime gameTime, Viewport viewport)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_timer >= _interval)
            {
                _currentFrame++;
                _timer = 0f;

                if(_currentFrame > _totalFrames)
                {
                    isExpired = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(
                _currentFrame * _frameWidth,
                0,
                _frameWidth,
                _texture.Height
            );

            spriteBatch.Draw(
                _texture,
                _position,
                sourceRect,
                Color.White,
                _rotation,
                _origin,
                new Vector2(_scale, _scale),
                SpriteEffects.None,
                0f
            );
        }
    }
}