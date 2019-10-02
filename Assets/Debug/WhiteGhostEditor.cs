using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Hamster)), CanEditMultipleObjects]
public class WhiteGhostEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        UnitBehaviour whiteGhost = (UnitBehaviour)target;

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
