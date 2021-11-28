using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyGame.GameStates;
using MyGame.Controls;
using MyGame.GameComponents.World;
using System.Linq;

namespace MyGame.GameScreens
{
    public class GameWinScreen : BaseGameState
    {
        private PictureBox _backgroundImage;
        private PictureBox _arrowImage;

        private LinkLabel _continue;
        private LinkLabel _exit;

        private Label _text;

        private float _maxItemWidth = 0f;

        public GameWinScreen(Game game, GameStateManager manager)
           : base(game, manager)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            var content = _gameRef.Content;

            _backgroundImage = new PictureBox
            (
                content.Load<Texture2D>("Scenes/GameWinScreen"),
                _gameRef.ScreenRectangle
            );

            var arrowTexture = content.Load<Texture2D>("GUI/LeftarrowUp");

            _arrowImage = new PictureBox
                (
                    arrowTexture,
                    new Rectangle
                        (
                            0,
                            0,
                            arrowTexture.Width,
                            arrowTexture.Height
                        )
                );

            _continue = new LinkLabel();
            _continue.Text = "Continue";
            _continue.Size = _continue.SpriteFont.MeasureString(_continue.Text);
            _continue.Selected += menuItem_Selected;

            _exit = new LinkLabel();
            _exit.Text = "Exit";
            _exit.Size = _exit.SpriteFont.MeasureString(_exit.Text);
            _exit.Selected += menuItem_Selected;

            _text = new Label();
            _text.Text = "At the moment, all levels have been completed...";
            _text.Size = _text.SpriteFont.MeasureString(_text.Text);

            _controlManager.AddRange(new List<Control>
            {
                _backgroundImage,
                _arrowImage,
                _continue,
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

            ControlManager_FocusChanged(_continue, null);
        }

        public override void Update(GameTime gameTime)
        {
            _controlManager.Update(gameTime, PlayerIndex.One);

            if (GameLevelManager.GameLevels.Last().Value.LevelData.Status == true)
            {
                _continue.Enabled = false;

                _text.Position = new Vector2
                    (
                        _gameRef.ScreenRectangle.Width / 2 - (_text.Size.X / 2),
                        _gameRef.ScreenRectangle.Height / 2 + 50
                    );

                _controlManager.Add(_text);
                _controlManager.Remove(_continue);

                _exit.HasFocus = true;
                ControlManager_FocusChanged(_exit, null);
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

        private void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            var control = sender as Control;

            _arrowImage.SetPosition(new Vector2(
                control.Position.X + _maxItemWidth + 10f,
                control.Position.Y));
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == _continue)
            {
                GameLevelManager.LoadLevel(GameLevelManager.CurrentLevel.LevelData.Key + 1);
                Transition(ChangeType.Change, _gameRef.GamePlayScreen);
            }

            if (sender == _exit)
            {
                _gameRef.Exit();
            }
        }
    }
}
