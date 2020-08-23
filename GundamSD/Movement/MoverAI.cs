using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.Maps;
using GundamSD.Models;
using Microsoft.Xna.Framework;

namespace GundamSD.Movement
{
    public class MoverAI : Mover
    {
        private bool _isMovingLeft = true;
        private bool _isMovingRight;

        public MoverAI(ISprite sprite) : base(sprite)
        {
        }

        public override void Move(GameTime gametime, MapManager mapManager)
        {
            ProcessAIInput(gametime, mapManager);
            base.Move(gametime, mapManager);
        }

        public void ProcessAIInput(GameTime gametime, MapManager mapManager)
        {
            List<Rectangle> Waypoints = mapManager.GetMapRectangles("WayPoints");

            //Sprite.CollisionHandler.CheckCollisionMapLayer(mapManager, "WayPoints");

            if (_isMovingLeft)
                VelocityX += -Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
            else if (_isMovingRight)
                VelocityX += Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < Waypoints.Count; i++)
            {
                if (CollisionChecker.IsCollisionWithRectangle(Sprite, Waypoints[0]))
                {
                    _isMovingLeft = false;
                    _isMovingRight = true;
                }
                if (CollisionChecker.IsCollisionWithRectangle(Sprite, Waypoints[1]))
                {
                    _isMovingLeft = true;
                    _isMovingRight = false;
                }
            }

            //foreach (Rectangle Waypoint in Waypoints)
            //{
            //    if (CollisionChecker.IsCollisionWithRectangle(Sprite, Waypoint))
            //    {
            //        VelocityX = -VelocityX;
            //    }
            //}
            //foreach (Rectangle Waypoint in Waypoints)
            //{
            //    if (Sprite.Position.X > Waypoint.X)
            //    {
            //        VelocityX += -Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
            //    }
            //}
        }
    }
}
