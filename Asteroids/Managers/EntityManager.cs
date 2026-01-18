using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids
{
    public static class EntityManager
    {
        private static List<Entity> _entities = new List<Entity>();

        private static List<Entity> _addedEntities = new List<Entity>();

        public static SoundEffect _explosionSound;

        public static void Add(Entity entity)
        {
            _addedEntities.Add(entity);
        }

        public static void Update(GameTime gameTime, Viewport viewport)
        {
            // Moving entities from added list to main list
            foreach(var entity in _addedEntities)
            {
                _entities.Add(entity);
            }

            _addedEntities.Clear();

            // Updating entities
            foreach(var entity in _entities)
            {
                entity.Update(gameTime, viewport);
            }

            // Checking collisions
            for(int i = 0; i < _entities.Count; i++)
            {
                for(int j = i+1; j < _entities.Count; j++)
                {
                    var e1 = _entities[i];
                    var e2 = _entities[j];

                    if(e1.IsColiding(e2))
                    {
                        HandleCollision(e1, e2);
                    }
                }
            }

            // Removing expired entities
            _entities.RemoveAll(x => x.isExpired);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(var entity in _entities)
            {
                entity.Draw(spriteBatch);
            }
        }

        public static void Clear()
        {
            _entities.Clear();
            _addedEntities.Clear();
        }

        private static void HandleCollision(Entity e1, Entity e2)
        {
            // Bullet hits Asteroid
            if((e1 is Bullet && e2 is Asteroid) || (e2 is Bullet && e1 is Asteroid))
            {
                e1.isExpired = true;
                e2.isExpired = true;
                
                _explosionSound.Play();
                GameManager.AddScore(10);

                Asteroid asteroid = e1 is Asteroid ? (Asteroid)e1 : (Asteroid)e2;
                Asteroid.SpawnExplosion(asteroid);
            }

            // Player hits Asteroid
            if((e1 is Player && e2 is Asteroid) || (e2 is Player && e1 is Asteroid))
            {
                e1.isExpired = true;
                e2.isExpired = true;
            }
        }

        public static int GetAsteroidCount()
        {
            return _entities.Count(x => x is Asteroid);
        }

        public static bool IsPlayerAlive()
        {
            return _entities.Any(x => x is Player);
        }

        public static Player GetPlayer()
        {
            return _entities.OfType<Player>().FirstOrDefault();
        }
    }
}