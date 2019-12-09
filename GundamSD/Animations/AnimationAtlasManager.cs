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
        private AnimationAtlasAction _action;
        private float _timer;
        private float _frameSpeed;
        private int _currentFrame;
        
        public Vector2 Position { get; set; }

        public AnimationAtlasManager(AnimationAtlas atlas, AnimationAtlasAction action)
        {
            _atlas = atlas;
            //_action = action;
            //_currentFrame = _action.StartFrame;
            _frameSpeed = 0.15f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int row = _currentFrame / _atlas.Columns;
            int column = _currentFrame % _atlas.Columns;

            Rectangle whatToDraw = new Rectangle(_atlas.FrameWidth * column, _atlas.FrameHeight * row, _atlas.FrameWidth, _atlas.FrameHeight);
            Rectangle whereToDraw = new Rectangle((int)Position.X, (int)Position.Y, _atlas.FrameWidth, _atlas.FrameHeight);


            spriteBatch.Draw(_atlas.Texture, whereToDraw, whatToDraw, Color.White);
        }

        public void Play(AnimationAtlasAction action) //what action to start playing
        {
            if (_action == action) return;

            _action = action;
            _currentFrame = _action.StartFrame; //startframe
            _timer = 0;
            
        }

        public void Stop() //what action to stop
        {
            _timer = 0f;
            _currentFrame = 0; //action start frame
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            

            if (_timer > _frameSpeed)
            {
                _timer = 0f;
                _currentFrame++;

                if (_currentFrame > _action.EndFrame) //action start frame >= action total frames
                {
                    if (_action.ShouldHold)
                    {
                        _currentFrame = _action.EndFrame;
                    } else
                    {
                        _currentFrame = _action.StartFrame; //action start frame
                    }
                    
                }
            }
        }
    }
}
