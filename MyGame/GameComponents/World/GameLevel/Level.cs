using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TiledSharp;

using MyGame.Components.Players;
using MyGame.Components.WorldMap;
using System.Linq;

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
        private Rectangle _playerEndPoint;
        private Rectangle _playerStartPoint;
        private int _currentPlayerKeys = 0;

        private Dictionary<int, Monster> _monsters;
        private Dictionary<int, Rectangle> _monstersRoutes;
        private Dictionary<int, Rectangle> _monstersStartPoint;

        private List<Rectangle> _collisions;
        private Dictionary<int, Rectangle> _keys;

        public LevelData LevelData
        {
            get { return _levelData; }
        }

        public LevelState LevelState
        {
            get { return _levelState; }
        }

        public TmxMap Map
        {
            get { return _map; }
        }

        public TileMapManager MapManager
        {
            get { return _mapManager; }
        }

        public Player Player
        {
            get { return _player; }
        }

        public Rectangle PlayerEndPoin
        {
            get { return _playerEndPoint; }
        }

        public Rectangle PlayerStartPoint
        {
            get { return _playerStartPoint; }
        }

        public Dictionary<int, Monster> Monsters
        {
            get { return _monsters; }
        }

        public Dictionary<int, Rectangle> MonstersRoutes
        {
            get { return _monstersRoutes; }
        }

        public Dictionary<int, Rectangle> MonstersStartPoint
        {
            get { return _monstersStartPoint; }
        }

        public List<Rectangle> Collisions
        {
            get { return _collisions; }
        }

        public Dictionary<int, Rectangle> Keys
        {
            get { return _keys; }
        }

        public Level(Game game, LevelData levelData)
        {
            _gameRef = (MainGame)game;
            _levelData = levelData;
        }

        public void Initialize()
        {
            _levelState = LevelState.Active;
        }

        public void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_gameRef.GraphicsDevice);

            CreateMap();
            CreateObjects();
            CreatePlayer();
            CreateMonsters();
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (_player.IsAlive == true)
            {
                _player.Update(gameTime);

                if (_player.CollectedKeys.Count > _currentPlayerKeys)
                {
                    foreach (var key in _player.CollectedKeys)
                    {
                        _mapManager.ResetGidTile(key.Value);
                    }

                    _currentPlayerKeys += 1;
                }

                if (_player.IsKeysCollectedSuccess == true)
                {
                    _levelState = LevelState.GameWin;
                }
            }
            else
            {
                _levelState = LevelState.GameOver;
            }

            foreach (var monster in _monsters.Values)
            {
                monster.Update(gameTime);
            }
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

            foreach (var monster in _monsters.Values)
            {
                monster.Draw(gameTime, _spriteBatch);
            }

            _player.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }

        public void Reset()
        {
            Initialize();
            LoadContent();
        }

        private void CreateMap()
        {
            _map = new TmxMap($@"Content/World/Levels/{_levelData.LevelName}.tmx");

            var tileset = _gameRef.Content.Load<Texture2D>("World/Tiles/" + _map.Tilesets[0].Name.ToString());
            var tileWidth = _map.Tilesets[0].TileWidth;
            var tileHeight = _map.Tilesets[0].TileHeight;
            var TileSetTilesWide = tileset.Width / tileWidth;

            _mapManager = new TileMapManager(_spriteBatch, _map, tileset, TileSetTilesWide, tileWidth, tileHeight);
        }

        private void CreateObjects()
        {
            _collisions = new List<Rectangle>();
            _keys = new Dictionary<int, Rectangle>();

            foreach (var obj in _map.ObjectGroups["Collisions"].Objects)
            {
                _collisions.Add
                    (
                        new Rectangle((int)obj.X, (int)obj.Y, (int)obj.Width, (int)obj.Height)
                    );
            }

            foreach (var obj in _map.ObjectGroups["ObjectCollisions"].Objects)
            {
                _keys.Add
                    (
                        obj.Id,
                        new Rectangle((int)obj.X, (int)obj.Y, (int)obj.Width, (int)obj.Height)
                    );
            }
        }

        private void CreatePlayer()
        {
            var playerEndPoint = _map.ObjectGroups["ExitPoint"].Objects.First();
            var playerStartPoint = _map.ObjectGroups["StartPoint"].Objects.First();

            _playerEndPoint = new Rectangle(
                (int)playerEndPoint.X,
                (int)playerEndPoint.Y,
                (int)playerEndPoint.Width,
                (int)playerEndPoint.Height);

            _playerStartPoint = new Rectangle(
                (int)playerStartPoint.X,
                (int)playerStartPoint.Y,
                (int)playerStartPoint.Width,
                (int)playerStartPoint.Height);

            _player = new Player(_gameRef, this);

            _player.LoadContent();
        }

        private void CreateMonsters()
        {
            _monsters = new Dictionary<int, Monster>();
            _monstersRoutes = new Dictionary<int, Rectangle>();
            _monstersStartPoint = new Dictionary<int, Rectangle>();

            foreach (var point in _map.ObjectGroups["MonsterPoints"].Objects)
            {
                foreach (var route in _map.ObjectGroups["MonsterRoutes"].Objects)
                {
                    if (new Rectangle((int)point.X, (int)point.Y, (int)point.Width, (int)point.Height).Intersects
                        (new Rectangle((int)route.X, (int)route.Y, (int)route.Width, (int)route.Height)) == true)
                    {
                        _monsters.Add(point.Id, new Monster(_gameRef, this, point.Id));
                        _monstersRoutes.Add(point.Id, new Rectangle((int)route.X, (int)route.Y, (int)route.Width, (int)route.Height));
                        _monstersStartPoint.Add(point.Id, new Rectangle((int)point.X, (int)point.Y, (int)point.Width, (int)point.Height));
                    }
                }
            }

            foreach (var monster in _monsters.Values)
            {
                monster.LoadContent();
            }
        }
    }
}
