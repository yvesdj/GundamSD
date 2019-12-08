using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Animations
{
    public class AnimationAtlasAction
    {
        
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        

        public AnimationAtlasAction(int startFrame, int endFrame)
        {
            
            StartFrame = startFrame;
            EndFrame = endFrame;
            
        }
    }
}
