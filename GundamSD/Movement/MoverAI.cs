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
            foreach (Rectangle Waypoint in Waypoints)
            {
                if (Sprite.Position.X > Waypoint.X)
                {
                    VelocityX += -Sprite.Speed * 2 * (float)gametime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }
}
