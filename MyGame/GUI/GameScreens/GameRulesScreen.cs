using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using MyGame.Input;
using MyGame.Controls;
using MyGame.GameStates;

namespace MyGame.GameScreens
{
    public class GameRulesCreen : BaseGameState
    {
        private PictureBox _backgroundImage;

        private Label _rulesGame;

        public GameRulesCreen(Game game, GameStateManager manager)
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
                content.Load<Texture2D>("Background/BackgroundMenu"),
                _gameRef.ScreenRectangle);

            _rulesGame = new Label();
            _rulesGame.Text = $@"Rules...";
            _rulesGame.Size = _rulesGame.SpriteFont.MeasureString(_rulesGame.Text);
            _rulesGame.Position = new Vector2
            (
                _gameRef.ScreenRectangle.Width  / 2 - (_rulesGame.Size.X / 2),
                _gameRef.ScreenRectangle.Height / 2 - 200
            );

            _controlManager.AddRange(new List<Control>
            {
                _backgroundImage,
                _rulesGame
            });

            _controlManager.NextControl();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _controlManager.Update(gameTime, PlayerIndex.One);

            if (InputHandler.KeyReleased(Keys.Escape))
            {
                Transition(ChangeType.Change, _gameRef.GameStartMenuScreen);
            }

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
