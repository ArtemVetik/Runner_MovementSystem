using UnityEngine;

namespace RunnerMovementSystem.Examples
{
    public class TestInput : MonoBehaviour
    {
        [SerializeField] private MovementSystem _roadMovement;

        private Vector3 _mousePosition;
        private float _offset;

        private void OnEnable()
        {
            _roadMovement.PathChanged += OnPathChanged;
        }

        private void OnPathChanged()
        {
            _offset = _roadMovement.Offset;
            _mousePosition = Input.mousePosition;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _offset = _roadMovement.Offset;
                _mousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                var offset = Input.mousePosition - _mousePosition;
                _roadMovement.SetOffset(_offset + offset.x * 0.01f);
                _roadMovement.MoveForward();
            }
        }
    }
}