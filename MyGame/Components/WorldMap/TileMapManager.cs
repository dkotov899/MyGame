using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TiledSharp;

namespace MyGame.Components.WorldMap
{
    public class TileMapManager
    {
        //All the variables which we will need
        private SpriteBatch spriteBatch;
        TmxMap map;
        Texture2D tileset;
        int tilesetTilesWide;
        int tileWidth;
        int tileHeight;

        public TileMapManager(SpriteBatch _spriteBatch, TmxMap _map, Texture2D _tileset, int _tilesetTilesWide, int _tileWidth, int _tileHeight)
        //Initializing our vairiables
        {
            spriteBatch = _spriteBatch;
            map = _map;
            tileset = _tileset;
            tilesetTilesWide = _tilesetTilesWide;
            tileWidth = _tileWidth;
            tileHeight = _tileHeight;
        }

        public void Draw()//This is where the magic happens :D
        {
            spriteBatch.Begin();//Strating the drawing to the screen
            for (var i = 0; i < map.TileLayers.Count; i++)//This loops through all the tile map layers present on our tile map
            {
                for (var j = 0; j < map.TileLayers[i].Tiles.Count; j++)//this loops through the tiles in each tile layer
                {
                    int gid = map.TileLayers[i].Tiles[j].Gid;//Getting the GID
                    if (gid == 0)
                    {
                        //If empty then do nothing
                    }
                    else//If not empty
                    {//Some complex math to check for the tile position :(
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                        float x = (j % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight;
                        Rectangle tilesetRec = new Rectangle((tileWidth) * column, (tileHeight) * row, tileWidth, tileHeight);//The origin rectangle
                        spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);//Drawing the tile
                    }
                }
            }
            spriteBatch.End();//End of the Draw to screen
        }
    }
}
