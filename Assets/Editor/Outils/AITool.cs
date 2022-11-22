using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NAVAI))]
[CanEditMultipleObjects]
public class AITool : Editor
{
    
    public override void OnInspectorGUI()
    {
        NAVAI mynav = (NAVAI)target;

        EditorGUILayout.BeginHorizontal();
            mynav.myType = (NAVAI.AItype)EditorGUILayout.EnumPopup("Behavior", mynav.myType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
         EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("range");
                mynav.range = EditorGUILayout.Slider(mynav.range, 0, 20);
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("sight");
                mynav.sight = EditorGUILayout.Slider(mynav.sight, 0, 20);
            EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        mynav.speed= EditorGUILayout.FloatField("Speed", mynav.speed);
        mynav.acceleration = EditorGUILayout.FloatField("Acceleration", mynav.acceleration);
        EditorGUILayout.EndHorizontal();
    }


}
