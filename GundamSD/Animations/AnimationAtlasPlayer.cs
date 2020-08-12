using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Animations
{
    public class AnimationAtlasPlayer : IAnimationAtlasPlayer
    {
        private IAnimationAtlas _atlas;
        //private AnimationAtlasAction _action;
        public IAnimationAtlasAction action;
        private float _timer;
        private float _frameSpeed;
        private int _currentFrame;

        public int CurrentFrame
        {
            get { return _currentFrame; }
        }

        public Vector2 Position { get; set; }

        public AnimationAtlasPlayer(IAnimationAtlas atlas, IAnimationAtlasAction action)
        {
            _atlas = atlas;
            this.action = action;
            _currentFrame = action.StartFrame;
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

        public void Play(IAnimationAtlasAction action) //what action to start playing
        {
            if (this.action == action) return;

            this.action = action;
            _currentFrame = this.action.StartFrame; //startframe
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

                if (_currentFrame > action.EndFrame) //action start frame >= action total frames
                {
                    if (action.ShouldHold)
                    {
                        _currentFrame = action.EndFrame;
                    } else
                    {
                        _currentFrame = action.StartFrame; //action start frame
                    }
                    
                }
            }
        }
    }
}
