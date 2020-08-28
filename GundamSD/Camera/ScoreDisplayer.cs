using GundamSD.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GundamSD.Camera
{
    public class HudDisplayer
    {
        public PlayerCamera PlayerCamera { get; set; }
        public SpriteFont Font { get; set; }

        private Vector2 _fontScorePos;
        private Vector2 _scoreOffset;

        private Vector2 _fontLivesPos;
        private Vector2 _livesOffset;

        public HudDisplayer(PlayerCamera playerCamera, SpriteFont font)
        {
            PlayerCamera = playerCamera;
            Font = font;
            _scoreOffset = new Vector2(10f);
            _livesOffset = _scoreOffset + new Vector2(0, _scoreOffset.Y + 20f);
        }

        public void Update()
        {
            _fontScorePos = PlayerCamera.CameraPos + _scoreOffset;
            _fontLivesPos = PlayerCamera.CameraPos + _livesOffset;
        }

        public void Draw(SpriteBatch spriteBatch, MapManager mapManager)
        {
            if (mapManager.GetPlayerScore() == null)
                return;
            spriteBatch.DrawString(Font, "Score: " + mapManager.GetPlayerScore().Score, _fontScorePos, Color.White);

            if (mapManager.GetPlayerLives() == null)
                return;
            spriteBatch.DrawString(Font, "Lives: " + mapManager.GetPlayerLives().Lives, _fontLivesPos, Color.White);
        }
    }
}