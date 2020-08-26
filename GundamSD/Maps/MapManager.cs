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
        private Texture2D _background;
        public List<ISprite> Sprites { get; set; }

        //private List<Rectangle> _collisionBoxes => GetMapCollidables();

        private int _tileWidth;
        private int _tileHeight;
        private int _tilesetTilesWide;
        private int _tilesetTilesHigh;


        public MapManager(TmxMap map, Texture2D tileSet, Texture2D background, List<ISprite> sprites)
        {
            _map = map;
            _tileSet = tileSet;
            _background = background;

            Sprites = sprites;
            SetSpriteSpawns();

            _tileWidth = _map.Tilesets[0].TileWidth;
            _tileHeight = _map.Tilesets[0].TileHeight;

            _tilesetTilesWide = _tileSet.Width / _tileWidth;
            _tilesetTilesHigh = _tileSet.Height / _tileHeight;
        }

        public IHasScore GetPlayerScore()
        {
            return Sprites[0] is IHasScore hasScore ? hasScore : null;
        }

        public Vector2 GetSpawnPoint(int spawnPointNumber)
        {
            TmxObject spawnPoint = _map.ObjectGroups["SpawnPoints"].Objects[spawnPointNumber];
            Vector2 spawnPointLocation = new Vector2((float)spawnPoint.X, (float)spawnPoint.Y);
            return spawnPointLocation;
        }

        public List<Rectangle> GetMapRectangles(string layerName)
        {
            TmxList<TmxObject> tmxObjects = _map.ObjectGroups[layerName].Objects;
            List <Rectangle> rectangles = new List<Rectangle>();

            foreach (TmxObject tmxObject in tmxObjects)
            {
                Rectangle objectBox = new Rectangle((int)tmxObject.X, (int)tmxObject.Y,
                                                    (int)tmxObject.Width, (int)tmxObject.Height);
                rectangles.Add(objectBox);
            }

            return rectangles;
        }

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

        public void SetSpriteSpawns()
        {
            //first sprite will always be Player
            //Sprites[0].Position = GetSpawnPoint(0);
            //Sprites[1].Position = GetSpawnPoint(1);
            //Sprites[2].Position = GetSpawnPoint(2);

            for (int i = 0; i < Sprites.Count; i++)
            {
                Sprites[i].Position = GetSpawnPoint(i);
            }
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, new Rectangle(0, 0, _background.Width, _background.Height), Color.DimGray);
            DrawLayer(spriteBatch, "BackgroundWall");
            DrawLayer(spriteBatch, "BackgroundStuff");
            DrawLayer(spriteBatch, "Walkable");

            foreach (var sprite in Sprites)
            {
                sprite.Draw(spriteBatch);
            }

            DrawLayer(spriteBatch, "Foreground");
        }

        public void UpdateMap(GameTime gameTime)
        {
            for (int i = 0; i < Sprites.Count; i++)
            {
                if (Sprites[i] is IHasHealth hasHealth && hasHealth.HealthHandler.IsDead)
                {
                    Console.WriteLine("DEAD");
                    Sprites.Remove(Sprites[i]);
                }
                else if (Sprites[i] is Bullet bullet && bullet.IsExpired)
                {
                    Sprites.Remove(Sprites[i]);
                }
                else
                    Sprites[i].Update(gameTime, this);
            }
        }

    }
}
