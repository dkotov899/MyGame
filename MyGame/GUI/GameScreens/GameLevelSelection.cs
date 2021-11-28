using System;
using System.Linq;

using System.Collections.Generic;
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

        private Dictionary<int, LinkLabel> _linkLabels;
        private Dictionary<int, PictureBox> _lockImages;

        private Texture2D _arrowTexture;
        private Texture2D _lockTexture;

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

            _backgroundImage = new PictureBox(
                content.Load<Texture2D>("Background/BackgroundMenu"),
                _gameRef.ScreenRectangle);

            _controlManager.Add(_backgroundImage);

            _arrowTexture = content.Load<Texture2D>("GUI/LeftarrowUp");
            _lockTexture = content.Load<Texture2D>("GUI/LockImage");

            CreateLevelMenu();

            _controlManager.NextControl();
            _controlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);        

            ControlManager_FocusChanged(_linkLabels.First().Value, null);
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

            RefreshLevelMenu();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _gameRef.SpriteBatch.Begin();

            _controlManager.Draw(_gameRef.SpriteBatch);

            base.Draw(gameTime);

            _gameRef.SpriteBatch.End();
        }

        private void CreateLevelMenu()
        {
            GameLevelManager.CreateLevels();

            _linkLabels = new Dictionary<int, LinkLabel>();
            _lockImages = new Dictionary<int, PictureBox>();

            _arrowImage = new PictureBox
                (
                    _arrowTexture,
                    new Rectangle(
                        0,
                        0,
                        _arrowTexture.Width,
                        _arrowTexture.Height)
                );            

            var position = new Vector2
                (
                    _gameRef.ScreenRectangle.Width / 2,
                    _gameRef.ScreenRectangle.Height / 2 - 200
                );

            foreach (var item in DataLevelManager.LevelsData)
            {
                var linkLabel = new LinkLabel();

                linkLabel.Text = item.Value.LevelName;
                linkLabel.Size = linkLabel.SpriteFont.MeasureString(linkLabel.Text);
                linkLabel.Selected += new EventHandler(menuItem_Selected);

                if (linkLabel.Size.X > _maxItemWidth)
                {
                    _maxItemWidth = linkLabel.Size.X;
                }

                position.Y += linkLabel.Size.Y + 5f;

                linkLabel.Position = new Vector2(
                        position.X - (linkLabel.Size.X / 2),
                        position.Y);

                if (item.Key - 1 != 0)
                {
                    if (GameLevelManager.GameLevels[item.Key - 1].LevelData.Status == false)
                    {
                        linkLabel.Enabled = false;

                        var lockImage = new PictureBox
                            (
                                _lockTexture,
                                new Rectangle
                                (
                                    0,
                                    0,
                                    _lockTexture.Width,
                                    _lockTexture.Height
                                )
                            );

                        lockImage.SetPosition
                            (   new Vector2
                                (
                                    linkLabel.Position.X + _maxItemWidth + 10f,
                                    linkLabel.Position.Y
                                )
                            );

                        _lockImages.Add(item.Key, lockImage);
                    }
                }

                _linkLabels.Add(item.Key, linkLabel);
            }

            _controlManager.Add(_arrowImage);
            _controlManager.AddRange(_linkLabels.Values);
            _controlManager.AddRange(_lockImages.Values);
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

            var level = GameLevelManager.GameLevels.First(n => n.Value.LevelData.LevelName == selectedLabel.Text).Value;

            if (level.LevelData.Key == 1 ||
                GameLevelManager.GameLevels[level.LevelData.Key - 1].LevelData.Status == true)
            {
                GameLevelManager.LoadLevel(level.LevelData.Key);

                Transition(ChangeType.Change, _gameRef.GamePlayScreen);
            }
        }

        private void RefreshLevelMenu()
        {
            if (_linkLabels == null)
            {
                return;
            }

            foreach (var level in GameLevelManager.GameLevels)
            {
                if (level.Key - 1 != 0)
                {
                    if (GameLevelManager.GameLevels[level.Key - 1].LevelData.Status == true)
                    {
                        if (_linkLabels[level.Key].Enabled == false)
                        {
                            _linkLabels[level.Key].Enabled = true;
                         
                            if (_lockImages.ContainsKey(level.Key))
                            {
                                _controlManager.Remove(_lockImages[level.Key]);
                                _lockImages.Remove(level.Key);
                            }
                        }
                    }
                }
            }
        }
    }
}
