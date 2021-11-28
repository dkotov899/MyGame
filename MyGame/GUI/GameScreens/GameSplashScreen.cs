using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyGame.Controls;
using MyGame.GameStates;

namespace MyGame.GameScreens
{
    public class GameSplashScreen : BaseGameState
    {
        private PictureBox _backgroundImage;

        private int _currentTime;

        public GameSplashScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            var content = Game.Content;

            _backgroundImage = new PictureBox(
                content.Load<Texture2D>("Scenes/GameSplashScreen"),
                _gameRef.ScreenRectangle);

            _controlManager.Add(_backgroundImage);
            _controlManager.NextControl();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _currentTime += gameTime.TotalGameTime.Seconds;

            if (_currentTime == 7)
            {
                Transition(ChangeType.Change, _gameRef.GameStartMenuScreen);
            }

            _controlManager.Update(gameTime, PlayerIndex.One);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _gameRef.SpriteBatch.Begin();

            _controlManager.Draw(_gameRef.SpriteBatch);

            base.Draw(gameTime);

            _gameRef.SpriteBatch.End();
        }
    }
}
