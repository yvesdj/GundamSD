using GundamSD.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Movement
{
    public class CollisionHandler
    {
        private ISprite _sprite;

        public bool IsGrounded { get; set; }



        public CollisionHandler(ISprite sprite)
        {
            _sprite = sprite;
        }

        public void CheckCollision(List<Rectangle> collisionBoxes)
        {
            foreach (Rectangle box in collisionBoxes)
            {
                if (_sprite.HitBox.Intersects(box))
                {
                    if (_sprite.Mover.Velocity.X < 0 && _sprite.HitBox.Right > box.Right)//left
                    {
                        _sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X + _sprite.Speed, _sprite.Mover.NextPosition.Y);
                        _sprite.Mover.Velocity = new Vector2(0, _sprite.Mover.Velocity.Y);
                    }
                    else if (_sprite.Mover.Velocity.X > 0 && _sprite.HitBox.Left < box.Left)
                    {
                        _sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X - _sprite.Speed, _sprite.Mover.NextPosition.Y);
                        _sprite.Mover.Velocity = new Vector2(0, _sprite.Mover.Velocity.Y);
                    }

                    
                    if (_sprite.Mover.Velocity.Y < 0 && _sprite.HitBox.Top > box.Top)
                    {
                        _sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X, _sprite.Mover.NextPosition.Y + _sprite.Speed);
                        _sprite.Mover.Velocity = new Vector2(_sprite.Mover.Velocity.X, 0);
                    }
                    else if (_sprite.Mover.Velocity.Y > 0 && _sprite.HitBox.Bottom < box.Bottom)
                    {
                        _sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X, _sprite.Mover.NextPosition.Y - _sprite.Speed);
                        _sprite.Mover.Velocity = new Vector2(_sprite.Mover.Velocity.X, 0);

                        IsGrounded = true;
                    }
                }
            }
        }
    }
}
