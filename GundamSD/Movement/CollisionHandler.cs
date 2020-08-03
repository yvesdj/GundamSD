using GundamSD.Maps;
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
        public bool IsColliding { get; set; }

        public CollisionHandler(ISprite sprite)
        {
            _sprite = sprite;
        }

        public void CheckCollision(MapManager mapManager)
        {
            List<Rectangle> collisionBoxes = mapManager.GetMapCollidables();
            foreach (Rectangle box in collisionBoxes)
            {
                if (_sprite.Mover.Velocity.Y > 0 &&
                    _sprite.HitBox.Bottom + _sprite.Mover.Velocity.Y > box.Top &&
                    _sprite.HitBox.Top < box.Top &&
                    _sprite.HitBox.Right > box.Left &&
                    _sprite.HitBox.Left < box.Right)
                {
                    //_sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X, _sprite.Mover.NextPosition.Y - _sprite.Speed);
                    _sprite.Mover.Velocity = new Vector2(_sprite.Mover.Velocity.X, 0);
                    IsColliding = true;
                    IsGrounded = true;
                    Console.WriteLine("Collision BOTTOM");

                }
                else if (_sprite.Mover.Velocity.Y < 0 &&
                    _sprite.HitBox.Top + _sprite.Mover.Velocity.Y < box.Bottom &&
                    _sprite.HitBox.Bottom > box.Bottom &&
                    _sprite.HitBox.Right > box.Left &&
                    _sprite.HitBox.Left < box.Right)
                {
                    //_sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X, _sprite.Mover.NextPosition.Y + _sprite.Speed);
                    _sprite.Mover.Velocity = new Vector2(_sprite.Mover.Velocity.X, 0);
                    IsColliding = true;
                    Console.WriteLine("Collision TOP");
                }

                else if (_sprite.Mover.Velocity.X > 0 &&
                    _sprite.HitBox.Right + _sprite.Mover.Velocity.X > box.Left &&
                    _sprite.HitBox.Left < box.Left &&
                    _sprite.HitBox.Bottom > box.Top &&
                    _sprite.HitBox.Top < box.Bottom)
                {
                    //_sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X - _sprite.Speed, _sprite.Mover.NextPosition.Y);
                    _sprite.Mover.Velocity = new Vector2(0, _sprite.Mover.Velocity.Y);
                    IsColliding = true;
                    Console.WriteLine("Collision RIGHT");

                }
                else if (_sprite.Mover.Velocity.X < 0 &&
                    _sprite.HitBox.Left + _sprite.Mover.Velocity.X < box.Right &&
                    _sprite.HitBox.Right > box.Right &&
                    _sprite.HitBox.Bottom > box.Top &&
                    _sprite.HitBox.Top < box.Bottom)
                {
                    //_sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X + _sprite.Speed, _sprite.Mover.NextPosition.Y);
                    _sprite.Mover.Velocity = new Vector2(0, _sprite.Mover.Velocity.Y);
                    IsColliding = true;
                    Console.WriteLine("Collision LEFT");
                }
                else
                {
                    IsColliding = false;
                    //Console.WriteLine("Collision NONE");
                }
            }
        }
    }
}