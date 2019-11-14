namespace GundamSD.Movement
{
    public interface IMover
    {
        void Move();
        void UpdatePosition();
        void ResetVelocity();
    }
}