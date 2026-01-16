using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Asteroids;

public enum GameState
{
    Menu,
    Playing,
    GameOver
}

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private GameState _currGameState = GameState.Menu;

    private Player _player;
    private SpriteFont _font;

    private Texture2D _shipTexture;
    private Texture2D _bulletTexture;
    private Texture2D _asteroidTexture;

    private Texture2D _backgroundTexture;


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

        try{
        // Loading textures
        _font = Content.Load<SpriteFont>("font");
        _shipTexture = Content.Load<Texture2D>("ship-asteroids");
        _bulletTexture = Content.Load<Texture2D>("asteroids-bullet");
        _asteroidTexture = Content.Load<Texture2D>("asteroids-asteroid");
        _backgroundTexture = Content.Load<Texture2D>("asteroids-background");
        }
        catch
        {
            System.Console.WriteLine("Error loading assets.");
            throw;
        }
        // Pasing bullet texture to player
        Player.bulletTexture = _bulletTexture;

        GameManager.Initialize
        (
            _asteroidTexture, 
            new Vector2(_graphics.PreferredBackBufferWidth, 
                        _graphics.PreferredBackBufferHeight
                        )
        );
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        switch(_currGameState)
        {
            case GameState.Menu:
                if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    StartGame();
                }
                break;
            case GameState.Playing:
                EntityManager.Update(gameTime, GraphicsDevice.Viewport);
                GameManager.Update(gameTime);

                if(!EntityManager.IsPlayerAlive())
                {
                    _currGameState = GameState.GameOver;
                }
                break;
            case GameState.GameOver:
                if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    StartGame();
                }
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Rectangle screenRect = new Rectangle(
            0, 0, 
            _graphics.PreferredBackBufferWidth, 
            _graphics.PreferredBackBufferHeight
        );

        if(_backgroundTexture != null)
        {
            _spriteBatch.Draw(
                _backgroundTexture, 
                screenRect, 
                Color.White
            );
        }

        Vector2 center = new Vector2(
            _graphics.PreferredBackBufferWidth / 2f,
            _graphics.PreferredBackBufferHeight / 2f
        );
        
        switch(_currGameState)
        {
            case GameState.Menu:
                string menuText = "ASTEROIDS\n\nPress ENTER to Start";
                Vector2 menuSize = _font.MeasureString(menuText);
                _spriteBatch.DrawString(
                    _font,
                    menuText,
                    center - menuSize / 2f,
                    Color.White
                );
                break;
            case GameState.Playing:
                EntityManager.Draw(_spriteBatch);
                break;
            case GameState.GameOver:
                string gameOverText = "GAME OVER\n\nPress ENTER to Restart";
                Vector2 gameOverSize = _font.MeasureString(gameOverText);
                _spriteBatch.DrawString(
                    _font,
                    gameOverText,
                    center - gameOverSize / 2f,
                    Color.White
                );
                break;
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void StartGame()
    {
        GameManager.Reset();

        Vector2 center = new Vector2(
            _graphics.PreferredBackBufferWidth / 2f,
            _graphics.PreferredBackBufferHeight / 2f
        );

        _player = new Player(_shipTexture, center);
        EntityManager.Add(_player);

        _currGameState = GameState.Playing;
    }
}
