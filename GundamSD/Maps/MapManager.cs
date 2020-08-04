using GundamSD.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;
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
        private List<ISprite> _sprites;
        //private List<Rectangle> _collisionBoxes => GetMapCollidables();

        private int _tileWidth;
        private int _tileHeight;
        private int _tilesetTilesWide;
        private int _tilesetTilesHigh;

        public MapManager(TmxMap map, Texture2D tileSet, List<ISprite> sprites)
        {
            _map = map;
            _tileSet = tileSet;
            _sprites = sprites;
            //first sprite will always be Player
            _sprites[0].Position = GetSpawnPoint(0);
            _sprites[1].Position = new Vector2(700, 100);

            _tileWidth = _map.Tilesets[0].TileWidth;
            _tileHeight = _map.Tilesets[0].TileHeight;

            _tilesetTilesWide = _tileSet.Width / _tileWidth;
            _tilesetTilesHigh = _tileSet.Height / _tileHeight;
        }

        public Vector2 GetSpawnPoint(int spawnPointNumber)
        {
            TmxObject spawnPoint = _map.ObjectGroups["SpawnPoints"].Objects[spawnPointNumber];
            Vector2 spawnPointLocation = new Vector2((float)spawnPoint.X, (float)spawnPoint.Y);
            return spawnPointLocation;
        }

        public List<Rectangle> GetMapCollidables()
        {
            TmxList<TmxObject> collidableObjects = _map.ObjectGroups["Collidable"].Objects;
            List <Rectangle> collidableBoxes = new List<Rectangle>();

            foreach (TmxObject tmxObject in collidableObjects)
            {
                Rectangle objectBox = new Rectangle((int)tmxObject.X, (int)tmxObject.Y,
                                                    (int)tmxObject.Width, (int)tmxObject.Height);
                collidableBoxes.Add(objectBox);
            }

            return collidableBoxes;
        }

        //public TmxLayerTile GetTile(int x, int y)
        //{
        //    for (int i = 0; i < length; i++)
        //    {

        //    }

        //}

        public void DrawLayer(SpriteBatch spriteBatch, string layerName)
        {
            
            for (var i = 0; i <  _map.Layers[layerName].Tiles.Count; i++)
            {
                int gid = _map.Layers[layerName].Tiles[i].Gid;
                
                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {
                    
                    int tileFrame = gid - 1;
                    int column = tileFrame % _tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)_tilesetTilesWide);

                    float x = (i % _map.Width) * _map.TileWidth;
                    float y = (float)Math.Floor(i / (double)_map.Width) * _map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(_tileWidth * column, _tileHeight * row, _tileWidth, _tileHeight);
                    Rectangle destination = new Rectangle((int)x, (int)y, _tileWidth, _tileHeight);

                    spriteBatch.Draw(_tileSet,
                        destination,
                        tilesetRec,
                        Color.White);
                    //spriteBatch.Draw(_tileSet, destination, tilesetRec, Color.White, 0, Vector2.Zero, SpriteEffects.None, depth);
                }
            }
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            DrawLayer(spriteBatch, "BackgroundWall");
            DrawLayer(spriteBatch, "BackgroundStuff");
            DrawLayer(spriteBatch, "Walkable");

            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }

            DrawLayer(spriteBatch, "Foreground");
        }

        public void UpdateMap(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, this);
                Console.WriteLine(sprite.Position);
            }

        }
    }
}
