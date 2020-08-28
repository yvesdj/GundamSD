﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.Camera;
using GundamSD.Maps;
using GundamSD.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;

namespace GundamSD.StateManagement.GameStates
{
    public class Level1 : GameState
    {
        private List<ISprite> _sprites;

        private TmxMap _map;
        private Texture2D _tileset;
        private MapManager _mapManager;

        private SpriteFont _font;
        private PlayerCamera _camera;
        private ScoreDisplayer _scoreDisplayer;

        public Level1(Game1 game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(game, graphicsDevice, graphicsDeviceManager)
        {
        }

        public override void Initialize()
        {
            Game.IsMouseVisible = false;
        }

        public override void LoadContent(ContentManager content)
        {
            #region PlayerInstantiation
            Texture2D playerAtlas = content.Load<Texture2D>("Models/ZetaGundam_Atlas_64");

            ISprite player = Factory.CreatePlayer(playerAtlas);

            _font = content.Load<SpriteFont>("Font");
            _camera = new PlayerCamera(_graphicsDeviceManager);
            _scoreDisplayer = new ScoreDisplayer(_camera, _font);
            #endregion

            #region EnemyInstantiation
            Texture2D gruntAtlas = content.Load<Texture2D>("Models/ZakuII_Atlas_64Flipped");

            List<int> wayPointIndexes0 = new List<int>() { 0, 1 };
            ISprite gruntMelee0 = Factory.CreateGruntMelee(gruntAtlas, wayPointIndexes0);
            List<int> wayPointIndexes1 = new List<int>() { 2, 3 };
            ISprite gruntMelee1 = Factory.CreateGruntMelee(gruntAtlas, wayPointIndexes1);

            ISprite gruntRanged0 = new GruntRanged(gruntAtlas);

            #endregion

            #region MapInstantiation
            _sprites = new List<ISprite>
            {
                player,
                gruntMelee0,
                gruntMelee1,
                gruntRanged0,
            };
            _map = new TmxMap("Maps/Tiled/Level1v2.tmx");
            List<Tileset> tileSets = new List<Tileset>();
            foreach (TmxTileset tileset in _map.Tilesets)
            {
                Texture2D tileSetTexture = content.Load<Texture2D>("maps/Tilesets/" + tileset.Name.ToString());
                tileSets.Add(new Tileset(tileSetTexture, tileset));
            }
            Texture2D background = content.Load<Texture2D>("Backgrounds/LevelBackground1");
            _mapManager = new MapManager(_map, tileSets, background, _sprites);
            #endregion
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _mapManager.UpdateMap(gameTime);

            _camera.Follow(_sprites[0], _map);
            _scoreDisplayer.Update();
            OpenPauseMenu();
        }

        private void OpenPauseMenu()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                GameState pauseMenu = new PauseMenu(Game, _graphicsDevice, _graphicsDeviceManager);
                GameStateManager.Instance.ChangeState(pauseMenu);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(transformMatrix: _camera.ViewMatrix);

            _mapManager.DrawMap(spriteBatch);

            _scoreDisplayer.Draw(spriteBatch, _mapManager);

            spriteBatch.End();
        }
    }
}
