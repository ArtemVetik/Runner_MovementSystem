using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PathCreationTools.Editor
{
    [CustomEditor(typeof(PathObject))]
    [CanEditMultipleObjects]
    public class PathObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Attach"))
                AttachAllTargets();
            if (GUILayout.Button("Attach Center"))
                AttachCenterAllTargets();
            if (GUILayout.Button("Update position"))
                UpdateAllTargets();
                
            GUILayout.EndHorizontal();


            base.OnInspectorGUI();
        }

        private void AttachAllTargets()
        {
            foreach (var target in targets)
                (target as PathObject).Attach();
        }

        private void AttachCenterAllTargets()
        {
            foreach (var target in targets)
                (target as PathObject).AttachCenter();
        }

        private void UpdateAllTargets()
        {
            foreach (var target in targets)
                (target as PathObject).UpdatePosition();
        }
    }
}