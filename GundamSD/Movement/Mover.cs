﻿using System;
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
            if (_sprite is IHasInput hasInput)
            {
                hasInput.Inputs.GetKeyboardState();
                if (hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Up))
                {
                    _velocityY = -_sprite.Speed;
                    _sprite.CollisionHandler.IsGrounded = false;
                }
                else if (hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Down))
                {
                    _velocityY = _sprite.Speed;
                }
                else if (hasInput.Inputs.KeyIsPressed(hasInput.Inputs.Jump))
                {
                    if (_sprite.CollisionHandler.IsGrounded)
                    {
                        _velocityY = -_sprite.JumpHeight;
                    }
                    _sprite.CollisionHandler.IsGrounded = false;
                }

                if (hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Left))
                    _velocityX += -_sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
                else if (hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Right))
                    _velocityX += _sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;

            }
            //if (_sprite.Inputs != null)
            //{
            //}

            ApplyGravity(gametime);
            ApplyDrag(gametime);
            ClampVelocity();

            Velocity = new Vector2(_velocityX, _velocityY);
            _sprite.CollisionHandler.CheckCollisionMap(mapManager);
            _sprite.CollisionHandler.CheckCollisionSprite(mapManager);

            //Console.WriteLine(_sprite.CollisionHandler.IsGrounded);

            //Console.WriteLine(_sprite.Mover.Velocity);
            NextPosition = _sprite.Position + Velocity;
        }

        private void ApplyDrag(GameTime gametime)
        {
            if (_sprite.CollisionHandler.IsGrounded)
            {
                _velocityX += -3f * _velocityX * (float)gametime.ElapsedGameTime.TotalSeconds;
                if (Math.Abs(_velocityX) < 0.1f)
                {
                    _velocityX = 0f;
                }
            }
        }

        private void ClampVelocity()
        {
            if (_velocityX > _sprite.Speed)
                _velocityX = _sprite.Speed;
            if (_velocityX < -_sprite.Speed)
                _velocityX = -_sprite.Speed;
            if (_velocityY > 20f)
                _velocityY = 20f;
            if (_velocityY < -20f)
                _velocityY = -20f;
        }

        private void ApplyGravity(GameTime gametime)
        {
            _velocityY += _gravity * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
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
            if (_sprite is IHasInput hasInput && Keyboard.GetState().IsKeyDown(hasInput.Inputs.Jump) && !_isJumping)
            {
                _jumpVelocity.Y = (float)Math.Sqrt(_sprite.JumpHeight * 2f * gravity);
                _isJumping = true;
            }

            _jumpVelocity.Y += gravity;
        }
    }
}
