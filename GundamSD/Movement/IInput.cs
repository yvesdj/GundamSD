using Microsoft.Xna.Framework.Input;

namespace GundamSD.Movement
{
    public interface IInput
    {
        Keys Down { get; set; }
        Keys Left { get; set; }
        Keys Right { get; set; }
        Keys Jump { get; set; }
        Keys Attack { get; set; }
    }
}