using GundamSD.Animations;
using GundamSD.Maps;
using GundamSD.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public TmxList<TmxObject> collidableLayer;
        //END TiledSharp Test

        //private TiledMap tutorialMap;
        //private TiledMapRenderer mapRenderer;

        //AtlasTest
        //private IAnimationAtlas _atlasPlayer;
        
        
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
        }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region PlayerInstantiation
            //AtlasTest
            Texture2D playerAtlas = Content.Load<Texture2D>("Models/ZetaGundam_Atlas_64");
            Texture2D enemyAtlas = Content.Load<Texture2D>("Models/ZakuII_Atlas_64");

            ISprite player = Factory.CreatePlayer(playerAtlas);
            ISprite grunt = Factory.CreateGrunt(enemyAtlas);
            #endregion

            _sprites = new List<ISprite>
            {
                //Factory.CreateSprite(_atlas, actions, /*true,*/ spawnPoint)
                player,
                grunt
            };

            //TiledSharp Test
            _tutorialMap = new TmxMap("Maps/Tiled/TutorialMap.tmx");
            _tileset = Content.Load<Texture2D>(_tutorialMap.Tilesets[0].Name.ToString());
            Console.WriteLine(_tutorialMap.Tilesets[0].Name.ToString());

            _mapManager = new MapManager(_tutorialMap, _tileset, _sprites);
            Vector2 spawnPoint = _mapManager.GetSpawnPoint(0);
            //END TiledSharp Test



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
            //foreach (var sprite in _sprites)
            //{
            //    sprite.Update(gameTime);
            //}

            _mapManager.UpdateMap(gameTime);
            //_mapManager.CheckCollision();

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
            //END TiledSharp Test
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
