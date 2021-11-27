using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Components.Players;
using System;
using TiledSharp;

namespace MyGame.Components.WorldMap
{
    public class TileMapManager
    {
        private SpriteBatch _spriteBatch;

        private TmxMap _map;

        private Texture2D _tileset;

        private int _tilesetTilesWide;
        private int _tileWidth;
        private int _tileHeight;

        Player player;

        public TileMapManager(SpriteBatch spriteBatch, TmxMap map, Player player, Texture2D tileset, int tilesetTilesWide, int tileWidth, int tileHeight)
        {
            _spriteBatch = spriteBatch;
            _map = map;
            this.player = player; 
            _tileset = tileset;
            _tilesetTilesWide = tilesetTilesWide;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }

        public void Draw()
        {


            for (var i = 0; i < _map.TileLayers.Count; i++)
            {
                for (var j = 0; j < _map.TileLayers[i].Tiles.Count; j++)
                {
                    int gid = _map.TileLayers[i].Tiles[j].Gid;

                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % _tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)_tilesetTilesWide);
                        float x = (j % _map.Width) * _map.TileWidth;
                        float y = (float)Math.Floor(j / (double)_map.Width) * _map.TileHeight;

                        var tilesetRec = new Rectangle((_tileWidth) * column, (_tileHeight) * row, _tileWidth, _tileHeight);

                        _spriteBatch.Draw(
                            _tileset,
                            new Rectangle((int)x, (int)y, tilesetRec.Width, tilesetRec.Height),
                            tilesetRec, 
                            Color.White);
                    }
                }
            }

     
        }
    }
}
