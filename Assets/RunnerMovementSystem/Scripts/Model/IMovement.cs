
namespace RunnerMovementSystem.Model
{
    public interface IMovement
    {
        float Offset { get; }

        void Update();
        void MoveForward();
        void SetOffset(float offset);
    }
}