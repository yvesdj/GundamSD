using GundamSD.Movement;

namespace GundamSD.Models
{
    public interface IHasInput
    {
        IInput Inputs { get; set; }
    }
}