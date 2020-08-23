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
        public static ISprite CreateSprite(IAnimationAtlas atlas, Dictionary<string, IAnimationAtlasAction> actions)
        {
            return new Sprite(atlas, actions);
        }

        public static IHealthHandler CreateHealthHandler(ISprite sprite, Color color)
        {
            return new HealthHandler(sprite, color);
        }

        #region Enemies
        public static ISprite CreateGruntMelee(Texture2D atlasTexture)
        {
            return new GruntMelee(atlasTexture);
        }
        #endregion

        #region Player
        //public static ISprite CreatePlayerTest(IAnimationAtlas atlas, Dictionary<string, IAnimationAtlasAction> actions)
        //{
        //    return new Player(atlas, actions);
        //}

        public static ISprite CreatePlayer(Texture2D atlasTexture)
        {
            return new Player(atlasTexture);
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
        public static IAnimationAtlas CreateAnimAtlas(Texture2D texture, int rows, int colums)
        {
            return new AnimationAtlas(texture, rows, colums);
        }

        public static IAnimationAtlasAction CreateAnimAtlasAction(int startFrame, int endFrame, bool shouldHold)
        {
            return new AnimationAtlasAction(startFrame, endFrame, shouldHold);
        }

        public static IAnimationAtlasPlayer CreateAnimAtlasPlayer(IAnimationAtlas atlas, IAnimationAtlasAction action)
        {
            return new AnimationAtlasPlayer(atlas, action);
        }

        public static IAnimationAtlasManager CreateAnimAtlasManager(ISprite sprite, Dictionary<string, IAnimationAtlasAction> actions)
        {
            return new AnimationAtlasManager(sprite, actions);
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
