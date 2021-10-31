using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MyGame.Controls;
using MyGame.GameStates;

namespace MyGame.GameScreens
{
    public class StartMenuScreen : BaseGameState
    {
        private PictureBox _backgroundImage;
        private PictureBox _arrowImage;
        private LinkLabel _startGame;
        private LinkLabel _rulesGame;
        private LinkLabel _exitGame;
        private float _maxItemWidth = 0f;

        public StartMenuScreen(Game game, GameStateManager manager)
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

            ContentManager Content = Game.Content;

            _backgroundImage = new PictureBox(
                Content.Load<Texture2D>("Background/BackgroundMenu"),
                GameRef.ScreenRectangle);

            ControlManager.Add(_backgroundImage);

            Texture2D arrowTexture = Content.Load<Texture2D>("GUI/LeftarrowUp");
            _arrowImage = new PictureBox(
                arrowTexture,
                new Rectangle(
                    0,
                    0,
                    arrowTexture.Width,
                    arrowTexture.Height));

            ControlManager.Add(_arrowImage);

            _startGame = new LinkLabel();
            _startGame.Text = "The story begins";
            _startGame.Size = _startGame.SpriteFont.MeasureString(_startGame.Text);
            _startGame.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(_startGame);

            _rulesGame = new LinkLabel();
            _rulesGame.Text = "The rules story ";
            _rulesGame.Size = _startGame.SpriteFont.MeasureString(_startGame.Text);
            _rulesGame.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(_rulesGame);

            _exitGame = new LinkLabel();
            _exitGame.Text = "The story ends";
            _exitGame.Size = _exitGame.SpriteFont.MeasureString(_exitGame.Text);
            _exitGame.Selected += menuItem_Selected;

            ControlManager.Add(_exitGame);
            ControlManager.NextControl();
            ControlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);

            Vector2 position = new Vector2(1080 / 2, 720 / 2);

            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    if (c.Size.X > _maxItemWidth)
                        _maxItemWidth = c.Size.X;

                    c.Position = position;
                    position.Y += c.Size.Y + 5f;
                }
            }

            ControlManager_FocusChanged(_startGame, null);
        }

        private void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;

            Vector2 position = new Vector2(control.Position.X + _maxItemWidth + 10f,
                control.Position.Y);

            _arrowImage.SetPosition(position);
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == _startGame)
            {
                StateManager.PushState(GameRef.GamePlayScreen);
            }

            if (sender == _rulesGame)
            {
                StateManager.ChangeState(GameRef.GameRulesScreen);
            }

            if (sender == _exitGame)
            {
                GameRef.Exit();
            }
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndexInControl);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            ControlManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }
    }
}
