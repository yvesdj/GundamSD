using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Animations
{
    public interface IAnimationAtlas
    {
        int Columns { get; set; }
        int FrameHeight { get; }
        int FrameWidth { get; }
        int Rows { get; set; }
        Texture2D Texture { get; set; }
        int TotalFrames { get; }
    }
}