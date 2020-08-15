using GundamSD.Models;
using System.Collections.Generic;

namespace GundamSD.Animations
{
    public class AtlasAnimationSingleActionManager : AnimationAtlasManager
    {
        public AtlasAnimationSingleActionManager(ISprite sprite, Dictionary<string, IAnimationAtlasAction> actions) : base(sprite, actions)
        {

        }

        public override void SetAnimation()
        {
            AtlasPlayer.Play(_actions["SingleAction"]);
        }
    }
}
