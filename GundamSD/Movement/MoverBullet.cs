using System;
using GundamSD.Maps;
using GundamSD.Models;
using Microsoft.Xna.Framework;

namespace GundamSD.Movement
{
    public class MoverBullet : IMover
    {
        public ISprite Sprite { get; set; }

        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        public Vector2 Velocity { get; set; }
        public Vector2 NextPosition { get; set; }

        public bool IsMovingLeft { get; private set; }
        public bool IsMovingRight { get; private set; }

        public MoverBullet(ISprite sprite)
        {
            Sprite = sprite;
        }

        public void Move(GameTime gameTime, MapManager mapManager)
        {
            VelocityX = Sprite.Speed;
            VelocityY = 0;
            Velocity = new Vector2(VelocityX, VelocityY);

            Sprite.CollisionHandler.CheckCollisionSprite(mapManager);

            NextPosition = Sprite.Position + Velocity;
        }

        public void ResetVelocity()
        {
            Velocity = Vector2.Zero;
        }

        public void UpdatePosition()
        {
            Sprite.Position = NextPosition;
        }
    }
}
