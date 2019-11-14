

using GundamSD.Animations;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Models
{
    public class Sprite : ISprite
    {
        protected IAnimationManager _animationManager;
        protected Dictionary<string, IAnimation> _animations;

        private Texture2D _texture;
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        
        public IInput Inputs { get; set; }

        public float Speed { get; set; }

        //public Vector2 Velocity;
        private Vector2 _velocity;

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public IMover Mover;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            Speed = 0.2f;
        }

        public Sprite(Dictionary<string, IAnimation> animations)
        {
            _animations = animations;
            _animationManager = Factory.CreateAnimationManager(_animations.First().Value); 
            Speed = 2f;
            Mover = Factory.CreateMover(this);
        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Mover.Move();

            SetAnimation();

            _animationManager.Update(gameTime);

            Mover.UpdatePosition();
            Mover.ResetVelocity();
        }

        protected void SetAnimation()
        {
            if (_velocity.X > 0)
                _animationManager.Play(_animations["WalkRight"]);
            else if (_velocity.X < 0)
                _animationManager.Play(_animations["WalkLeft"]);
            //else if (Velocity.Y > 0)
            //    _animationManager.Play(_animations["WalkDown"]);
            //else if (Velocity.Y < 0)
            //    _animationManager.Play(_animations["WalkUp"]);
            else _animationManager.Stop();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("this ni goe");
        }

        //private void Move()
        //{
        //    Speed = 0.2f;
        //    if (Inputs == null)
        //    {
        //        return;
        //    }

        //    if (Keyboard.GetState().IsKeyDown(Inputs.Up))
        //        _velocity.Y = -Speed;
        //    else if (Keyboard.GetState().IsKeyDown(Inputs.Down))
        //        _velocity.Y = Speed;
        //    else if (Keyboard.GetState().IsKeyDown(Inputs.Left))
        //        _velocity.X = -Speed;
        //    else if (Keyboard.GetState().IsKeyDown(Inputs.Right))
        //        _velocity.X = Speed;
        //}
    }

}
