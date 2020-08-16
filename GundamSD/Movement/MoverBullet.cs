using System;
using GundamSD.Maps;
using GundamSD.Models;
using Microsoft.Xna.Framework;

namespace GundamSD.Movement
{
    public class MoverBullet : IMover
    {
        private ISprite _sprite;

        private float _velocityX;
        private float _velocityY;

        public Vector2 Velocity { get; set; }
        public Vector2 NextPosition { get; set; }

        public MoverBullet(ISprite sprite)
        {
            _sprite = sprite;
        }

        public void Move(GameTime gameTime, MapManager mapManager)
        {
            _velocityX = _sprite.Speed;
            _velocityY = 0;
            Velocity = new Vector2(_velocityX, _velocityY);

            //if (_sprite.CollisionHandler.IsCollisionSprite(mapManager))
            //{
            //    if (_sprite is Bullet bullet)
            //    {
            //        Console.WriteLine("Attacked with " + bullet.Damage + "Damage.");
            //    }
            //}
            _sprite.CollisionHandler.CheckCollisionSprite(mapManager);

            NextPosition = _sprite.Position + Velocity;
        }

        public void ResetVelocity()
        {
            Velocity = Vector2.Zero;
        }

        public void UpdatePosition()
        {
            _sprite.Position = NextPosition;
        }
    }
}
