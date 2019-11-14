using GundamSD.Models;
using GundamSD.Movement;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD
{
    public static class Factory
    {
        public static IAnimation CreateAnimation()
        {
            return new Animation();
        }

        public static IInput CreateInput()
        {
            return new Input();
        }

        public static IMover CreateMover(Sprite sprite)
        {
            return new Mover(sprite);
        }
    }
}
