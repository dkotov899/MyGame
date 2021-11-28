using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Audio;
using MyGame.Controls;
using MyGame.GameStates;

namespace MyGame.GameScreens
{
    public class GameStartMenuScreen : BaseGameState
    {
        private PictureBox _backgroundImage;
        private PictureBox _arrowImage;

        private Label _copyrightGame;

        private LinkLabel _startGame;
        private LinkLabel _rulesGame;
        private LinkLabel _exitGame;

        private float _maxItemWidth = 0f;

        public GameStartMenuScreen(Game game, GameStateManager manager)
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

            GameSound.PlayMusic();

            _backgroundImage = new PictureBox(
                content.Load<Texture2D>("Background/BackgroundMenu"),
                _gameRef.ScreenRectangle);

            var arrowTexture = content.Load<Texture2D>("GUI/LeftarrowUp");

            _arrowImage = new PictureBox(
                arrowTexture,
                new Rectangle(
                    0,
                    0,
                    arrowTexture.Width,
                    arrowTexture.Height));

            _startGame = new LinkLabel();
            _startGame.Text = "Start";
            _startGame.Size = _startGame.SpriteFont.MeasureString(_startGame.Text);
            _startGame.Selected += new EventHandler(menuItem_Selected);

            _rulesGame = new LinkLabel();
            _rulesGame.Text = "Rules";
            _rulesGame.Size = _startGame.SpriteFont.MeasureString(_startGame.Text);
            _rulesGame.Selected += new EventHandler(menuItem_Selected);

            _exitGame = new LinkLabel();
            _exitGame.Text = "Escape";
            _exitGame.Size = _exitGame.SpriteFont.MeasureString(_exitGame.Text);
            _exitGame.Selected += menuItem_Selected;

            _copyrightGame = new Label();
            _copyrightGame.Text = "@Copyright, Dkotov899, 2021.";
            _copyrightGame.Size = _exitGame.SpriteFont.MeasureString(_copyrightGame.Text);

            _controlManager.AddRange(new List<Control>
            {
                    _backgroundImage,
                    _arrowImage,
                    _startGame,
                    _rulesGame,
                    _exitGame,
                    _copyrightGame
            });

            _controlManager.NextControl();
            _controlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);

            var position = new Vector2(
                _gameRef.ScreenRectangle.Width / 2,
                _gameRef.ScreenRectangle.Height / 2);

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
                        position.Y - 50);
                }

                if (control is Label)
                {
                    control.Position = new Vector2(
                        position.X - (control.Size.X / 2),
                        _gameRef.ScreenRectangle.Height - 50);
                }
            }

            ControlManager_FocusChanged(_startGame, null);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _controlManager.Update(gameTime, _playerIndexInControl);
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
            if (sender == _startGame)
            {
                Transition(ChangeType.Change, _gameRef.GameLevelSelection);
            }

            if (sender == _rulesGame)
            {
                Transition(ChangeType.Change, _gameRef.GameRulesScreen);
            }

            if (sender == _exitGame)
            {
                _gameRef.Exit();
            }
        }
    }
}
