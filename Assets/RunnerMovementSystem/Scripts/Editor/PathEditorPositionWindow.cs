using System;
using UnityEngine;
using UnityEditor;
using PathCreation;

namespace RunnerMovementSystem.Editor
{
    public class PathEditorPositionWindow : EditorWindow
    {
        private class AnchorPoint
        {
            public PathSegment Path;
            public float Distance;
            public float Offset;
        }

        private static PathSegment _selfPathSegment;
        private static PathCreator _selfPathCreator;
        private static AnchorPoint _startPoint;
        private static AnchorPoint _endPoint;

        public static void OpenWindow(PathSegment pathSegment)
        {
            _selfPathSegment = pathSegment;
            _selfPathCreator = _selfPathSegment.GetComponent<PathCreator>();

            _startPoint = new AnchorPoint();
            _endPoint = new AnchorPoint();

            var window = GetWindow<PathEditorPositionWindow>("Path Editor Position Window");
        }
        
        [Obsolete]
        private void OnGUI()
        {
            if (GUILayout.Button("Auto Setup"))
            {
                _startPoint = GetNearestPoint(_selfPathCreator.path.GetPointAtDistance(0));
                _endPoint = GetNearestPoint(_selfPathCreator.path.GetPointAtDistance(_selfPathCreator.path.length, EndOfPathInstruction.Stop));
            }

            GUILayout.Label("Start Point:");
            DrawAnchorPoint(_startPoint);
            GUILayout.Space(20);
            GUILayout.Label("End Point:");
            DrawAnchorPoint(_endPoint);

            if (_startPoint.Path != null)
                UpdatePosition(0, _startPoint);
            if (_endPoint.Path != null)
                UpdatePosition(_selfPathCreator.bezierPath.NumPoints - 1, _endPoint);
        }

        [Obsolete]
        private void DrawAnchorPoint(AnchorPoint point)
        {
            point.Path = EditorGUILayout.ObjectField("PathSegment", point.Path, typeof(PathSegment)) as PathSegment;
            point.Distance = EditorGUILayout.FloatField("Distance", point.Distance);
            point.Offset = EditorGUILayout.FloatField("Offset", point.Offset);
        }

        private void UpdatePosition(int pointIndex, AnchorPoint position)
        {
            var pathCreator = position.Path.GetComponent<PathCreator>();
            var targetPoint = pathCreator.path.GetPointAtDistance(position.Distance);
            var normal = pathCreator.path.GetNormalAtDistance(position.Distance);
            targetPoint += normal * position.Offset;

            _selfPathCreator.bezierPath.SetPoint(pointIndex, _selfPathCreator.transform.InverseTransformPoint(targetPoint));
        }

        private AnchorPoint GetNearestPoint(Vector3 position)
        {
            var allPaths = FindObjectsOfType<PathSegment>();

            PathSegment nearestPath = null;
            var minDistance = float.MaxValue;

            foreach (var path in allPaths)
            {
                if (path == _selfPathSegment)
                    continue;

                var pathCreator = path.GetComponent<PathCreator>();

                var nearestPoint = pathCreator.path.GetClosestPointOnPath(position);
                var distance = Vector3.Distance(nearestPoint, position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPath = path;
                }
            }

            AnchorPoint point = new AnchorPoint();
            point.Path = nearestPath;
            point.Distance = nearestPath.GetComponent<PathCreator>().path.GetClosestDistanceAlongPath(position);
            point.Offset = 0f;

            return point;
        }
    }
}