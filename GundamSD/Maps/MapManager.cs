using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace GundamSD.Maps
{
    public class MapManager
    {
        private TmxMap _map;
        private Texture2D _tileSet;

        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;

        public MapManager(TmxMap map, Texture2D tileSet)
        {
            _map = map;
            _tileSet = tileSet;

            tileWidth = _map.Tilesets[0].TileWidth;
            tileHeight = _map.Tilesets[0].TileHeight;

            tilesetTilesWide = _tileSet.Width / tileWidth;
            tilesetTilesHigh = _tileSet.Height / tileHeight;
        }

        public TmxLayer GetTmxLayer(int index)
        {
            return _map.Layers[index];
        }

        public void DrawLayer(SpriteBatch spriteBatch, int layer)
        {
            for (var i = 0; i < _map.Layers[layer].Tiles.Count; i++)
            {
                int gid = _map.Layers[layer].Tiles[i].Gid;
                
                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % _map.Width) * _map.TileWidth;
                    float y = (float)Math.Floor(i / (double)_map.Width) * _map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    spriteBatch.Draw(_tileSet, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                }
            }
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _map.Layers.Count; i++)
            {
                DrawLayer(spriteBatch, i);
            }
        }
    }
}
