using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PathCreationTools.Editor
{
    [CustomEditor(typeof(PathObjectRedactor))]
    public class PathObjectRedactorEditor : UnityEditor.Editor
    {
        private PathObjectRedactor _pathObjectRedactor;
        private Vector3 _startDragPosition;
        private KeyCode _currentKeyCode;

        public bool AddAction => _currentKeyCode == KeyCode.LeftShift;
        public bool RemoveAction => _currentKeyCode == KeyCode.LeftControl;

        private void Awake()
        {
            _pathObjectRedactor = target as PathObjectRedactor;
        }

        private void OnEnable()
        {
            Tools.hidden = true;
        }

        private void OnDisable()
        {
            Tools.hidden = false;
        }

        private void OnSceneGUI()
        {
            Tools.hidden = true;
            Event e = Event.current;
            if (e.isMouse && e.button != 0)
                return;

            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            switch (e.GetTypeForControl(controlID))
            {
                case EventType.MouseDown:
                    GUIUtility.hotControl = controlID;
                    OnMouseDown();
                    e.Use();
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                    break;
                case EventType.MouseUp:
                    GUIUtility.hotControl = controlID;
                    OnMouseUp();
                    break;
                case EventType.MouseDrag:
                    GUIUtility.hotControl = controlID;
                    OnMouseDrag();
                    e.Use();
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                    break;
                case EventType.KeyDown:
                    GUIUtility.keyboardControl = controlID;
                    _currentKeyCode = Event.current.keyCode;
                    break;
                case EventType.KeyUp:
                    GUIUtility.keyboardControl = controlID;
                    _currentKeyCode = KeyCode.None;
                    break;
            }
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Bake"))
                _pathObjectRedactor.Bake();
            if (GUILayout.Button("Clear"))
                _pathObjectRedactor.Clear();

            GUILayout.EndHorizontal();

            GUILayout.Label($"Cached: {_pathObjectRedactor.CachedCount} path");

            base.OnInspectorGUI();
        }

        private bool Raycast(out RaycastHit hitInfo)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            return Physics.Raycast(ray, out hitInfo);
        }

        private void OnMouseDown()
        {
            if (Raycast(out RaycastHit hitInfo))
            {
                if (AddAction)
                {
                    _startDragPosition = _pathObjectRedactor.GetNearestPathPoint(hitInfo.point);
                    _pathObjectRedactor.InstantiateObject(hitInfo);
                }
                else if (RemoveAction)
                {
                    _pathObjectRedactor.RemoveObject(hitInfo);
                }
            }
        }

        private void OnMouseUp()
        {
            _startDragPosition = Vector3.one * float.MaxValue;
        }

        private void OnMouseDrag()
        {
            if (Raycast(out RaycastHit hitInfo))
            {
                if (AddAction)
                {
                    var currentPathPoint = _pathObjectRedactor.GetNearestPathPoint(hitInfo.point);
                    if (Vector3.Distance(currentPathPoint, _startDragPosition) < _pathObjectRedactor.DistanceBetweenObjects)
                        return;

                    _startDragPosition = currentPathPoint;
                    _pathObjectRedactor.InstantiateObject(hitInfo);
                }
                else if (RemoveAction)
                {
                    _pathObjectRedactor.RemoveObject(hitInfo);
                }
            }
        }

        protected override void OnHeaderGUI() { }
    }
}