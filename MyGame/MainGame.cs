using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyGame.Input;
using MyGame.GameStates;
using MyGame.GameScreens;
using MyGame.GUI.GameScreens;
using MyGame.GameComponents.World.GameLevel;
using MyGame.Audio;

namespace MyGame
{
    public class MainGame : Game
    {
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private GameStateManager _gameStateManager;

        public const int ScreenWidth = 1280;
        public const int ScreenHeight = 720;

        public readonly Rectangle ScreenRectangle =
            new Rectangle(0, 0, ScreenWidth, ScreenHeight);

        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }

        public GameSplashScreen GameSplashScreen { get; private set; }
        public GameStartMenuScreen GameStartMenuScreen { get; private set; }
        public GameRulesCreen GameRulesScreen { get; private set; }
        public GamePlayScreen GamePlayScreen { get; private set; }
        public GameWinScreen GameWinScreen { get; private set; }
        public GameOverScreen GameOverScreen { get; private set; }
        public GameLevelSelection GameLevelSelection { get; private set; }

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = ScreenWidth,
                PreferredBackBufferHeight = ScreenHeight
            };

            _gameStateManager = new GameStateManager(this);

            IsMouseVisible = false;

            Window.Title = "We have to run.";
            Window.IsBorderless = false;
            Window.AllowUserResizing = false;

            Content.RootDirectory = "Content";
             
            Components.Add(new InputHandler(this));
        }

        protected override void Initialize()
        {
            GameSplashScreen = new GameSplashScreen(this, _gameStateManager);
            GameStartMenuScreen = new GameStartMenuScreen(this, _gameStateManager);
            GameRulesScreen = new GameRulesCreen(this, _gameStateManager);
            GamePlayScreen = new GamePlayScreen(this, _gameStateManager);
            GameOverScreen = new GameOverScreen(this, _gameStateManager);
            GameWinScreen = new GameWinScreen(this, _gameStateManager);
            GameLevelSelection = new GameLevelSelection(this, _gameStateManager);

            _gameStateManager.ChangeState(GameSplashScreen);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameSound.Load(Content);
            GameSound.PlayMusic();

            DataLevelManager.ReadLevelData();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(47, 47, 46));

            base.Draw(gameTime);
        }
    }
}
