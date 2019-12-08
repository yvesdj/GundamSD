using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Animations
{
    public class AnimationAtlasAction
    {
        private AnimationAtlas _atlas;
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public string ActionName { get; set; }

        public AnimationAtlasAction(AnimationAtlas atlas, int startFrame, int endFrame, string actionName)
        {
            _atlas = atlas;
            StartFrame = startFrame;
            EndFrame = endFrame;
            ActionName = actionName;
        }
    }
}
