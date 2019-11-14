using Microsoft.Xna.Framework.Graphics;

namespace GundamSD
{
    public interface IAnimation
    {
        int CurrentFrame { get; set; }
        int FrameCount { get; set; } // shouldn't have a set
        int FrameHeight { get; }
        float FrameSpeed { get; set; }
        int FrameWidth { get; }
        bool IsLooping { get; set; }
        Texture2D Texture { get; set; } // shouldn't have a set
    }
}