using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyGame.Input;
using MyGame.GameStates;
using MyGame.GameScreens;

namespace MyGame
{
    public class MainGame : Game
    {
        private float fps;
        private float updateInterval = 1.0f;
        private float timeSinceLastUpdate = 0.0f;
        private float frameCount = 0;

        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private GameStateManager _gameStateManager;

        public const int ScreenWidth = 1280;
        public const int ScreenHeight = 720;

        public readonly Rectangle ScreenRectangle =
            new Rectangle(0, 0, ScreenWidth, ScreenHeight);

        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }

        public SplashScreen SplashScreen { get; private set; }

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

            Window.IsBorderless = true;
            Window.Position = new Point(40, 10);

            Content.RootDirectory = "Content";

            Components.Add(new InputHandler(this));

            SplashScreen = new SplashScreen(this, _gameStateManager);
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
            GraphicsDevice.Clear(Color.Black);


            base.Draw(gameTime);
        }
    }
}
