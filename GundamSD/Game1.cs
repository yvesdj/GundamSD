using GundamSD.Animations;
using GundamSD.Maps;
using GundamSD.Models;
using GundamSD.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TiledSharp;
using GundamSD.StateManagement;
using GundamSD.StateManagement.GameStates;

namespace GundamSD
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphicsManager;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphicsManager.PreferredBackBufferWidth = 1600;  // set this value to the desired width of your window
            _graphicsManager.PreferredBackBufferHeight = 800;   // set this value to the desired height of your window
            _graphicsManager.ApplyChanges();
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
            //IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            GameStateManager.Instance.SetContent(Content);

            GameState MainMenu = new MainMenu(this, GraphicsDevice, _graphicsManager);

            GameStateManager.Instance.AddState(MainMenu);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            GameStateManager.Instance.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            GameStateManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Black);

            GameStateManager.Instance.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}