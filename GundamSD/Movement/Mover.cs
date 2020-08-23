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
        public ISprite Sprite { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 NextPosition { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        private const float _gravity = 9.81f;
        private const float _frameSpeed = 0.15f;

        private float _jumpHeight = 10f;
        private Vector2 _jumpVelocity;
        private float gravity = -9.81f;
        private bool _isJumping;

        public Mover(ISprite sprite)
        {
            Sprite = sprite;
        }

        public virtual void Move(GameTime gametime, MapManager mapManager)
        {
            ProcessInput(gametime);

            ApplyGravity(gametime);
            ApplyDrag(gametime);
            ClampVelocity();

            Velocity = new Vector2(VelocityX, VelocityY);
            Sprite.CollisionHandler.CheckCollisionMapLayer(mapManager, "Collidable");

            NextPosition = Sprite.Position + Velocity;
        }

        public virtual void ProcessInput(GameTime gametime)
        {
            if (Sprite is IHasInput hasInput)
            {
                hasInput.Inputs.GetKeyboardState();
                if (hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Up))
                {
                    VelocityY = -Sprite.Speed;
                    Sprite.CollisionHandler.IsGrounded = false;
                }
                else if (hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Down))
                {
                    VelocityY = Sprite.Speed;
                }
                else if (hasInput.Inputs.KeyIsPressed(hasInput.Inputs.Jump))
                {
                    if (Sprite.CollisionHandler.IsGrounded)
                    {
                        VelocityY = -_jumpHeight;
                    }
                    Sprite.CollisionHandler.IsGrounded = false;
                }

                if (hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Left))
                    VelocityX += -Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
                else if (hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Right))
                    VelocityX += Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;

            }
        }

        private void ApplyDrag(GameTime gametime)
        {
            if (Sprite.CollisionHandler.IsGrounded)
            {
                VelocityX += -3f * VelocityX * (float)gametime.ElapsedGameTime.TotalSeconds;
                if (Math.Abs(VelocityX) < 0.1f)
                {
                    VelocityX = 0f;
                }
            }
        }

        private void ClampVelocity()
        {
            if (VelocityX > Sprite.Speed)
                VelocityX = Sprite.Speed;
            if (VelocityX < -Sprite.Speed)
                VelocityX = -Sprite.Speed;
            if (VelocityY > 20f)
                VelocityY = 20f;
            if (VelocityY < -20f)
                VelocityY = -20f;
        }

        private void ApplyGravity(GameTime gametime)
        {
            VelocityY += _gravity * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
        }

        public void UpdatePosition()
        {
            Sprite.Position = NextPosition;
        }

        public void ResetVelocity()
        {
            Velocity = Vector2.Zero;
        }

        //public void Jump()
        //{
        //    if (Sprite is IHasInput hasInput && Keyboard.GetState().IsKeyDown(hasInput.Inputs.Jump) && !_isJumping)
        //    {
        //        _jumpVelocity.Y = (float)Math.Sqrt(_jumpHeight * 2f * gravity);
        //        _isJumping = true;
        //    }

        //    _jumpVelocity.Y += gravity;
        //}
    }
}
