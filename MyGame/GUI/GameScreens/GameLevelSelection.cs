using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using MyGame.Input;
using MyGame.Controls;
using MyGame.GameStates;
using MyGame.GameComponents.World;
using MyGame.GameComponents.World.GameLevel;

namespace MyGame.GUI.GameScreens
{
    public class GameLevelSelection : BaseGameState
    {
        private PictureBox _backgroundImage;
        private PictureBox _arrowImage;

        private List<LinkLabel> _levels;

        private float _maxItemWidth = 0f;

        public GameLevelSelection(Game game, GameStateManager manager)
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

            GameLevelManager.GameRef = _gameRef;
            GameLevelManager.CreateLevels();

            _levels = new List<LinkLabel>();

            _backgroundImage = new PictureBox(
                content.Load<Texture2D>("Background/BackgroundMenu"),
                _gameRef.ScreenRectangle);

            _controlManager.Add(_backgroundImage);

            var arrowTexture = content.Load<Texture2D>("GUI/LeftarrowUp");

            _arrowImage = new PictureBox(
                arrowTexture,
                new Rectangle(
                    0,
                    0,
                    arrowTexture.Width,
                    arrowTexture.Height));

            _controlManager.Add(_arrowImage);

            foreach (var item in DataLevelManager.LevelsData)
            {
                var linkLabel = new LinkLabel();

                linkLabel.Text = item.Value.LevelName;
                linkLabel.Size = linkLabel.SpriteFont.MeasureString(linkLabel.Text);
                linkLabel.Selected += new EventHandler(menuItem_Selected);

                _levels.Add(linkLabel);
            }

            _controlManager.AddRange(_levels);

            _controlManager.NextControl();
            _controlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);

            var position = new Vector2
                (
                    _gameRef.ScreenRectangle.Width / 2,
                    _gameRef.ScreenRectangle.Height / 2
                );

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
                        position.Y - 250);
                }
            }

            ControlManager_FocusChanged(_levels.First(), null);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyReleased(Keys.Escape))
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

        private void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            var control = sender as Control;

            _arrowImage.SetPosition(new Vector2(
                control.Position.X + _maxItemWidth + 10f,
                control.Position.Y));
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            var selectedLabel = (LinkLabel)sender;

            GameLevelManager.CurrentLevel = GameLevelManager.GameLevels.First(n => n.Value.LevelData.LevelName == selectedLabel.Text).Value;

            var test = GameLevelManager.GameLevels.First(n => n.Value.LevelData.LevelName == selectedLabel.Text).Value;

            Transition(ChangeType.Change, _gameRef.GamePlayScreen);
        }
    }
}
