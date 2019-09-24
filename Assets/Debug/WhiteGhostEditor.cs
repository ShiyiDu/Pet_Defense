using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WhiteGhost)), CanEditMultipleObjects]
public class WhiteGhostEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        WhiteGhost whiteGhost = (WhiteGhost)target;

        for (int i = 0; i < whiteGhost.routePoints.Length; i++) {
            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPosition = Handles.PositionHandle(whiteGhost.routePoints[i], Quaternion.identity);

            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(whiteGhost, "change routes");
                whiteGhost.routePoints[i] = newTargetPosition;
                //spider.Update()
            }
        }
    }
}
