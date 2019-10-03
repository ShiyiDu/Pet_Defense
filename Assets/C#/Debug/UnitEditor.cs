using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnitBehaviour), true), CanEditMultipleObjects]
public class UnitEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        UnitBehaviour unit = (UnitBehaviour)target;

        if (unit.routePoints.Length <= 0) return;

        Handles.color = Color.red;

        Handles.DrawLine(unit.transform.position, unit.routePoints[0]);

        for (int i = 0; i < unit.routePoints.Length - 1; i++) {
            Handles.DrawLine(unit.routePoints[i], unit.routePoints[i + 1]);
        }

        //change target position
        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(unit.destination, Quaternion.identity);

        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(unit, "change routes");
            unit.destination = newTargetPosition;
            unit.routePoints = PetUtility.FindRoute(unit.transform.position, unit.destination);
            //spider.Update()
        }

        Handles.Label(unit.destination, unit.gameObject.name + " destination");
        Handles.DrawLine(unit.destination, unit.transform.position);
        //for (int i = 0; i < whiteGhost.routePoints.Length; i++) {
        //    EditorGUI.BeginChangeCheck();
        //    Vector3 newTargetPosition = Handles.PositionHandle(whiteGhost.routePoints[i], Quaternion.identity);

        //    if (EditorGUI.EndChangeCheck()) {
        //        Undo.RecordObject(whiteGhost, "change routes");
        //        whiteGhost.routePoints[i] = newTargetPosition;
        //        //spider.Update()
        //    }
        //}
    }
}
