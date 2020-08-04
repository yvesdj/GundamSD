using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.Maps;
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

        private Vector2 _jumpVelocity;
        private float gravity = -9.81f;
        private bool _isJumping;

        public Mover(ISprite sprite)
        {
            //_inputs = inputs;
            _sprite = sprite;
        }
        public void Move(GameTime gametime, MapManager mapManager)
        {
            if (_sprite.Inputs != null)
            {
                _sprite.Inputs.GetKeyboardState();
                if (_sprite.Inputs.KeyIsHoldDown(_sprite.Inputs.Up))
                {
                    _velocityY = -_sprite.Speed;
                    _sprite.CollisionHandler.IsGrounded = false;
                }
                else if (_sprite.Inputs.KeyIsHoldDown(_sprite.Inputs.Down))
                {
                    _velocityY = _sprite.Speed;
                }
                else if (_sprite.Inputs.KeyIsPressed(_sprite.Inputs.Jump))
                {
                    if (_sprite.CollisionHandler.IsGrounded)
                    {
                        _velocityY = -200f;
                    }
                    _sprite.CollisionHandler.IsGrounded = false;
                }
                else
                {
                    _velocityY = 0;
                }

                if (_sprite.Inputs.KeyIsHoldDown(_sprite.Inputs.Left))
                    _velocityX = -_sprite.Speed;
                else if (_sprite.Inputs.KeyIsHoldDown(_sprite.Inputs.Right))
                    _velocityX = _sprite.Speed;
                else
                    _velocityX = 0;
            }

            

            ApplyGravity(gametime);

            Velocity = new Vector2(_velocityX, _velocityY);
            _sprite.CollisionHandler.CheckCollisionMap(mapManager);
            _sprite.CollisionHandler.CheckCollisionSprite(mapManager);
            //Console.WriteLine(_sprite.Mover.Velocity);
            



            NextPosition = _sprite.Position + Velocity;
            //Console.WriteLine(_sprite.CollisionHandler.IsGrounded);
            //Console.WriteLine(_velocityY);
        }

        private void ApplyGravity(GameTime gametime)
        {
            if (!_sprite.CollisionHandler.IsGrounded || !_sprite.CollisionHandler.IsColliding)
            {
                _timer += (float)gametime.ElapsedGameTime.TotalSeconds;
                _velocityY += _gravity * 3 * _timer;
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
