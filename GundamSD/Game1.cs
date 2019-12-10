using GundamSD.Animations;
using GundamSD.Maps;
using GundamSD.Models;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System;
using System.Collections.Generic;
using TiledSharp;

namespace GundamSD
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<ISprite> _sprites;

        ////TiledSharp Test
        private TmxMap _tutorialMap;
        private Texture2D _tileset;
        private MapManager _mapManager;

        //int tileWidth;
        //int tileHeight;
        //int tilesetTilesWide;
        //int tilesetTilesHigh;
        //END TiledSharp Test

        //private TiledMap tutorialMap;
        //private TiledMapRenderer mapRenderer;

        //AtlasTest
        private AnimationAtlas _atlas;
        private AnimationAtlasManager _atlasManager;
        private AnimationAtlasAction _action;
        //END AtlasTest
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1600;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 800;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            //tutorialMap = Content.Load<TiledMap>("maps/TutorialMap");
            //mapRenderer = new TiledMapRenderer(GraphicsDevice);

            //AtlasTest
            
            _action = new AnimationAtlasAction(30, 38);
            _atlasManager = new AnimationAtlasManager(_atlas, _action);
            //END AtlasTest

        }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TiledSharp Test
            _tutorialMap = new TmxMap("Maps/Tiled/TutorialMap.tmx");
            _tileset = Content.Load<Texture2D>(_tutorialMap.Tilesets[0].Name.ToString());

            _mapManager = new MapManager(_tutorialMap, _tileset);

            //tileWidth = _tutorialMap.Tilesets[0].TileWidth;
            //tileHeight = _tutorialMap.Tilesets[0].TileHeight;

            //tilesetTilesWide = _tileset.Width / tileWidth;
            //tilesetTilesHigh = _tileset.Height / tileHeight;
            //END TiledSharp Test

            //AtlasTest
            Texture2D zetaAtlas = Content.Load<Texture2D>("Models/ZetaGundam_Atlas_64");
            _atlas = new AnimationAtlas(zetaAtlas, 10, 10);
            //END AtlasTest

            AnimationAtlasAction WalkRight = new AnimationAtlasAction(0, 3);
            AnimationAtlasAction Jump = new AnimationAtlasAction(5, 7, true);
            AnimationAtlasAction Attack = new AnimationAtlasAction(30, 38);

            Dictionary<string, AnimationAtlasAction> actions = new Dictionary<string, AnimationAtlasAction>()
            {
                { "WalkRight", WalkRight },
                { "Jump", Jump },
                { "Attack", Attack },
            };

            _sprites = new List<ISprite>
            {
                Factory.CreateSprite(_atlas, actions, true)
            };



            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
            {

                sprite.Update(gameTime, _sprites);
            }

            //AtlasTest
            //_animatedAtlas.Update();
            _atlasManager.Play(_action);
            _atlasManager.Update(gameTime);
            //END AtlasTest

            //mapRenderer.Update(tutorialMap, gameTime);
            
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //TiledSharp Test
            _mapManager.DrawMap(spriteBatch);
            //for (var i = 0; i < _tutorialMap.Layers[0].Tiles.Count; i++)
            //{
            //    int gid = _tutorialMap.Layers[0].Tiles[i].Gid;

            //    // Empty tile, do nothing
            //    if (gid == 0)
            //    {

            //    }
            //    else
            //    {
            //        int tileFrame = gid - 1;
            //        int column = tileFrame % tilesetTilesWide;
            //        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

            //        float x = (i % _tutorialMap.Width) * _tutorialMap.TileWidth;
            //        float y = (float)Math.Floor(i / (double)_tutorialMap.Width) * _tutorialMap.TileHeight;

            //        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

            //        spriteBatch.Draw(_tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
            //    }
            //}
            //END TiledSharp Test

            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }

            //AtlasTest
            _atlasManager.Draw(spriteBatch);
            //END AtlasTest


            //mapRenderer.Draw(tutorialMap);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
