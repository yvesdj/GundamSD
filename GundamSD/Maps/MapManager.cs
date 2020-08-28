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
using GundamSD.StateManagement;
using GundamSD.StateManagement.GameStates;

namespace GundamSD.Maps
{
    public class MapManager
    {
        private TmxMap _map;

        private Texture2D _tileSet;
        private List<Texture2D> _tileSetsTextures;

        private List<Tileset> _tilesets;

        private Texture2D _background;
        public List<ISprite> Sprites { get; set; }

        private int _tileWidth;
        private int _tileHeight;
        private int _tilesetTilesWide;
        private int _tilesetTilesHigh;

        public MapManager(TmxMap map, List<Tileset> tilesets, Texture2D background, List<ISprite> sprites)
        {
            _map = map;
            _tilesets = tilesets;
            _background = background;

            Sprites = sprites;
            SetSpriteSpawns();

            _tileWidth = _map.Tilesets[0].TileWidth;
            _tileHeight = _map.Tilesets[0].TileHeight;
        }

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

        public MapManager(TmxMap map, List<Texture2D> tileSets, Texture2D background, List<ISprite> sprites)
        {
            _map = map;
            _tileSetsTextures = tileSets;
            _background = background;

            Sprites = sprites;
            SetSpriteSpawns();

            _tileWidth = _map.Tilesets[0].TileWidth;
            _tileHeight = _map.Tilesets[0].TileHeight;

            _tilesetTilesWide = _tileSetsTextures[0].Width / _tileWidth;
            _tilesetTilesHigh = _tileSetsTextures[0].Height / _tileHeight;
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
            TmxLayer mapLayer = _map.Layers[layerName];

            for (var i = 0; i < mapLayer.Tiles.Count; i++)
            {
                foreach (Tileset tileset in _tilesets)
                {
                    int gid = mapLayer.Tiles[i].Gid;

                    // Empty tile, do nothing
                    if (gid == 0)
                    {

                    }
                    else
                    {

                        int tileFrame = gid - 1;
                        int column = tileFrame % tileset.TilesWide;
                        int row = (int)Math.Floor(((double)tileFrame - tileset.TmxTileset.FirstGid + 1) / (double)tileset.TilesWide);

                        float x = (i % _map.Width) * _map.TileWidth;
                        float y = (float)Math.Floor(i / (double)_map.Width) * _map.TileHeight;

                        Rectangle tilesetRec = new Rectangle(_tileWidth * column, _tileHeight * row, _tileWidth, _tileHeight);
                        Rectangle destination = new Rectangle((int)x, (int)y, _tileWidth, _tileHeight);

                        mapLayer.Properties.TryGetValue("Tileset", out string tileSetName);

                        if (tileset.TileSetName == tileSetName)
                            spriteBatch.Draw(tileset.TileSetTexture,
                                        destination,
                                        tilesetRec,
                                        Color.White);
                    }
                }
            }
        }

        public void SetSpriteSpawns()
        {
            //First sprite will always be Player, First spawnpoint belongs to player
            for (int i = 0; i < Sprites.Count; i++)
            {
                Sprites[i].Position = GetSpawnPoint(i);
            }
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, new Rectangle(0, 0, _background.Width, _background.Height), Color.DimGray);
            foreach (TmxLayer layer in _map.Layers)
            {
                if (layer.Name == "Foreground")
                {
                    foreach (var sprite in Sprites)
                    {
                        sprite.Draw(spriteBatch);
                    }
                    DrawLayer(spriteBatch, layer.Name);
                }
                DrawLayer(spriteBatch, layer.Name);
            }
        }

        public void UpdateMap(GameTime gameTime)
        {
            for (int i = 0; i < Sprites.Count; i++)
            {
                if (Sprites[i] is IHasHealth hasHealth && hasHealth.HealthHandler.IsDead)
                {
                    if (Sprites[i] is Player player)
                    {
                        RespawnIfPlayer(i, hasHealth, player);
                    }

                    else
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

        private void RespawnIfPlayer(int i, IHasHealth hasHealth, Player player)
        {
            player.Lives--;
            hasHealth.HealthHandler.CurrentHealth = player.MaxHealth;
            hasHealth.HealthHandler.IsDead = false;
            player.Position = GetSpawnPoint(i);
        }
    }
}
