using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.Models;
using GundamSD.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.StateManagement.GameStates
{
    public class GameComplete : GameState
    {
        private ISprite _player;
        private Texture2D _background;
        private List<Button> _buttons;
        private SpriteFont _font;

        private Vector2 _fontScorePos;
        private Vector2 _fontLivesPos;

        public GameComplete(Game1 game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, ISprite player) : base(game, graphicsDevice, graphicsDeviceManager)
        {
            _player = player;
        }

        public override void Initialize()
        {
            if (!Game.IsMouseVisible)
            {
                Game.IsMouseVisible = true;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("UI/Complete");

            Texture2D btnTexture = content.Load<Texture2D>("UI/Button");
            _font = content.Load<SpriteFont>("Font");

            Button restartBtn = new Button(btnTexture, _font, "Restart", new Vector2(405, 250));
            restartBtn.Click += RestartBtn_Click;

            Button quitBtn = new Button(btnTexture, _font, "Quit", new Vector2(405, 350));
            quitBtn.Click += QuitBtn_Click;

            _buttons = new List<Button>()
            {
                restartBtn,
                quitBtn
            };

            _fontScorePos = new Vector2(404, 450);
            _fontLivesPos = new Vector2(404, 500);
        }

        private void QuitBtn_Click(object sender, EventArgs e)
        {
            Game.Exit();
        }

        private void RestartBtn_Click(object sender, EventArgs e)
        {
            GameState TutorialLvl = new TutorialLevel(Game, _graphicsDevice, _graphicsDeviceManager);
            GameStateManager.Instance.ChangeState(TutorialLvl);
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button btn in _buttons)
            {
                btn.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_background, new Rectangle(0, 0, 1600, 800), Color.White);

            foreach (Button btn in _buttons)
            {
                btn.Draw(spriteBatch);
            }

            if (_player is Player player)
            {
                spriteBatch.DrawString(_font, "Score: " + player.Score, _fontScorePos, Color.Teal);
                spriteBatch.DrawString(_font, "Lives left: " + player.Lives, _fontLivesPos, Color.Teal);
            }

            spriteBatch.End();
        }
    }
}
