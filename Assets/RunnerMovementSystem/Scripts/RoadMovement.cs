using UnityEngine;
using UnityEngine.Events;

namespace RunnerMovementSystem
{
    [RequireComponent(typeof(MovementBehaviour))]
    internal class RoadMovement : MonoBehaviour, IMovement
    {
        private MovementBehaviour _movementBehaviour;
        private RoadSegment _roadSegment;

        public event UnityAction<RoadSegment> EndReached;

        public float Offset => _movementBehaviour.Offset;

        private void Awake()
        {
            _movementBehaviour = GetComponent<MovementBehaviour>();
        }

        private void Update()
        {
            if (_roadSegment.AutoMoveForward == false)
                return;

            Move();
        }

        public void Init(RoadSegment roadSegment, MovementOptions movementOptions)
        {
            _roadSegment = roadSegment;
            _movementBehaviour.Init(roadSegment, movementOptions);
        }

        public void MoveForward()
        {
            if (_roadSegment.AutoMoveForward)
                return;

            Move();
        }

        public void SetOffset(float offset)
        {
            _movementBehaviour.SetOffset(offset);
        }

        private void Move()
        {
            _movementBehaviour.MoveForward();

            if (_movementBehaviour.EndReached)
                EndReached?.Invoke(_roadSegment);
            else
                _movementBehaviour.UpdateTransform();
        }
    }
}