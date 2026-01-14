using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids
{
    public class Asteroid : Entity
    {
        private float _roatationSpeed;

        public Asteroid(Texture2D texture, Vector2 startPos, Vector2 velocity) : base(texture, startPos)
        {
            // Setting initial velocity and random rotation speed
            _velocity = velocity;
            Random rand = new Random();
            _roatationSpeed = (float)(rand.NextDouble() * 4 - 2);
        }

        public override void Update(GameTime gameTime, Viewport viewport)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update rotation
            _rotation += _roatationSpeed * deltaTime;

            base.Update(gameTime, viewport);
        }
    }
}