using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Animations
{
    class AnimationAtlas
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int _currentFrame;
        private int _totalFrames;

        public AnimationAtlas(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            _currentFrame = 0;
            _totalFrames = Rows * Columns;
        }

        public void Update()
        {
            _currentFrame++;
            if (_currentFrame == _totalFrames)
            {
                _currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)_currentFrame / (float)Columns);
            int column = _currentFrame % Columns;

            Rectangle whatToDraw = new Rectangle(width * column, height * row, width, height);
            Rectangle whereToDraw = new Rectangle((int)location.X, (int)location.Y, width, height);

            
            spriteBatch.Draw(Texture, whereToDraw, whatToDraw, Color.White);
            
        }
    }
}
