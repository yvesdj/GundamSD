using GundamSD.Animations;
using GundamSD.Models;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
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
        public static ISprite CreateSprite(AnimationAtlas atlas, Dictionary<string, AnimationAtlasAction> actions, /*bool isplayer,*/ Vector2 spawnPoint)
        {
            return new Sprite(atlas, actions, /*isPlayer,*/ spawnPoint);
        }

        public static IPlayer CreatePlayer(ISprite playerSprite)
        {
            return new Player(playerSprite);
        }

        //public static IAnimationManager CreateAnimationManager(IAnimation animation)
        //{
        //    return new AnimationManager(animation);
        //}

        //public static IAnimation CreateAnimation()
        //{
        //    return new Animation();
        //}

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
