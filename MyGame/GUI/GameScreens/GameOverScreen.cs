using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyGame.GameStates;
using MyGame.Controls;
using MyGame.GameComponents.World;

namespace MyGame.GameScreens
{
    public class GameOverScreen : BaseGameState
    {
        private PictureBox _backgroundImage;
        private PictureBox _arrowImage;

        private LinkLabel _tryAgain;
        private LinkLabel _exit;

        private float _maxItemWidth = 0f;

        public GameOverScreen(Game game, GameStateManager manager)
           : base(game, manager)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            var content = _gameRef.Content;

            _backgroundImage = new PictureBox
            (
                content.Load<Texture2D>("Background/GameOverScreenImage"),
                _gameRef.ScreenRectangle
            );

            var arrowTexture = content.Load<Texture2D>("GUI/LeftarrowUp");

            _arrowImage = new PictureBox(
                arrowTexture,
                new Rectangle(
                    0,
                    0,
                    arrowTexture.Width,
                    arrowTexture.Height));

            _tryAgain = new LinkLabel();
            _tryAgain.Text = "Try again?";
            _tryAgain.Size = _tryAgain.SpriteFont.MeasureString(_tryAgain.Text);
            _tryAgain.Selected += new EventHandler(menuItem_Selected);

            _exit = new LinkLabel();
            _exit.Text = "Exit to main menu";
            _exit.Size = _exit.SpriteFont.MeasureString(_exit.Text);
            _exit.Selected += menuItem_Selected;

            _controlManager.AddRange(new List<Control>
            {
                _backgroundImage,
                _arrowImage,
                _tryAgain,
                _exit
            });

            _controlManager.NextControl();
            _controlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);

            var position = new Vector2(
                _gameRef.ScreenRectangle.Width / 2,
                _gameRef.ScreenRectangle.Height / 2 + 100);

            foreach (var control in _controlManager)
            {
                if (control is LinkLabel)
                {
                    if (control.Size.X > _maxItemWidth)
                    {
                        _maxItemWidth = control.Size.X;
                    }

                    position.Y += control.Size.Y + 5f;

                    control.Position = new Vector2(
                        position.X - (control.Size.X / 2),
                        position.Y);
                }
            }

            ControlManager_FocusChanged(_tryAgain, null);
        }

        public override void Update(GameTime gameTime)
        {
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

        private void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            var control = sender as Control;

            _arrowImage.SetPosition(new Vector2(
                control.Position.X + _maxItemWidth + 10f,
                control.Position.Y));
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == _tryAgain)
            {
                Transition(ChangeType.Change, _gameRef.GamePlayScreen);
            }

            if (sender == _exit)
            {
                _gameRef.Exit();
            }
        }
    }
}
