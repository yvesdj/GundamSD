﻿using GundamSD.Animations;
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
        public static ISprite CreateSprite(AnimationAtlas atlas, Dictionary<string, IAnimationAtlasAction> actions, /*bool isplayer,*/ Vector2 spawnPoint)
        {
            return new Sprite(atlas, actions, /*isPlayer,*/ spawnPoint);
        }
        #region Player
        public static IPlayer CreatePlayer(ISprite playerSprite)
        {
            return new Player(playerSprite);
        }

        public static IInput CreateInput()
        {
            return new Input();
        }

        public static IMover CreateMover(ISprite sprite)
        {
            return new Mover(sprite);
        }
        #endregion
        #region Animation
        public static IAnimationAtlasPlayer CreateAnimAtlasPlayer(AnimationAtlas atlas, IAnimationAtlasAction action)
        {
            return new AnimationAtlasPlayer(atlas, action);
        }

        public static IAnimationAtlasAction CreateAnimAtlasAction(int startFrame, int endFrame, bool shouldHold)
        {
            return new AnimationAtlasAction(startFrame, endFrame, shouldHold);
        }
        #endregion

        //public static IAnimationManager CreateAnimationManager(IAnimation animation)
        //{
        //    return new AnimationManager(animation);
        //}

        //public static IAnimation CreateAnimation()
        //{
        //    return new Animation();
        //}
    }
}
