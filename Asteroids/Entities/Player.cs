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
        private float _drag = 1.5f;
        private float _maxSpeed = 450f;

        private float _rotationSpeed = 3.5f;

        private KeyboardState _prevKeyState;
        private KeyboardState _currKeyState;

        public static Texture2D bulletTexture;

        public Player(Texture2D texture, Vector2 startPos) : base(texture, startPos)
        {
            
        }

        public override void Update(GameTime gameTime, Viewport viewport)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _currKeyState = Keyboard.GetState();

            // Rotation
            if (_currKeyState.IsKeyDown(Keys.A)) _rotation -= _rotationSpeed * deltaTime;
            if (_currKeyState.IsKeyDown(Keys.D)) _rotation += _rotationSpeed * deltaTime;

            // Movement input
            Vector2 direction = new Vector2((float)Math.Cos(_rotation - MathHelper.PiOver2),
                                            (float)Math.Sin(_rotation - MathHelper.PiOver2));

            if (_currKeyState.IsKeyDown(Keys.W))
            {
                _velocity += direction * _thrust * deltaTime;
            }

            // Apply drag
            _velocity -= _velocity * _drag * deltaTime;

            if(_velocity.Length() > _maxSpeed)
            {
                _velocity.Normalize();
                _velocity *= _maxSpeed;
            }

            // Shooting
            if(_currKeyState.IsKeyDown(Keys.Space)&&
               _prevKeyState.IsKeyUp(Keys.Space))
            {
                if(bulletTexture != null)
                {
                    var bulletStartPos = _position + direction * (_radius + 5f);

                    var bullet = new Bullet(bulletTexture, bulletStartPos, _rotation);
                    EntityManager.Add(bullet);
                }
            }

            _prevKeyState = _currKeyState;

            base.Update(gameTime, viewport);
        }
    }
}