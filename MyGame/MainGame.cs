using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    internal class MainGame : Game
    {
        // GAME STATES:
        private enum GameState { Intro, Menu, Play }
        private GameState _gameState = GameState.Menu;

        // DISPLAY                  
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //------------------
        // C O N S T R U C T
        //------------------
        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X,
                PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y,
                IsFullScreen = false,
                PreferredDepthStencilFormat = DepthFormat.Depth16,
                GraphicsProfile = GraphicsProfile.HiDef
            };

            Window.IsBorderless = false;
            //Window.Position = new Point(0, 0);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            ScreenManager.Instance.LoadContent(Content);
        }

        //--------
        // U N L O A D
        //--------
        protected override void UnloadContent()
        {
            base.UnloadContent();

            ScreenManager.Instance.UnloadContent();
        }

        //------------
        // U P D A T E
        //------------
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ScreenManager.Instance.Update(gameTime);
        }

        //--------
        // D R A W
        //--------
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            ScreenManager.Instance.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
