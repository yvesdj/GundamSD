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
        public Vector2 Velocity { get; set; }
        public Vector2 NextPosition { get; set; }

        private float _velocityX;
        private float _velocityY;
        private const float _gravity = 9.81f;
        private const float _frameSpeed = 0.15f;
        private float _timer;

        public Mover(ISprite sprite)
        {
            //_inputs = inputs;
            _sprite = sprite;
        }
        public void Move(GameTime gametime)
        {
            if (_sprite.Inputs == null)
            {
                return;
            }

            _sprite.Inputs.GetKeyboardState();
            if (_sprite.Inputs.KeyIsHoldDown(_sprite.Inputs.Up))
            {
                _velocityY = -_sprite.Speed;
                _sprite.CollisionHandler.IsGrounded = false;
            }
            else if (_sprite.Inputs.KeyIsHoldDown(_sprite.Inputs.Down))
                _velocityY = _sprite.Speed;
            else if (_sprite.Inputs.KeyIsPressed(_sprite.Inputs.Jump))
            {
                if (_velocityY == 0)
                    _velocityY = -100f;
                _sprite.CollisionHandler.IsGrounded = false;
            }
            else
                _velocityY = 0;

            if (_sprite.Inputs.KeyIsHoldDown(_sprite.Inputs.Left))
                _velocityX = -_sprite.Speed;
            else if (_sprite.Inputs.KeyIsHoldDown(_sprite.Inputs.Right))
                _velocityX = _sprite.Speed;
            else
                _velocityX = 0;

            //_velocityY += _gravity * (float)gametime.ElapsedGameTime.TotalSeconds;
            ApplyGravity(gametime);

            Velocity = new Vector2(_velocityX, _velocityY);

            NextPosition = _sprite.Position + Velocity;
            Console.WriteLine(_sprite.CollisionHandler.IsGrounded);
            //ResetVelocity();
        }

        private void ApplyGravity(GameTime gametime)
        {
            if (!_sprite.CollisionHandler.IsGrounded)
            {
                _timer += (float)gametime.ElapsedGameTime.TotalSeconds;
                _velocityY += _gravity * 2 * _timer;
                if (_timer > _frameSpeed)
                {
                    _timer = 0f;
                }
            }
        }

        public void UpdatePosition()
        {
            _sprite.Position = NextPosition;
        }

        public void ResetVelocity()
        {
            Velocity = Vector2.Zero;
        }
    }
}
