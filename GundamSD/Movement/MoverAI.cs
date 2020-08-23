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
        private float _timer = 0;
        private bool _isMovingLeft = true;
        private bool _isMovingRight;

        public MoverAI(ISprite sprite) : base(sprite)
        {
        }

        public override void Move(GameTime gametime, MapManager mapManager)
        {
            ChooseDirection(gametime, mapManager);
            base.Move(gametime, mapManager);
        }

        public override void ProcessInput(GameTime gametime)
        {
            if (_isMovingLeft)
                VelocityX += -Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
            else if (_isMovingRight)
                VelocityX += Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
        }

        public void ChooseDirection(GameTime gametime, MapManager mapManager)
        {
            List<Rectangle> Waypoints = mapManager.GetMapRectangles("WayPoints");

            for (int i = 0; i < Waypoints.Count; i++)
            {
                if (CollisionChecker.IsCollisionWithRectangle(Sprite, Waypoints[0]))
                {
                    _isMovingLeft = false;
                    if (PauseMovement(gametime, 5f))
                        _isMovingRight = true;
                }
                if (CollisionChecker.IsCollisionWithRectangle(Sprite, Waypoints[1]))
                {
                    _isMovingRight = false;
                    if (PauseMovement(gametime, 5f))
                        _isMovingLeft = true;
                }
            }
        }

        private bool PauseMovement(GameTime gametime, float holdTime)
        {
            ResetVelocity();
          
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            Console.WriteLine(_timer);
            if (_timer >= holdTime)
            {
                _timer = 0f;
                Console.WriteLine("RESET TIMER");
                return true;
            }
            return false;
        }

    }
}
