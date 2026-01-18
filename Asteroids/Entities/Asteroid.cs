using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids
{
    public class Asteroid : Entity
    {
        private float _roatationSpeed;

        private const int _frameWidth = 128;
        private const int _frameHeight = 128;

        public Asteroid(Texture2D texture, Vector2 startPos, Vector2 velocity) : base(texture, startPos)
        {
            // Setting initial velocity and random rotation speed
            _velocity = velocity;
            Random rand = new Random();
            _roatationSpeed = (float)(rand.NextDouble() * 4 - 2);

            // Setting origin and radius for collision detection
            _origin = new Vector2(_frameWidth / 2f, _frameWidth / 2f);
            _radius = (_frameWidth / 2f) * _scale;
        }

        public override void Update(GameTime gameTime, Viewport viewport)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update rotation
            _rotation += _roatationSpeed * deltaTime;

            base.Update(gameTime, viewport);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(0, 0, _frameWidth, _frameHeight);

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

        public static void SpawnExplosion(Asteroid asteroid)
        {
            var explosion = new Explosion(
                asteroid._texture, 
                asteroid._position
            );

            EntityManager.Add(explosion);
        }
    }
}