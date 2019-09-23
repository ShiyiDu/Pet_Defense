using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScrSpider)), CanEditMultipleObjects]
public class ScrSpiderEditor : Editor
{
	protected virtual void OnSceneGUI()
	{
		ScrSpider spider = (ScrSpider)target;

		//EditorGUI.BeginChangeCheck();;
		for (int i = 0; i < spider.patrolPositions.Length; i++) {
			EditorGUI.BeginChangeCheck();
			Vector3 newTargetPosition = Handles.PositionHandle(spider.patrolPositions[i], Quaternion.identity);
			//newTargetPosition.y = spider.transform.position.y;
			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObject(spider, "change patrolling routes");
				spider.patrolPositions[i] = newTargetPosition;
				//spider.Update()
			}
		}
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if (GUILayout.Button("Snap to x-axis")) {
			ScrSpider spider = (ScrSpider)target;
			float y = spider.transform.position.y;
			for (int i = 0; i < spider.patrolPositions.Length; i++) {
				spider.patrolPositions[i].y = y;
			}
			Undo.RecordObject(spider, "change patrolling routes");
		} else if (GUILayout.Button("Snap to y-axis")) {
			ScrSpider spider = (ScrSpider)target;
			float x = spider.transform.position.x;
			for (int i = 0; i < spider.patrolPositions.Length; i++) {
				spider.patrolPositions[i].x = x;
			}
			Undo.RecordObject(spider, "change patrolling routes");
		}
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
