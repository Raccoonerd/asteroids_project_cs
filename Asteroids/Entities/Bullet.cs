using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids
{
    public class Bullet : Entity
    {
        private float _lifespan = 1.5f; // seconds
        private float _speed = 800f;
        public Bullet(Texture2D texture, Vector2 startPos, float rotation) : base(texture, startPos)
        {
            _rotation = rotation;

            // Setting initial velocity based on rotation    
            Vector2 direction = new Vector2(
                (float)Math.Cos(_rotation - MathHelper.PiOver2),
                (float)Math.Sin(_rotation - MathHelper.PiOver2)
            );
            
            _velocity = direction * _speed;

            _radius = 2f * _scale;
        }

        public override void Update(GameTime gameTime, Viewport viewport)
        {
            // Update lifespan
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _lifespan -= deltaTime;

            // Expire bullet if lifespan is over
            if(_lifespan <= 0f)
            {
                isExpired = true;
            }

            base.Update(gameTime, viewport);
        }
    }
}