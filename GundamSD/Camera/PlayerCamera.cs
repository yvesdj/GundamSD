using GundamSD.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Camera
{
    public class PlayerCamera
    {
        public Vector2 CameraPos { get; set; }
        public Matrix ViewMatrix { get; private set; }

        private float _positionX;
        private float _positionY;
        private float _zoomScale = 1.5f;

        public GraphicsDeviceManager Graphics { get; set; }
        public PlayerCamera(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
        }

        public void Follow(ISprite target)
        {
            _positionX = ((target.Position.X - (target.HitBox.Width / 2))
                         - (Graphics.PreferredBackBufferWidth / 2 - 300) / _zoomScale);
            _positionY = ((target.Position.Y - (target.HitBox.Height / 2))
                         - (Graphics.PreferredBackBufferHeight / 2) / _zoomScale);
            ClampCamera();

            CameraPos = new Vector2(_positionX, _positionY);

            ViewMatrix = Matrix.CreateTranslation(new Vector3(-CameraPos, 0))
                                                * Matrix.CreateScale(_zoomScale);
        }

        private void ClampCamera()
        {
            if (_positionX < 0)
                _positionX = 0;
            if (_positionY < 0)
                _positionY = 0;
            if (_positionY > 250f)
                _positionY = 250f;
        }
    }
}
