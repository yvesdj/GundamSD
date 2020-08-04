using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public class Grunt : Sprite
    {
        public Grunt(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction WalkRight = Factory.CreateAnimAtlasAction(0, 3, false);
            IAnimationAtlasAction Jump = Factory.CreateAnimAtlasAction(5, 6, true);
            IAnimationAtlasAction Attack = Factory.CreateAnimAtlasAction(20, 22, false);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "WalkRight", WalkRight },
                { "Jump", Jump },
                { "Attack", Attack },
            };
            _atlasManager = Factory.CreateAnimAtlasManager(this, actions);
            Inputs = Factory.CreateInput();
            HealthHandler = Factory.CreateHealthHandler(this, Color.Red);
        }
    }
}
