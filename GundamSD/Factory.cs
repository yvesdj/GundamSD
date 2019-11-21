using GundamSD.Animations;
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
        public static ISprite CreateSprite(Dictionary<string, IAnimation> animations)
        {
            return new Sprite(animations);
        }

        public static IAnimationManager CreateAnimationManager(IAnimation animation)
        {
            return new AnimationManager(animation);
        }

        public static IAnimation CreateAnimation()
        {
            return new Animation();
        }

        public static IInput CreateInput()
        {
            return new Input();
        }

        public static IMover CreateMover(ISprite sprite)
        {
            return new Mover(sprite);
        }
    }
}
