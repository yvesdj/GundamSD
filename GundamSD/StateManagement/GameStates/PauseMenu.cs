﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.StateManagement.GameStates
{
    public class PauseMenu : GameState
    {
        private Texture2D _background;
        private List<Button> _buttons;

        public PauseMenu(Game1 game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(game, graphicsDevice, graphicsDeviceManager)
        {
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
            _background = content.Load<Texture2D>("UI/PauseMenuBackground");

            Texture2D btnTexture = content.Load<Texture2D>("UI/Button");
            SpriteFont font = content.Load<SpriteFont>("Font");

            Button resumeGameBtn = new Button(btnTexture, font, "Resume", new Vector2(950, 150));
            resumeGameBtn.Click += ResumeGameBtn_Click;

            Button quitBtn = new Button(btnTexture, font, "Quit", new Vector2(950, 350));
            quitBtn.Click += QuitBtn_Click;

            _buttons = new List<Button>()
            {
                resumeGameBtn,
                quitBtn
            };
        }

        private void QuitBtn_Click(object sender, EventArgs e)
        {
            Game.Exit();
        }

        private void ResumeGameBtn_Click(object sender, EventArgs e)
        {
            GameStateManager.Instance.RemoveState();
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

            spriteBatch.Draw(_background, new Rectangle(0, 0, 1600, 800), Color.DimGray);

            foreach (Button btn in _buttons)
            {
                btn.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
