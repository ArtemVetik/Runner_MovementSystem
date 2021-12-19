using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

namespace RunnerMovementSystem
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class TransitionSegment : PathSegment
    {
        [SerializeField] private float _width;

        public override float Width => _width;

        private void OnDrawGizmos()
        {
            var path = GetComponent<PathCreator>();

            var saveColor = Gizmos.color;
            Gizmos.color = Color.yellow;
            for (int distance = 0; distance < path.path.length; distance++)
            {
                var point = path.path.GetPointAtDistance(distance);
                var normal = path.path.GetNormalAtDistance(distance);
                Gizmos.DrawLine(point - normal * Width, point + normal * Width);
            }
            Gizmos.color = saveColor;
        }

        protected override void OnAwake()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MovementSystem movement))
            {
                movement.Transit(this);
            }
        }
    }
}