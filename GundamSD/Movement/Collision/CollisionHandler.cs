﻿using GundamSD.Maps;
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

        public void CheckCollisionMapLayer(MapManager mapManager, string mapLayer)
        {
            List<Rectangle> collisionBoxes = mapManager.GetMapRectangles(mapLayer);
            foreach (Rectangle box in collisionBoxes)
            {
                if (CollisionChecker.IsCollisionBottom(_sprite, box))
                {
                    //_sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X, _sprite.Mover.NextPosition.Y - _sprite.Speed);
                    _sprite.Mover.Velocity = new Vector2(_sprite.Mover.Velocity.X, 0);
                    IsColliding = true;
                    IsGrounded = true;

                }
                else if (CollisionChecker.IsCollisionTop(_sprite, box))
                {
                    //_sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X, _sprite.Mover.NextPosition.Y + _sprite.Speed);
                    _sprite.Mover.Velocity = new Vector2(_sprite.Mover.Velocity.X, 0);
                    IsColliding = true;
                }

                else if (CollisionChecker.IsCollisionRight(_sprite, box))
                {
                    //_sprite.Mover.NextPosition = new Vector2(_sprite.Mover.NextPosition.X - _sprite.Speed, _sprite.Mover.NextPosition.Y);
                    _sprite.Mover.Velocity = new Vector2(0, _sprite.Mover.Velocity.Y);
                    IsColliding = true;

                }
                else if (CollisionChecker.IsCollisionLeft(_sprite, box))
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

        //not sure if sprites should be able to pass eachother, or block path
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
                if (CollisionChecker.IsCollisionSprite(_sprite, otherSprites[i]))
                {
                    IsColliding = true;
                    //Console.WriteLine("COLLIDED WITH SPRITE");
                } else
                    IsColliding = false;
            }
        }

        public bool IsCollisionSprite(MapManager mapManager)
        {
            List<ISprite> otherSprites = mapManager.Sprites;

            for (int i = 0; i < otherSprites.Count; i++)
            {
                if (otherSprites[i] == _sprite)
                {
                    continue;
                }
                //Console.WriteLine(otherSprites[i]);
                if (CollisionChecker.IsCollisionSprite(_sprite, otherSprites[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public ISprite GetOtherSprite(Rectangle hitbox, MapManager mapManager)
        {
            List<ISprite> otherSprites = mapManager.Sprites;

            for (int i = 0; i < otherSprites.Count; i++)
            {
                if (otherSprites[i] == _sprite)
                {
                    continue;
                }
                if (CollisionChecker.IsCollisionMelee(hitbox, otherSprites[i]))
                {
                    //Console.WriteLine(otherSprites[i]);
                    //otherSprites[i].HealthHandler.TakeDamage(10);
                    return otherSprites[i];
                }
            }
            return null;
            //return false;
        }
    }
}