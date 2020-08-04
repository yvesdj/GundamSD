using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.Animations;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public class Player : Sprite
    {
        public Player(IAnimationAtlas atlas) : base(atlas)
        {
            IAnimationAtlasAction WalkRight = Factory.CreateAnimAtlasAction(0, 3, false);
            IAnimationAtlasAction Jump = Factory.CreateAnimAtlasAction(5, 7, true);
            IAnimationAtlasAction Attack = Factory.CreateAnimAtlasAction(30, 38, false);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "WalkRight", WalkRight },
                { "Jump", Jump },
                { "Attack", Attack },
            };
            _atlasManager = Factory.CreateAnimAtlasManager(this, actions);
            Inputs = Factory.CreateInput();
        }

        public Player(IAnimationAtlas atlas, Dictionary<string, IAnimationAtlasAction> actions) : base(atlas, actions)
        {
            Inputs = Factory.CreateInput();
        }
    }
}
