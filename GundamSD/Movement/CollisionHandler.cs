using GundamSD.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Movement
{
    public static class CollisionHandler
    {
        //private List<Rectangle> _collisionBoxes;

        public static void CheckCollision(ISprite sprite, List<Rectangle> collisionBoxes)
        {
            foreach (Rectangle box in collisionBoxes)
            {
                Console.WriteLine(sprite.HitBox);
                if (sprite.Mover.Velocity.X < 0) //player going left
                {
                    if (sprite.HitBox.Intersects(box))
                    {
                        Console.WriteLine("Collision to the left");
                        sprite.Mover.ResetVelocity();
                        sprite.Position += new Vector2(3f, 0);
                    }
                }
                else if (sprite.Mover.Velocity.X > 0)
                {
                    if (sprite.HitBox.Intersects(box))
                    {
                        Console.WriteLine("Collision to the right");
                        sprite.Mover.ResetVelocity();
                        sprite.Position += new Vector2(-3f, 0);
                    }
                }

                if (sprite.Mover.Velocity.Y < 0) //player going up
                {
                    if (sprite.HitBox.Intersects(box))
                    {
                        Console.WriteLine("Collision to the left");
                        sprite.Mover.ResetVelocity();
                        sprite.Position += new Vector2(0, 3f);
                    }
                }
                else if (sprite.Mover.Velocity.Y > 0)
                {
                    if (sprite.HitBox.Intersects(box))
                    {
                        Console.WriteLine("Collision to the right");
                        sprite.Mover.ResetVelocity();
                        sprite.Position += new Vector2(0, -3f);
                    }
                }
            }
        }
    }
}
