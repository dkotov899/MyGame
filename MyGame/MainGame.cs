using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public const int ScreenWidth = 1280;
        public const int ScreenHeight = 720;

        public readonly Rectangle ScreenRectangle =
            new Rectangle(0, 0, ScreenWidth, ScreenHeight);

        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }

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

            Content.RootDirectory = "Content";

            Window.IsBorderless = true;
            Window.Position = new Point(40, 10);
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
