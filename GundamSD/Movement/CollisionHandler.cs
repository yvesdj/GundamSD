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

        public bool IsColliding { get; set; }

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
                        Console.WriteLine("Collision LEFT");
                        _sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X + 1, _sprite.Mover.NextPosition.Y);
                        _sprite.Mover.Velocity = new Vector2(0, _sprite.Mover.Velocity.Y);
                    }
                    else if (_sprite.Mover.Velocity.X > 0 && _sprite.HitBox.Left < box.Left)
                    {
                        Console.WriteLine("Collision RIGHT");
                        _sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X - 1, _sprite.Mover.NextPosition.Y);
                        _sprite.Mover.Velocity = new Vector2(0, _sprite.Mover.Velocity.Y);
                    }

                    if (_sprite.Mover.Velocity.Y < 0 && _sprite.HitBox.Top > box.Top)
                    {
                        Console.WriteLine("Collision UP");
                        _sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X, _sprite.Mover.NextPosition.Y - 1);
                        _sprite.Mover.Velocity = new Vector2(_sprite.Mover.Velocity.X, 0);
                    }
                    else if (_sprite.Mover.Velocity.Y > 0 && _sprite.HitBox.Bottom < box.Bottom)
                    {
                        Console.WriteLine("Collision DOWN");
                        _sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X, _sprite.Mover.NextPosition.Y + 1);
                        _sprite.Mover.Velocity = new Vector2(_sprite.Mover.Velocity.X, 0);

                    }
                }
            }
        }
    }
}
