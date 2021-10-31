using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Components.Players;
using MyGame.Components.WorldMap;
using MyGame.GameStates;
using System.Collections.Generic;
using TiledSharp;

namespace MyGame.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        private Player _player;
        private List<Monster> _monsters;

        private SpriteBatch _spriteBatch;
        private TmxMap _map;
        private TileMapManager _mapManager;
        private List<Rectangle> _collisionObjects;
        private Dictionary<int, Rectangle> _locksObjects;

        private Game _gameRef;

        private Texture2D _rectangleStats;

        private SpriteFont _spriteFont;

        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            _gameRef = game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _map = new TmxMap("Content/World/Levels/LevelOne.tmx");

            _rectangleStats = Game.Content.Load<Texture2D>("GUI/ListBoxImage");

            _spriteFont = Game.Content.Load<SpriteFont>("Fonts/ControlFont");

            var tileset = Game.Content.Load<Texture2D>("World/Tiles/" + _map.Tilesets[0].Name.ToString());
            var tileWidth = _map.Tilesets[0].TileWidth;
            var tileHeight = _map.Tilesets[0].TileHeight;
            var TileSetTilesWide = tileset.Width / tileWidth;

            _mapManager = new TileMapManager(_spriteBatch, _map, tileset, TileSetTilesWide, tileWidth, tileHeight);

            _collisionObjects = new List<Rectangle>();
            _locksObjects = new Dictionary<int, Rectangle>();

            foreach (var o in _map.ObjectGroups["Collisions"].Objects)
            {
                _collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            var key = 0;

            foreach (var o in _map.ObjectGroups["Boxes"].Objects)
            {
                _locksObjects.Add(key, new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
                key++;
            }

            _player = new Player(_gameRef, _collisionObjects, _locksObjects);

            _player.LoadContent();

            _monsters = new List<Monster>()
            {
                new Monster(_gameRef,
                    _player,
                    new Vector2(192, 80),
                    60,
                    MonsterType.Fighter,
                    new MonsterAction[]
                    {
                        MonsterAction.WalkRight,
                        MonsterAction.WalkLeft
                    }),

                new Monster(_gameRef,
                    _player,
                    new Vector2(192, 448),
                    40,
                    MonsterType.Fighter,
                    new MonsterAction[]
                    {
                        MonsterAction.WalkRight,
                        MonsterAction.WalkDown,
                        MonsterAction.WalkUp,
                        MonsterAction.WalkLeft
                    }),

                new Monster(_gameRef,
                    _player,
                    new Vector2(576, 256),
                    40,
                    MonsterType.Wizard,
                    new MonsterAction[]
                    {
                        MonsterAction.WalkDown,
                        MonsterAction.WalkRight,
                        MonsterAction.WalkLeft,
                        MonsterAction.WalkUp
                    }),
            };

            _monsters.ForEach(x => x.LoadContent());

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _monsters.ForEach(x => x.Update(gameTime));

            if (_player.Locks  == 3)
            {
                StateManager.PushState(GameRef.GameWinScreen);
            }

            foreach (var m in _monsters)
            {
                if (m.IsCaught == true)
                {
                    StateManager.PushState(GameRef.GameOverScreen);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                null);

            _mapManager.Draw();

            GameRef.SpriteBatch.Draw
                (
                    _rectangleStats,
                    new Rectangle(960, 0, 280, 640),
                    Color.Gray
                );

            _player.Draw(gameTime);

            _monsters.ForEach(x => x.Draw(gameTime));

            GameRef.SpriteBatch.DrawString(
                _spriteFont,
                $" Statistics:",
                new Vector2(1030, 50),
                Color.White);

            GameRef.SpriteBatch.DrawString(
                _spriteFont,
                $"Locks: {_player.Locks}",
                new Vector2(980, 90),
                Color.White);

            GameRef.SpriteBatch.DrawString(
                _spriteFont,
                $"Time: {gameTime.TotalGameTime.Hours}.{gameTime.TotalGameTime.Minutes}.{gameTime.TotalGameTime.Seconds}:{gameTime.TotalGameTime.Milliseconds}",
                new Vector2(980, 130),
                Color.White);

            base.Draw(gameTime);

            GameRef.SpriteBatch.End();
        }
    }
}