using UnityEngine;

namespace RunnerMovementSystem
{
    internal class MovementBehaviour : MonoBehaviour
    {
        private PathSegment _pathSegment;
        private MovementOptions _movementOptions;
        private float _distanceTravelled;

        public bool EndReached => _distanceTravelled >= _pathSegment.Length;
        public float Offset { get; private set; }

        public void Init(PathSegment roadSegment, MovementOptions movementOptions)
        {
            _pathSegment = roadSegment;
            _movementOptions = movementOptions;

            _distanceTravelled = _pathSegment.GetClosestDistanceAlongPath(transform.position);

            Offset = _pathSegment.GetOffsetByPosition(transform.position);
            UpdateTransform();
        }

        public void MoveForward()
        {
            var speedRate = _pathSegment.GetSpeedRate(_distanceTravelled / _pathSegment.Length);
            _distanceTravelled += _movementOptions.MoveSpeed * speedRate * Time.deltaTime;
            _distanceTravelled = Mathf.Clamp(_distanceTravelled, 0f, _pathSegment.Length);
        }

        public void SetOffset(float offset)
        {
            var width = _pathSegment.Width - _movementOptions.BorderOffset;
            Offset = Mathf.Clamp(offset, -width, width);
        }

        public void UpdateTransform()
        {
            var targetRotation = _pathSegment.GetRotationAtDistance(_distanceTravelled);
            targetRotation *= Quaternion.Euler(0, 0, 90f);
            targetRotation = _pathSegment.IgnoreRotation.Apply(targetRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _movementOptions.RotationSpeed * Time.deltaTime);

            transform.position = _pathSegment.GetPointAtDistance(_distanceTravelled);
            transform.position += transform.right * Offset;
        }
    }
}
