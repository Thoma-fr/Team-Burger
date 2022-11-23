using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(NPCController))]
public class NPCControllerEditor : Editor
{
    private NPCController myObject = null;
    private string sent, altSent;

    private void OnEnable()
    {
        this.myObject = (NPCController)this.target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("NPC");
        EditorGUILayout.EndHorizontal();

        this.myObject.NPCname = EditorGUILayout.TextField("Name", this.myObject.NPCname);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Dialogues");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Placer \"$*$\" pour séparer les phrases", MessageType.Info);

        GUIStyle areaStyle = GUI.skin.textArea;
        this.myObject.sentences = EditorGUILayout.TextField("Dialogue", this.myObject.sentences, areaStyle, GUILayout.Height(80));
        this.myObject.altSentences = EditorGUILayout.TextField("Alternative Dialogue", this.myObject.altSentences, areaStyle, GUILayout.Height(80));

        EditorGUILayout.Space();

        this.myObject.state = (NPCState)EditorGUILayout.EnumPopup("NPCState state", this.myObject.state);


        DrawDefaultInspector();
    }
}
