using GundamSD.Models;
using Microsoft.Xna.Framework;

namespace GundamSD.Movement
{
    public static class CollisionChecker
    {
        public static bool IsCollisionBottom(ISprite sprite, Rectangle box)
        {
            return sprite.Mover.Velocity.Y > 0 &&
                    sprite.HitBox.Bottom + sprite.Mover.Velocity.Y > box.Top &&
                    sprite.HitBox.Top < box.Top &&
                    sprite.HitBox.Right > box.Left &&
                    sprite.HitBox.Left < box.Right;
        }

        public static bool IsCollisionTop(ISprite sprite, Rectangle box)
        {
            return sprite.Mover.Velocity.Y < 0 &&
                    sprite.HitBox.Top + sprite.Mover.Velocity.Y < box.Bottom &&
                    sprite.HitBox.Bottom > box.Bottom &&
                    sprite.HitBox.Right > box.Left &&
                    sprite.HitBox.Left < box.Right;
        }

        public static bool IsCollisionRight(ISprite sprite, Rectangle box)
        {
            return sprite.Mover.Velocity.X > 0 &&
                    sprite.HitBox.Right + sprite.Mover.Velocity.X > box.Left &&
                    sprite.HitBox.Left < box.Left &&
                    sprite.HitBox.Bottom > box.Top &&
                    sprite.HitBox.Top < box.Bottom;
        }

        public static bool IsCollisionLeft(ISprite sprite, Rectangle box)
        {
            return sprite.Mover.Velocity.X < 0 &&
                    sprite.HitBox.Left + sprite.Mover.Velocity.X < box.Right &&
                    sprite.HitBox.Right > box.Right &&
                    sprite.HitBox.Bottom > box.Top &&
                    sprite.HitBox.Top < box.Bottom;
        }

        public static bool IsCollisionSprite(ISprite sprite, ISprite otherSprite)
        {
            return sprite.HitBox.Intersects(otherSprite.HitBox);
        }

        public static bool IsCollisionMelee(Rectangle hitbox, ISprite otherSprite)
        {
            return hitbox.Intersects(otherSprite.HitBox);
        }
    }
}