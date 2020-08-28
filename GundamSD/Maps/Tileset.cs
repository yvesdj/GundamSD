using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace GundamSD.Maps
{
    public class Tileset
    {
        public Texture2D TileSetTexture { get; set; }
        public TmxTileset TmxTileset { get; set; }
        public int TilesWide => TileSetTexture.Width / TmxTileset.TileWidth;
        public int TilesHigh => TileSetTexture.Height / TmxTileset.TileHeight;
        public string TileSetName => TmxTileset.Name.ToString();

        public Tileset(Texture2D tileSetTexture, TmxTileset tmxTileset)
        {
            TileSetTexture = tileSetTexture;
            TmxTileset = tmxTileset;
        }


    }
}
