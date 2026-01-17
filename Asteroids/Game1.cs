using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    // Graphics and rendering
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Game state management
    private GameState _currGameState = GameState.Menu;
    private KeyboardState _prevKState;
    private KeyboardState _currKState;

    // Player entity
    private Player _player;

    // Assets
    private SpriteFont _font;

    private Texture2D _shipTexture;
    private Texture2D _titleTexture;
    private Texture2D _bulletTexture;
    private Texture2D _asteroidTexture;
    private Texture2D _gameOverTexture;
    private Texture2D _backgroundTexture;

    private SoundEffect _shootSound;


    public Game1()
    {
        // Initialize graphics device
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // Set fixed time step for consistent updates
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
    }

    protected override void Initialize()
    {
        // Set preferred window size
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
        _titleTexture = Content.Load<Texture2D>("asteroids-title");
        _bulletTexture = Content.Load<Texture2D>("asteroids-bullet");
        _gameOverTexture = Content.Load<Texture2D>("asteroids-gameover");
        _asteroidTexture = Content.Load<Texture2D>("asteroids-asteroid");
        _backgroundTexture = Content.Load<Texture2D>("asteroids-background");

        // Loading sounds
        _shootSound = Content.Load<SoundEffect>("asteroids-shoot");
        }
        catch
        {
            System.Console.WriteLine("Error loading assets.");
            throw;
        }
        // Pasing bullet texture to player
        Player._bulletTexture = this._bulletTexture;
        Player._shootSound = this._shootSound;

        GameManager.Initialize
        (
            _asteroidTexture, 
            new Vector2(_graphics.PreferredBackBufferWidth, 
                        _graphics.PreferredBackBufferHeight
                        )
        );

        UIManager.Initialize(_font, GraphicsDevice.Viewport, _titleTexture, _gameOverTexture);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _currKState = Keyboard.GetState();

        // Game state management
        switch(_currGameState)
        {
            case GameState.Menu:
                int menuChoice = UIManager.UpdateMenu(_currKState, _prevKState);
                if(menuChoice == 0)
                {
                    StartGame();
                }
                else if(menuChoice == 1)
                {
                    Environment.Exit(0);
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
                if(_prevKState.IsKeyUp(Keys.Enter) &&
                    _currKState.IsKeyDown(Keys.Enter))
                {
                    _currGameState = GameState.Menu;
                }
                break;
        }

        _prevKState = _currKState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // Draw background
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

        // Center point for UI
        Vector2 center = new Vector2(
            _graphics.PreferredBackBufferWidth / 2f,
            _graphics.PreferredBackBufferHeight / 2f
        );
        
        // Game state drawing
        switch(_currGameState)
        {
            case GameState.Menu:
                UIManager.DrawMenu(_spriteBatch);
                break;
            case GameState.Playing:
                EntityManager.Draw(_spriteBatch);
                UIManager.DrawGameplay(_spriteBatch);
                break;
            case GameState.GameOver:
                UIManager.DrawGameOver(_spriteBatch);
                break;
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void StartGame()
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
