using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyGame.Input;
using MyGame.GameStates;
using MyGame.GameScreens;
using MyGame.Audio;
using Microsoft.Xna.Framework.Media;

namespace MyGame
{
    public class MainGame : Game
    {
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private GameStateManager _gameStateManager;

        public const int ScreenWidth = 1240;
        public const int ScreenHeight = 640;

        public readonly Rectangle ScreenRectangle =
            new Rectangle(0, 0, ScreenWidth, ScreenHeight);

        public Sound Sound { get; private set; } = new Sound();

        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }

        public SplashScreen SplashScreen { get; private set; }
        public StartMenuScreen StartMenuScreen { get; private set; }
        public GamePlayScreen GamePlayScreen { get; private set; }
        public GameWinScreen GameWinScreen { get; private set; }
        public GameOverScreen GameOverScreen { get; private set; }
        public GameRulesCreen GameRulesScreen { get; private set; }

        //------------------
        // C O N S T R U C T
        //------------------
        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = ScreenWidth,
                PreferredBackBufferHeight = ScreenHeight,
                IsFullScreen = false,              
            };

            _gameStateManager = new GameStateManager(this);

            Window.Title = "GetGoGold";
            Window.IsBorderless = false;
            Window.Position = new Point(40, 10);
            Window.AllowUserResizing = false;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";

            Components.Add(new InputHandler(this));

            SplashScreen = new SplashScreen(this, _gameStateManager);
            StartMenuScreen = new StartMenuScreen(this, _gameStateManager);
            GamePlayScreen = new GamePlayScreen(this, _gameStateManager);
            GameWinScreen = new GameWinScreen(this, _gameStateManager);
            GameOverScreen = new GameOverScreen(this, _gameStateManager);
            GameRulesScreen = new GameRulesCreen(this, _gameStateManager);

            _gameStateManager.ChangeState(SplashScreen);
        }

        //--------
        // I N I T
        //--------
        protected override void Initialize()
        {
            base.Initialize();
        }

        //--------
        // L O A D
        //--------
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //Sound.LoadContent(Content);
        }

        //--------
        // U N L O A D
        //--------
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        //------------
        // U P D A T E
        //------------
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        //--------
        // D R A W
        //--------
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            base.Draw(gameTime);
        }
    }
}
