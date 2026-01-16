using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids
{
    public static class GameManager
    {
        public static int _level = 0;
        public static float _respawnTimer = 0;

        private static Texture2D _asteroidTexture;
        private static Vector2 _screenSize;

        public static void Initialize(Texture2D asteroidTexture, Vector2 screenSize)
        {
            _asteroidTexture = asteroidTexture;
            _screenSize = screenSize;
            _level = 0;
        }

        public static void Update(GameTime gameTime)
        {
            int count = EntityManager.GetAsteroidCount();

            if(count == 0)
            {
                _respawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if(_respawnTimer <= 0f)
                {
                    _level++;
                    SpawnWave(_level);
                    _respawnTimer = 3f;
                }
            }
        }

        private static void SpawnWave(int level)
        {
            Random rand = new Random();
            
            int asteroidCount = 3 + level;

            for (int i = 0; i < asteroidCount; i++)
            {
                Vector2 pos = new Vector2(
                    rand.Next(0, (int)_screenSize.X),
                    rand.Next(0, (int)_screenSize.Y)
                );

                Vector2 center = _screenSize / 2f;

                if(Vector2.Distance(pos, center) < 100f)
                {
                    pos.X = (pos.X + 200) %  _screenSize.X;
                }

                float speed = 1.0f + (level * 0.1f);

                Vector2 velocity = new Vector2(
                    (float)(rand.NextDouble() * 2 - 1),
                    (float)(rand.NextDouble() * 2 - 1)
                );
                velocity.Normalize();
                velocity *= rand.Next(50, 150) * speed;

                var asteroid = new Asteroid(_asteroidTexture, pos, velocity);
                EntityManager.Add(asteroid);
            }
        }
    }
}