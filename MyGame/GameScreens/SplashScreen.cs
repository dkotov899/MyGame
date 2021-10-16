using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MyGame.GameStates;

namespace MyGame.GameScreens
{
    public class SplashScreen : BaseGameState
    {
        private Texture2D _backgroundImage;
        private ContentManager _content;

        public SplashScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            _content = GameRef.Content;
        }

        protected override void LoadContent()
        {
            _backgroundImage = _content.Load<Texture2D>("SplashScreen/SplashScreen");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
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

            GameRef.SpriteBatch.End();
        }
    }
}
