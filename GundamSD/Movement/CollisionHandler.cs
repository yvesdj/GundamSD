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

        public void CheckCollisionMap(MapManager mapManager)
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
                }
                else
                {
                    IsColliding = false;
                }
            }
        }

        public void CheckCollisionSprite(MapManager mapManager)
        {
            List<ISprite> otherSprites = mapManager.Sprites;

            for (int i = 0; i < otherSprites.Count; i++)
            {
                if (otherSprites[i] == _sprite)
                {
                    continue;
                }
                //Console.WriteLine(otherSprites[i]);
                if (_sprite.HitBox.Intersects(otherSprites[i].HitBox))
                {
                    Console.WriteLine("COLLIDED WITH SPRITE");
                }
            }
            
        }
    }
}