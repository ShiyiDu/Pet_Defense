﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PetUtility)), CanEditMultipleObjects]
public class UtilityEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        PetUtility utility = (PetUtility)target;
        Handles.color = Color.red;
        //draw the floor marker to indicate which floors
        for (int i = 0; i < utility.floorMarker.Length; i++) {
            Handles.Label(utility.floorMarker[i].position, "floor " + i + " :to " + (utility.floorMarker[i].toRight ? "to right" : "to left"));

            Handles.DrawLine(utility.floorMarker[i].position, utility.floorMarker[i].position + Vector2.right * 20);

            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPosition = Handles.PositionHandle(utility.floorMarker[i].position, Quaternion.identity);

            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(utility, "change floor marker");
                utility.floorMarker[i].position = newTargetPosition;
                //spider.Update()
            }
        }

        if (utility.wayPoints.Length <= 0) return;

        //draw the way points marker for the doors
        for (int i = 0; i < utility.wayPoints.Length - 1; i++) {
            Handles.DrawLine(utility.wayPoints[i], utility.wayPoints[i + 1]);
        }

        for (int i = 0; i < utility.wayPoints.Length; i++) {
            Handles.Label(utility.wayPoints[i], "way point " + i);

            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPosition = Handles.PositionHandle(utility.wayPoints[i], Quaternion.identity);

            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(utility, "change routes");
                utility.wayPoints[i] = newTargetPosition;
                //spider.Update()
            }
        }

        for (int i = 0; i < utility.spawnPoints.Length; i++) {
            Handles.Label(utility.spawnPoints[i], "spawn point " + i);

            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPosition = Handles.PositionHandle(utility.spawnPoints[i], Quaternion.identity);

            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(utility, "change spawn points");
                utility.spawnPoints[i] = newTargetPosition;
                //spider.Update()
            }
        }
    }
}
