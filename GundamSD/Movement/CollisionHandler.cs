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
        //private ISprite _sprite;
        //private List<Rectangle> _collisionBoxes;

        public static void CheckCollision(ISprite sprite, List<Rectangle> collisionBoxes)
        {
            foreach (Rectangle box in collisionBoxes)
            {
                if (sprite.Mover.Velocity.X < 0) //player going left
                {
                    if (sprite.HitBox.Intersects(box))
                    {
                        Console.WriteLine("Collision to the left");
                    }
                }
                else if (sprite.Mover.Velocity.X > 0)
                {
                    if (sprite.HitBox.Intersects(box))
                    {
                        Console.WriteLine("Collision to the right");
                    }
                }
            }
        }
    }
}
