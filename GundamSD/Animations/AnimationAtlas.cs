using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Animations
{
    public class AnimationAtlas : IAnimationAtlas
    {
        public Texture2D Texture { get; set; }
        public int FrameWidth => Texture.Width / Columns;
        public int FrameHeight => Texture.Height / Rows;
        public int Rows { get; set; }
        public int Columns { get; set; }

        private readonly int _totalFrames;

        public int TotalFrames => _totalFrames;

        public AnimationAtlas(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            
            _totalFrames = Rows * Columns;
        }

        //public void Update()
        //{
        //    _currentFrame++;
        //    if (_currentFrame == _totalFrames)
        //    {
        //        _currentFrame = 0;
        //    }
        //}

        //public void Draw(SpriteBatch spriteBatch, Vector2 location)
        //{
            //int row = (_currentFrame / Columns);
            //int column = _currentFrame % Columns;

            //Rectangle whatToDraw = new Rectangle(FrameWidth * column, FrameHeight * row, FrameWidth, FrameHeight);
            //Rectangle whereToDraw = new Rectangle((int)location.X, (int)location.Y, FrameWidth, FrameHeight);


            //spriteBatch.Draw(Texture, whereToDraw, whatToDraw, Color.White);

        //}
    }
}
