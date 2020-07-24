using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Animations
{
    public class AnimationAtlasAction : IAnimationAtlasAction
    {
        
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public bool ShouldHold { get; set; }


        //public AnimationAtlasAction(int startFrame, int endFrame)
        //{
            
        //    StartFrame = startFrame;
        //    EndFrame = endFrame;
            
        //}

        public AnimationAtlasAction(int startFrame, int endFrame, bool shouldHold)
        {

            StartFrame = startFrame;
            EndFrame = endFrame;
            ShouldHold = shouldHold;

        }
    }
}
