
namespace RunnerMovementSystem
{
    public interface IMovement
    {
        float Offset { get; }

        void MoveForward();
        void SetOffset(float offset);
    }
}