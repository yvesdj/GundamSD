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

        private TmxMap _tutorialMap;
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
            ISprite gruntMelee = Factory.CreateGruntMelee(gruntAtlas);
            //ISprite grunt = new GruntBase(gruntAtlas);
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

            _tutorialMap = new TmxMap("Maps/Tiled/TutorialMap.tmx");
            _tileset = content.Load<Texture2D>(_tutorialMap.Tilesets[0].Name.ToString());
            Console.WriteLine(_tutorialMap.Tilesets[0].Name.ToString());

            _mapManager = new MapManager(_tutorialMap, _tileset, _sprites);
            Vector2 spawnPoint = _mapManager.GetSpawnPoint(0);
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _mapManager.UpdateMap(gameTime);

            _camera.Follow(_sprites[0]);
            _scoreDisplayer.Update();

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
