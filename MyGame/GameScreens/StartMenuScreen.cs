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
        PictureBox backgroundImage;
        PictureBox arrowImage;
        LinkLabel startGame;
        LinkLabel loadGame;
        LinkLabel exitGame;
        float maxItemWidth = 0f;

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

            backgroundImage = new PictureBox(
                Content.Load<Texture2D>("Background/BackgroundMenu"),
                GameRef.ScreenRectangle);

            ControlManager.Add(backgroundImage);

            Texture2D arrowTexture = Content.Load<Texture2D>("GUI/LeftarrowUp");
            arrowImage = new PictureBox(
                arrowTexture,
                new Rectangle(
                    0,
                    0,
                    arrowTexture.Width,
                    arrowTexture.Height));

            ControlManager.Add(arrowImage);

            startGame = new LinkLabel();
            startGame.Text = "The story begins";
            startGame.Size = startGame.SpriteFont.MeasureString(startGame.Text);
            startGame.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(startGame);

            loadGame = new LinkLabel();
            loadGame.Text = "The story continues";
            loadGame.Size = loadGame.SpriteFont.MeasureString(loadGame.Text);
            loadGame.Selected += menuItem_Selected;
            ControlManager.Add(loadGame);

            exitGame = new LinkLabel();
            exitGame.Text = "The story ends";
            exitGame.Size = exitGame.SpriteFont.MeasureString(exitGame.Text);
            exitGame.Selected += menuItem_Selected;

            ControlManager.Add(exitGame);
            ControlManager.NextControl();
            ControlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);

            Vector2 position = new Vector2(1080 / 2, 720 / 2);

            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    if (c.Size.X > maxItemWidth)
                        maxItemWidth = c.Size.X;

                    c.Position = position;
                    position.Y += c.Size.Y + 5f;
                }
            }

            ControlManager_FocusChanged(startGame, null);
        }

        private void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;

            Vector2 position = new Vector2(control.Position.X + maxItemWidth + 10f,
                control.Position.Y);

            arrowImage.SetPosition(position);
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == startGame)
            {
                StateManager.PushState(GameRef.GamePlayScreen);
            }

            if (sender == loadGame)
            {
                StateManager.PushState(GameRef.GamePlayScreen);
            }

            if (sender == exitGame)
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
