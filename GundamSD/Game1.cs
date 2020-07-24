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
        //END TiledSharp Test

        //private TiledMap tutorialMap;
        //private TiledMapRenderer mapRenderer;

        //AtlasTest
        private AnimationAtlas _atlas;
        
        
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

            //TiledSharp Test
            _tutorialMap = new TmxMap("Maps/Tiled/TutorialMap.tmx");
            _tileset = Content.Load<Texture2D>(_tutorialMap.Tilesets[0].Name.ToString());

            _mapManager = new MapManager(_tutorialMap, _tileset);
            Vector2 spawnPoint = _mapManager.GetSpawnPoint(0);
            //END TiledSharp Test

            #region PlayerInstantiation
            //AtlasTest
            Texture2D playerAtlas = Content.Load<Texture2D>("Models/ZetaGundam_Atlas_64");
            _atlas = new AnimationAtlas(playerAtlas, 10, 10);
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

            ISprite playerSprite = Factory.CreateSprite(_atlas, actions, /*true,*/ spawnPoint);
            IPlayer player = Factory.CreatePlayer(playerSprite);
            #endregion

            _sprites = new List<ISprite>
            {
                //Factory.CreateSprite(_atlas, actions, /*true,*/ spawnPoint)
                player.PlayerSprite
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
            _mapManager.DrawLayer(spriteBatch, "BackgroundWall");
            _mapManager.DrawLayer(spriteBatch, "BackgroundStuff");
            _mapManager.DrawLayer(spriteBatch, "Walkable");

            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }

            _mapManager.DrawLayer(spriteBatch, "Foreground");
            //END TiledSharp Test

            

            

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
