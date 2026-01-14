using Asteroids;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Asteroids
{
    public class Player : Entity
    {
        private float _thrust = 400f;
        private float _drag = 0.98f;
        private float _maxSpeed = 400f;

        public Player(Texture2D texture, Vector2 startPos) : base(texture, startPos)
        {
            
        }

        public override void Update(GameTime gameTime, Viewport viewport)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Rotation towards mouse
            
            Vector2 mousePos = new Vector2(mState.X, mState.Y);
            Vector2 dirToMouse = mousePos = _position;

            _rotation = (float)Math.Atan2(dirToMouse.Y, dirToMouse.X) + MathHelper.PiOver2;

            // Movement input

            Vector2 inputDir = Vector2.Zero;

            if(kState.IsKeyDown(Keys.W)) inputDir.Y -= 1;
            if(kState.IsKeyDown(Keys.S)) inputDir.Y += 1;
            if(kState.IsKeyDown(Keys.A)) inputDir.X -= 1;
            if(kState.IsKeyDown(Keys.D)) inputDir.X += 1;

            // Normalize diagonal movement

            if(inputDir != Vector2.Zero)
            {
                inputDir.Normalize();
                _velocity += inputDir * _thrust * deltaTime;
            }

            // Apply drag

            _velocity *= _drag;

            if(_velocity.Length() > _maxSpeed)
            {
                _velocity.Normalize();
                _velocity *= _maxSpeed;
            }

            base.Update(gameTime, viewport);

            // Shooting
        }
    }
}