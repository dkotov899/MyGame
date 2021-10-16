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
        private LinkLabel _loadGame;
        private LinkLabel _exitGame;

        private float _maxItemWidth = 0f;

        private ContentManager _content;

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

            _content = Game.Content;

            _backgroundImage = new PictureBox(
                _content.Load<Texture2D>("Background/BackgroundMenu"),
                GameRef.ScreenRectangle);

            ControlManager.Add(_backgroundImage);

            var arrowTexture = _content.Load<Texture2D>("GUI/LeftarrowUp");

            _arrowImage = new PictureBox(arrowTexture,
                new Rectangle(0,0,
                    arrowTexture.Width,
                    arrowTexture.Height));

            ControlManager.Add(_arrowImage);

            _startGame = new LinkLabel() 
            { 
                Text = "The story begins",
                Size = new Vector2(100, 20),
            };
            _startGame.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(_startGame);

            _loadGame = new LinkLabel()
            {
                Text = "The story continues",
                Size = new Vector2(100, 20),
            };
            _loadGame.Selected += menuItem_Selected;

            ControlManager.Add(_loadGame);

            _exitGame = new LinkLabel()
            {
                Text = "The story ends",
                Size = new Vector2(100, 20),
            };
            _exitGame.Selected += menuItem_Selected;

            ControlManager.Add(_exitGame);

            var position = new Vector2(350, 500);

            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    if (c.Size.X > _maxItemWidth)
                    {
                        _maxItemWidth = c.Size.X;
                    }

                    c.Position = position;
                    position.Y += c.Size.Y + 5f;
                }
            }

            ControlManager_FocusChanged(_startGame, null);
        }

        void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            var control = sender as Control;

            var position = new Vector2(control.Position.X + _maxItemWidth + 10f,
                control.Position.Y);

            _arrowImage.SetPosition(position);
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == _startGame)
            {
                StateManager.PushState(GameRef.GamePlayScreen);
            }

            if (sender == _loadGame)
            {
                StateManager.PushState(GameRef.GamePlayScreen);
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
