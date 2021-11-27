using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TiledSharp;

using MyGame.Components.Players;
using MyGame.Components.WorldMap;

namespace MyGame.GameComponents.World
{
    public enum LevelState
    {
        Active,
        GameWin,
        GameOver,
    }

    public class Level
    {
        private MainGame _gameRef;
        private SpriteBatch _spriteBatch;

        private LevelData _levelData;
        private LevelState _levelState;

        private TmxMap _map;
        private TileMapManager _mapManager;
        private Player _player;
        private List<Monster> _monsters;
        private List<Rectangle> _collisions;

        public LevelData LevelData
        {
            get { return _levelData; }
        }

        public LevelState LevelState
        {
            get { return _levelState; }
        }

        public Player Player
        {
            get { return _player; }
        }

        public Level(Game game, LevelData levelData)
        {
            _gameRef = (MainGame)game;
            _levelData = levelData;
        }

        public void Initialize()
        {

        }

        public void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_gameRef.GraphicsDevice);
            _levelState = LevelState.Active;

            // Map
            _map = new TmxMap($@"Content/World/Levels/{_levelData.LevelName}.tmx");

            var tileset = _gameRef.Content.Load<Texture2D>("World/Tiles/" + _map.Tilesets[0].Name.ToString());
            var tileWidth = _map.Tilesets[0].TileWidth;
            var tileHeight = _map.Tilesets[0].TileHeight;
            var TileSetTilesWide = tileset.Width / tileWidth;

            _mapManager = new TileMapManager(_spriteBatch, _map, _player, tileset, TileSetTilesWide, tileWidth, tileHeight);

            // Objects
            _collisions = new List<Rectangle>();

            foreach (var o in _map.ObjectGroups["Collisions"].Objects)
            {
                _collisions.Add
                    (
                        new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height)
                    );
            }

            // Player
            _player = new Player
                (
                    _gameRef,
                    new Vector2
                    (
                        (float)_map.ObjectGroups["StartPoint"].Objects[0].X,
                        (float)_map.ObjectGroups["StartPoint"].Objects[0].Y
                    ),
                    _collisions
                );

            _player.LoadContent();

            // Monsters
            _monsters = new List<Monster>();

            foreach (var point in _map.ObjectGroups["MonsterPoints"].Objects)
            {
                foreach (var route in _map.ObjectGroups["MonsterRoutes"].Objects)
                {
                    if (new Rectangle((int)point.X, (int)point.Y, (int)point.Width, (int)point.Height).Intersects(
                        new Rectangle((int)route.X, (int)route.Y, (int)route.Width, (int)route.Height)) == true)
                    {
                        _monsters.Add
                            (
                                new Monster
                                (
                                    _gameRef,
                                    _player,
                                    new Vector2((float)point.X, (float)point.Y),
                                    new Rectangle((int)route.X, (int)route.Y, (int)route.Width, (int)route.Height)
                                )
                            );
                    }
                }
            }

            _monsters.ForEach(m => m.LoadContent());
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (_player.IsAlive == true)
            {
                _player.Update(gameTime);
            }
            else
            {
                _levelState = LevelState.GameOver;
            }

            _monsters.ForEach(m => m.Update(gameTime));
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin
            (
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                _player.Camera.Transformation
            );

            _mapManager.Draw();

            _monsters.ForEach(m => m.Draw(gameTime, _spriteBatch));

            _player.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }

        public void Reset()
        {
            _player = new Player
                (
                    _gameRef,
                    new Vector2
                    (
                        (float)_map.ObjectGroups["StartPoint"].Objects[0].X,
                        (float)_map.ObjectGroups["StartPoint"].Objects[0].Y
                    ),
                    _collisions
                );

            _player.LoadContent();

            _levelState = LevelState.Active;
        }
    }
}
