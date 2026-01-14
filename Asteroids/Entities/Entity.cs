using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public abstract class Entity
    {
        public Vector2 _position;
        public Vector2 _velocity;
        public float _rotation;

        public bool isExpired;

        protected Texture2D _texture;
        protected Vector2 _origin;

        public float _radius;

        public Entity(Texture2D texture, Vector2 startPos)
        {
            _texture = texture;
            _position = startPos;
            _velocity = Vector2.Zero;
            _rotation = 0f;
            isExpired = false;

            if(_texture != null)
            {
                _origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
                _radius = (_texture.Width/2f);
            }
        }

        public virtual void Update(GameTime gameTime, Viewport viewport)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _position += _velocity * deltaTime;

            float margin = _radius;

            if(_position.X > viewport.Width + margin)
            {
                _position.X = -margin;
            }
            else if(_position.X < -margin)
            {
                _position.X = viewport.Width + margin;
            }


            if (_position.Y > viewport.Height + margin)
            { 
                _position.Y = -margin; 
            }
            else if (_position.Y < -margin) 
            {
                _position.Y = viewport.Height + margin;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _position,
                null,
                Color.White,
                _rotation,
                _origin,
                Vector2.One,
                SpriteEffects.None,
                0f
            );
        }

        public bool IsColiding(Entity other)
        {
            float distance = Vector2.Distance(this._position, other._position);
            return distance < (this._radius + other._radius);
        }
    }
}