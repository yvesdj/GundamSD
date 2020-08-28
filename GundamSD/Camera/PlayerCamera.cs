using GundamSD.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace GundamSD.Camera
{
    public class PlayerCamera
    {
        public Vector2 CameraPos { get; set; }
        public Matrix ViewMatrix { get; private set; }

        private float _positionX;
        private float _positionY;
        private float _horizontalOffset;
        private float _verticalOffset;
        private float _zoomScale = 1.5f;

        public GraphicsDeviceManager Graphics { get; set; }
        public PlayerCamera(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            _horizontalOffset = (Graphics.PreferredBackBufferWidth / 2 - 300);
            _verticalOffset = (Graphics.PreferredBackBufferHeight / 2);
        }

        public void Follow(ISprite target, TmxMap tmxMap)
        {
            _positionX = ((target.Position.X - (target.HitBox.Width / 2))
                         - _horizontalOffset / _zoomScale);
            _positionY = ((target.Position.Y - (target.HitBox.Height / 2))
                         - _verticalOffset / _zoomScale);
            ClampCamera(tmxMap);

            CameraPos = new Vector2(_positionX, _positionY);
            //Console.WriteLine(CameraPos);
            ViewMatrix = Matrix.CreateTranslation(new Vector3(-CameraPos, 0))
                                                * Matrix.CreateScale(_zoomScale);
        }

        private void ClampCamera(TmxMap tmxMap)
        {
            int mapWidth = tmxMap.Width * tmxMap.TileWidth;
            int mapHeight = tmxMap.Height * tmxMap.TileHeight;
            

            if (_positionX < 0)
                _positionX = 0;
            if (_positionX > mapWidth - (int)(_horizontalOffset * 2) - 70)
                _positionX = mapWidth - (int)(_horizontalOffset * 2) - 70;
            if (_positionY < 0)
                _positionY = 0;
            if (_positionY > 250f)
                _positionY = 250f;
        }
    }
}
