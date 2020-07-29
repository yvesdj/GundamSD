using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GundamSD.Movement
{
    public class Mover : IMover
    {
        //private IInput _inputs;
        private ISprite _sprite;

        private Vector2 _jumpVelocity;
        private float gravity = -9.81f;
        private bool _isJumping;

        public Mover(ISprite sprite)
        {
            //_inputs = inputs;
            _sprite = sprite;
        }
        public void Move()
        {
            if (_sprite.Inputs == null)
            {
                return;
            }

            //_sprite.Position = new Vector2(0, 2);

            if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Down))
                _sprite.Velocity = new Vector2(0 , _sprite.Speed);
            else if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Left))
                _sprite.Velocity = new Vector2(-_sprite.Speed , 0);
            else if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Right))
                _sprite.Velocity = new Vector2(_sprite.Speed , 0);
        }

        public void UpdatePosition()
        {
            _sprite.Position += _sprite.Velocity;
        }

        public void ResetVelocity()
        {
            _sprite.Velocity = Vector2.Zero;
        }

        public void Jump()
        {
            if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Jump) && !_isJumping)
            {
                _jumpVelocity.Y = (float)Math.Sqrt(_sprite.JumpHeight * 2f * gravity);
                _isJumping = true;
            }

            _jumpVelocity.Y += gravity;
        }
    }
}
