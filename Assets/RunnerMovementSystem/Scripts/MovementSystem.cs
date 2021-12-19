using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RunnerMovementSystem
{
    [RequireComponent(typeof(RoadMovement))]
    [RequireComponent(typeof(TransitionMovement))]
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private RoadSegment _firstRoad;
        [SerializeField] private MovementOptions _options;

        private RoadMovement _roadMovement;
        private TransitionMovement _transitionMovement;

        private IMovement _currentMovement;

        public event UnityAction PathChanged;

        public float Offset => _currentMovement.Offset;

        private void Awake()
        {
            _roadMovement = GetComponent<RoadMovement>();
            _transitionMovement = GetComponent<TransitionMovement>();
        }

        private void OnEnable()
        {
            _roadMovement.EndReached += OnRoadEnd;
            _transitionMovement.EndReached += OnTransitionEnd;
        }

        private void OnDisable()
        {
            _roadMovement.EndReached -= OnRoadEnd;
            _transitionMovement.EndReached -= OnTransitionEnd;
        }

        private void Start()
        {
            _roadMovement.Init(_firstRoad, _options);
            _currentMovement = _roadMovement;
        }

        public void MoveForward()
        {
            _currentMovement.MoveForward();
        }

        public void SetOffset(float offset)
        {
            _currentMovement.SetOffset(offset);
        }

        public void Transit(TransitionSegment transition)
        {
            PathChanged?.Invoke();

            _transitionMovement.Init(transition, _options);
            _currentMovement = _transitionMovement;
        }

        private void OnRoadEnd(RoadSegment roadSegment)
        {
            var nearestRoad = roadSegment.GetNearestRoad(transform.position);
            if (nearestRoad == null)
                return;
            
            _roadMovement.Init(nearestRoad, _options);
            _currentMovement = _roadMovement;
            PathChanged?.Invoke();
        }

        private void OnTransitionEnd(TransitionSegment transition)
        {
            var nearestRoad = transition.GetNearestRoad(transform.position);
            _roadMovement.Init(nearestRoad, _options);
            _currentMovement = _roadMovement;
            PathChanged?.Invoke();
        }
    }
}