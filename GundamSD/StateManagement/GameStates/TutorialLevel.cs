using System;
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
    public class TutorialLevel : GameState
    {
        private List<ISprite> _sprites;

        private TmxMap _map;
        private Texture2D _tileset;
        private MapManager _mapManager;

        private SpriteFont _font;
        private PlayerCamera _camera;
        private ScoreDisplayer _scoreDisplayer;

        public TutorialLevel(Game1 game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(game, graphicsDevice, graphicsDeviceManager)
        {
        }

        public override void Initialize()
        {
            Game.IsMouseVisible = false;
        }

        public override void LoadContent(ContentManager content)
        {
            #region PlayerInstantiation
            //AtlasTest
            Texture2D playerAtlas = content.Load<Texture2D>("Models/ZetaGundam_Atlas_64");
            Texture2D gruntAtlas = content.Load<Texture2D>("Models/ZakuII_Atlas_64Flipped");

            ISprite player = Factory.CreatePlayer(playerAtlas);

            List<int> wayPointIndexes = new List<int>() { 0, 1 };
            ISprite gruntMelee = Factory.CreateGruntMelee(gruntAtlas, wayPointIndexes);

            ISprite gruntRanged = new GruntRanged(gruntAtlas);

            _font = content.Load<SpriteFont>("Font");
            _camera = new PlayerCamera(_graphicsDeviceManager);
            _scoreDisplayer = new ScoreDisplayer(_camera, _font);

            #endregion

            _sprites = new List<ISprite>
            {
                player,
                gruntMelee,
                gruntRanged
            };

            _map = new TmxMap("Maps/Tiled/TutorialMap.tmx");
            List<Tileset> tileSets = new List<Tileset>();
            foreach (TmxTileset tileset in _map.Tilesets)
            {
                Texture2D tileSetTexture = content.Load<Texture2D>("maps/Tilesets/" + tileset.Name.ToString());
                tileSets.Add(new Tileset(tileSetTexture, tileset));
            }

            Texture2D background = content.Load<Texture2D>("Backgrounds/LevelBackground1");
            _mapManager = new MapManager(_map, tileSets, background, _sprites);

            Vector2 spawnPoint = _mapManager.GetSpawnPoint(0);
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _mapManager.UpdateMap(gameTime);
            CheckPlayerLives();
            CheckLevelCompletion();

            _camera.Follow(_sprites[0], _map);
            _scoreDisplayer.Update();
            OpenPauseMenu();
        }

        private void CheckLevelCompletion()
        {
            if (_mapManager.LevelComplete)
            {
                GameState level1 = new Level1(Game, _graphicsDevice, _graphicsDeviceManager);

                GameStateManager.Instance.AddState(level1);
            }
        }

        private void CheckPlayerLives()
        {
            if (_sprites[0] is Player player && player.GameOver)
            {
                player.Lives = 3;
                player.Score = 0;
                GameState gameOver = new GameOver(Game, _graphicsDevice, _graphicsDeviceManager);

                GameStateManager.Instance.AddState(gameOver);
            }
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
