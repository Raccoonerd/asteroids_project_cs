using Asteroids;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        private const int _frameWidth = 32;
        private const int _frameHeight = 32;
        private int _currFrame = 0;
        private float _timer = 0;
        private float _animInterval = 0.1f;

        private KeyboardState _prevKeyState;
        private KeyboardState _currKeyState;

        public static Texture2D _bulletTexture;
        public static SoundEffect _shootSound;

        public Player(Texture2D texture, Vector2 startPos) : base(texture, startPos)
        {
            _origin = new Vector2(_frameWidth / 2f, _frameWidth / 2f);
            _radius = (_frameWidth / 2f) * _scale;
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
                // Physics
                _velocity += direction * _thrust * deltaTime;

                // Animation
                _timer += deltaTime;
                if(_timer > _animInterval)
                {
                    _timer = 0;

                    if(_currFrame == 0) _currFrame = 1;
                    else _currFrame = (_currFrame == 1) ? 2 : 1;
                } 
            }
            else
            {
               _currFrame = 0;
               _timer = 0; 
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
                if(_bulletTexture != null)
                {
                    var bulletStartPos = _position + direction * (_radius + 5f);

                    var bullet = new Bullet(_bulletTexture, bulletStartPos, _rotation);
                    EntityManager.Add(bullet);
                    _shootSound.Play();
                }
            }

            _prevKeyState = _currKeyState;

            base.Update(gameTime, viewport);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(
                _currFrame * _frameWidth,
                0,
                _frameWidth,
                _frameHeight
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