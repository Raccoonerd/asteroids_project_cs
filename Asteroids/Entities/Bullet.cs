using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids
{
    public class Bullet : Entity
    {
        public Bullet(Texture2D texture, Vector2 startPos, float rotation) : base(texture, startPos)
        {}
    }
}