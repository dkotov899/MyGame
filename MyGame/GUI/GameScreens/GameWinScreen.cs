using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MyGame.Input;
using MyGame.GameStates;

namespace MyGame.GameScreens
{
    public class GameWinScreen : BaseGameState
    {
        private Texture2D _backgroundImage;
        private ContentManager _content;
        private SpriteFont _spriteFont;
        private string _text;

        public GameWinScreen(Game game, GameStateManager manager)
           : base(game, manager)
        {
            _text = "Game win!";
        }

        protected override void LoadContent()
        {
            _content = _gameRef.Content;
            _spriteFont = _content.Load<SpriteFont>("Fonts/ControlFont");
            _backgroundImage = _content.Load<Texture2D>("Background/BackgroundMenu");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _controlManager.Update(gameTime, PlayerIndex.One);

            if (InputHandler.KeyReleased(Keys.Enter))
            {
                StateManager.ChangeState(_gameRef.GameStartMenuScreen);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _gameRef.SpriteBatch.Begin();
            base.Draw(gameTime);

            _gameRef.SpriteBatch.Draw(
                _backgroundImage,
                _gameRef.ScreenRectangle,
                Color.White);

            _gameRef.SpriteBatch.DrawString(
                _spriteFont,
                _text,
                new Vector2(1280 / 2 - 50, 720 / 2),
                Color.White);

            _gameRef.SpriteBatch.End();
        }
    }
}
