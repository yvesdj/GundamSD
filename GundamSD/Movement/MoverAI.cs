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
        public bool IsStatic { get; set; }
        public List<int> WayPointIndexes { get; set; }
        //public List<Rectangle> MyProperty { get; set; }
        private float _timer = 0f;

        public MoverAI(ISprite sprite, bool isStatic) : base(sprite)
        {
            IsMovingLeft = true;
            IsMovingRight = false;

            IsStatic = isStatic;
        }

        public MoverAI(ISprite sprite, bool isStatic, List<int> wayPointIndexes) : base(sprite)
        {
            IsMovingLeft = true;
            IsMovingRight = false;

            IsStatic = isStatic;
            WayPointIndexes = wayPointIndexes;
        }

        public override void Move(GameTime gametime, MapManager mapManager)
        {
            if (!IsStatic)
                ChooseDirection(gametime, mapManager);
            base.Move(gametime, mapManager);
        }

        public override void ProcessInput(GameTime gametime)
        {
            if (Sprite.AtlasManager.IsMeleeAttacking || Sprite.AtlasManager.IsRangedAttacking)
            {
                ResetVelocity();
            }
            else if (!IsStatic)
            {
                if (IsMovingLeft)
                    VelocityX += -Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
                else if (IsMovingRight)
                    VelocityX += Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void ChooseDirection(GameTime gametime, MapManager mapManager)
        {
            List<Rectangle> allWaypoints = mapManager.GetMapRectangles("WayPoints");
            List<Rectangle> wayPoints = GetActiveWayPoints(allWaypoints);

            for (int i = 0; i < wayPoints.Count; i++)
            {
                if (CollisionChecker.IsCollisionWithRectangle(Sprite, wayPoints[0]))
                {
                    IsMovingLeft = false;
                    if (PauseMovement(gametime, 5f))
                        IsMovingRight = true;
                }
                if (CollisionChecker.IsCollisionWithRectangle(Sprite, wayPoints[1]))
                {
                    IsMovingRight = false;
                    if (PauseMovement(gametime, 5f))
                        IsMovingLeft = true;
                }
            }
        }

        private List<Rectangle> GetActiveWayPoints(List<Rectangle> allWaypoints)
        {
            List<Rectangle> wayPoints = new List<Rectangle>();

            for (int i = 0; i < allWaypoints.Count; i++)
            {
                foreach (int index in WayPointIndexes)
                {
                    if (index == i)
                    {
                        wayPoints.Add(allWaypoints[i]);
                    }
                }
            }

            return wayPoints;
        }

        private bool PauseMovement(GameTime gametime, float holdTime)
        {
            ResetVelocity();
          
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            if (_timer >= holdTime)
            {
                _timer = 0f;
                return true;
            }
            return false;
        }

    }
}
