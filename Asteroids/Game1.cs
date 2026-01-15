using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Player _player;

    private Texture2D _shipTexture;
    private Texture2D _bulletTexture;
    private Texture2D _asteroidTexture;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;


        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Loading textures
        _shipTexture = Content.Load<Texture2D>("ship-asteroids");
        _bulletTexture = Content.Load<Texture2D>("asteroids-bullet");
        _asteroidTexture = Content.Load<Texture2D>("asteroids-asteroid");

        // Pasing bullet texture to player
        Player.bulletTexture = _bulletTexture;

        // Creating player
        Vector2 center = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                        _graphics.PreferredBackBufferHeight / 2);
        _player = new Player(_shipTexture, center);

        EntityManager.Add(_player);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        EntityManager.Update(gameTime, GraphicsDevice.Viewport);
        // TODO: game state update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        EntityManager.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
