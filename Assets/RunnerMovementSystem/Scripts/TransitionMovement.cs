using UnityEngine;
using UnityEngine.Events;
using PathCreation;

namespace RunnerMovementSystem
{
    [RequireComponent(typeof(MovementBehaviour))]
    internal class TransitionMovement : MonoBehaviour, IMovement
    {
        private MovementBehaviour _movementBehaviour;
        private TransitionSegment _transitionSegment;

        public event UnityAction<TransitionSegment> EndReached;

        public float Offset => _movementBehaviour.Offset;

        private void Awake()
        {
            _movementBehaviour = GetComponent<MovementBehaviour>();
            enabled = false;
        }

        private void Update()
        {
            _movementBehaviour.MoveForward();

            if (_movementBehaviour.EndReached)
            {
                EndReached?.Invoke(_transitionSegment);
                enabled = false;
            }
            else
            {
                _movementBehaviour.UpdateTransform();
            }
        }

        public void Init(TransitionSegment transitionSegment, MovementOptions movementOptions)
        {
            _transitionSegment = transitionSegment;
            _movementBehaviour.Init(transitionSegment, movementOptions);

            enabled = true;
        }

        public void MoveForward() { }

        public void SetOffset(float offset)
        {
            _movementBehaviour.SetOffset(offset);
        }
    }
}