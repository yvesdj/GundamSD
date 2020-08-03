using Microsoft.Xna.Framework;

namespace GundamSD.Movement
{
    public interface IMover
    {
        Vector2 Velocity { get; set; }
        Vector2 NextPosition { get; set; }
        void Move(GameTime gameTime);
        void UpdatePosition();
        void ResetVelocity();
    }
}