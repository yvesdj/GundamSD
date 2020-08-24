using GundamSD.Maps;
using GundamSD.Models;
using Microsoft.Xna.Framework;

namespace GundamSD.Movement
{
    public interface IMover
    {
        ISprite Sprite { get; set; }
        float VelocityX { get; set; }
        float VelocityY { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 NextPosition { get; set; }
        bool IsMovingLeft { get; }
        bool IsMovingRight { get; }

        void Move(GameTime gameTime, MapManager mapManager);
        void UpdatePosition();
        void ResetVelocity();
    }
}