namespace GundamSD.Animations
{
    public interface IAnimationAtlasAction
    {
        int EndFrame { get; set; }
        bool ShouldHold { get; set; }
        int StartFrame { get; set; }
    }
}