using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SceneControl))]
public class CustomSceneControlEditor : Editor {

//	public override void OnInspectorGUI(){
//
//		SceneControl mySceneControl = (SceneControl)target;
//
//		mySceneControl.jumpToCue = EditorGUILayout.Toggle("Jump To Cue", mySceneControl.jumpToCue);
//		mySceneControl.cameraMoveTime[mySceneControl.cueToRun] = EditorGUILayout.FloatField("Camera Move Time", mySceneControl.cameraMoveTime[mySceneControl.cueToRun]);
//
//		EditorGUILayout.LabelField("Cue To Run", mySceneControl.cueToRun.ToString());
//
//		EditorGUILayout.LabelField("Cam Move Value", mySceneControl.cameraMove[mySceneControl.cueToRun].ToString());
//
//	}
}
