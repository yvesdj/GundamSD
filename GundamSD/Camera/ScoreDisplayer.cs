using GundamSD.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GundamSD.Camera
{
    public class ScoreDisplayer
    {
        public PlayerCamera PlayerCamera { get; set; }
        public SpriteFont Font { get; set; }

        private Vector2 _fontPos;
        private Vector2 _offset;

        public ScoreDisplayer(PlayerCamera playerCamera, SpriteFont font)
        {
            PlayerCamera = playerCamera;
            Font = font;
            _offset = new Vector2(10f);
        }

        public void Update()
        {
            _fontPos = PlayerCamera.CameraPos + _offset;
        }

        public void Draw(SpriteBatch spriteBatch, MapManager mapManager)
        {
            if (mapManager.GetPlayerScore() == null)
                return;
            spriteBatch.DrawString(Font, "Score: " + mapManager.GetPlayerScore().Score, _fontPos, Color.White);
        }
    }
}