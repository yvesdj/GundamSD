using GundamSD.Animations;
using GundamSD.Models;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System.Collections.Generic;

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

        private TiledMap tutorialMap;
        private TiledMapRenderer mapRenderer;

        //AtlasTest
        private AnimationAtlas _animatedAtlas;
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

            tutorialMap = Content.Load<TiledMap>("maps/TutorialMap");
            mapRenderer = new TiledMapRenderer(GraphicsDevice);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //AtlasTest
            Texture2D zetaAtlas = Content.Load<Texture2D>("Models/ZetaGundam_Atlas_64");
            _animatedAtlas = new AnimationAtlas(zetaAtlas, 10, 10);
            //END AtlasTest

            //var texture = Content.Load<Texture2D>("Models/ZetaGundam");
            IAnimation WalkRight = Factory.CreateAnimation();
            WalkRight.Texture = Content.Load<Texture2D>("Models/ZetaGundam_WalkRight");
            WalkRight.FrameCount = 4;
            IAnimation WalkLeft = Factory.CreateAnimation();
            WalkLeft.Texture = Content.Load<Texture2D>("Models/ZetaGundam_WalkLeft");
            WalkLeft.FrameCount = 4;

            Dictionary<string, IAnimation> animations = new Dictionary<string, IAnimation>()
            {
                { "WalkRight", WalkRight },
                { "WalkLeft", WalkLeft }
            };

            //_sprites = new List<Sprite>()
            //{
            //    new Sprite(animations)
            //    //ISprite sprite1 = Factory.CreateSprite(animations)

            //    {
            //        Position = new Vector2(100,100),
            //        Inputs = Factory.CreateInput()
            //    }
            //};

            _sprites = new List<ISprite>
            {
                Factory.CreateSprite(animations, true)
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
            _animatedAtlas.Update();
            //END AtlasTest

            mapRenderer.Update(tutorialMap, gameTime);
            
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            
            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }

            //AtlasTest
            _animatedAtlas.Draw(spriteBatch, new Vector2(800,400));
            //END AtlasTest


            mapRenderer.Draw(tutorialMap);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
