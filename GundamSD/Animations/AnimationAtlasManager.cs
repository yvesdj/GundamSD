using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Animations
{
    public class AnimationAtlasManager
    {
        private AnimationAtlas _atlas;
        private float _timer;
        private float _frameSpeed;
        public Vector2 Position { get; set; }

        public AnimationAtlasManager(AnimationAtlas atlas)
        {
            _atlas = atlas;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            int row = (_atlas.CurrentFrame / _atlas.Columns);
            int column = _atlas.CurrentFrame % _atlas.Columns;

            Rectangle whatToDraw = new Rectangle(_atlas.FrameWidth * column, _atlas.FrameHeight * row, _atlas.FrameWidth, _atlas.FrameHeight);
            Rectangle whereToDraw = new Rectangle((int)Position.X, (int)Position.Y, _atlas.FrameWidth, _atlas.FrameHeight);


            spriteBatch.Draw(_atlas.Texture, whereToDraw, whatToDraw, Color.White);
        }

        public void Play(AnimationAtlas atlas)
        {
            if (_atlas == atlas) return;

            _atlas = atlas;
            _atlas.CurrentFrame = 0;
            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;
            _atlas.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime, float frameSpeed)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameSpeed = frameSpeed;

            if (_timer > _frameSpeed)
            {
                _timer = 0f;
                _atlas.CurrentFrame++;

                if (_atlas.CurrentFrame >= _atlas.TotalFrames)
                    _atlas.CurrentFrame = 0;
            }
        }
    }
}
