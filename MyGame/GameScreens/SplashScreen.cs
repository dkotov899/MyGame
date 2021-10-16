using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MyGame.Input;
using MyGame.GameStates;

namespace MyGame.GameScreens
{
    public class SplashScreen : BaseGameState
    {
        private Texture2D _backgroundImage;
        private ContentManager _content;
        private SpriteFont _spriteFont;
        private string _text;

        public SplashScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            _text = "Press Enter to continue...";
        }

        protected override void LoadContent()
        {
            _content = GameRef.Content;
            _spriteFont = _content.Load<SpriteFont>("Fonts/ControlFont");
            _backgroundImage = _content.Load<Texture2D>("SplashScreen/SplashScreen");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            if (InputHandler.KeyReleased(Keys.Enter))
            {
                StateManager.PushState(GameRef.StartMenuScreen);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);

            GameRef.SpriteBatch.Draw(
                _backgroundImage,
                GameRef.ScreenRectangle,
                Color.White);

            GameRef.SpriteBatch.DrawString(
                _spriteFont,
                _text,
                new Vector2(1280 / 2 - 150, 680),
                Color.White);

            GameRef.SpriteBatch.End();
        }
    }
}
