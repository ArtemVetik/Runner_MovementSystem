using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RunnerMovementSystem.Editor
{
    [CustomEditor(typeof(TransitionSegment))]
    public class PathSegmentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(15);

            if (GUILayout.Button("Open Editor Position Window"))
            {
                PathEditorPositionWindow.OpenWindow(target as TransitionSegment);
            }
        }
    }
}