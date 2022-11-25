using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

[CanEditMultipleObjects]
[CustomEditor(typeof(NPCController))]
[System.Serializable]
public class NPCControllerEditor : Editor
{
    private NPCController myObject;
    private ScrItemsData itemDataBase;
    private string[] itemChoices;
    private int choice = 0;
    private SerializedProperty nameProperty, sentencesProperty, altSentencesProperty, stateProperty; 

    private ReorderableList zoneList, itemList, audioList;


    private void OnEnable()
    {
        myObject = (NPCController)target;

        nameProperty = serializedObject.FindProperty("NPCname");
        sentencesProperty = serializedObject.FindProperty("sentences");
        altSentencesProperty = serializedObject.FindProperty("altSentences");
        stateProperty = serializedObject.FindProperty("state");

        zoneList = new ReorderableList(serializedObject, serializedObject.FindProperty("zoneToUnlock"), true, true, true, true);
        itemList = new ReorderableList(serializedObject, serializedObject.FindProperty("itemToGive"), true, true, true, true);
        audioList = new ReorderableList(serializedObject, serializedObject.FindProperty("sons"), true, true, true, true);

        zoneList.drawElementCallback = DrawListZones;
        zoneList.drawHeaderCallback = DrawHeaderZones;
        itemList.drawElementCallback = DrawListItems;
        itemList.drawHeaderCallback = DrawHeaderItems;
        audioList.drawElementCallback = DrawListAudio;
        audioList.drawHeaderCallback = DrawHeaderAudio;


        /*try
        {
            itemDataBase = (ScrItemsData)Resources.Load("Data/DefaultData/ItemsData");
            itemChoices = new string[itemDataBase.itemsData.Count];
            for(int i = 0; i< itemDataBase.itemsData.Count; i++)
            {
                itemChoices[i] = itemDataBase.itemsData[i].name;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("no file found");
        }*/
    }
    void DrawListZones(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = zoneList.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Zone "+index);
        EditorGUI.PropertyField(
            new Rect(rect.x+ 50, rect.y, 100, EditorGUIUtility.singleLineHeight),
            element,
            GUIContent.none);
    }

    void DrawHeaderZones(Rect rect)
    {
        string name = "Zones";
        EditorGUI.LabelField(rect, name);
    }

    void DrawListAudio(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = audioList.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Son " + index);
        EditorGUI.PropertyField(
            new Rect(rect.x + 50, rect.y, 100, EditorGUIUtility.singleLineHeight),
            element,
            GUIContent.none);
    }

    void DrawHeaderAudio(Rect rect)
    {
        string name = "Sons";
        EditorGUI.LabelField(rect, name);
    }

    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = itemList.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Item"+index);

        EditorGUI.LabelField(new Rect(rect.x+50, rect.y, 100, EditorGUIUtility.singleLineHeight), "Name");
        EditorGUI.PropertyField(
            new Rect(rect.x+100, rect.y, 100, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("name"),
            GUIContent.none);

        EditorGUI.LabelField(new Rect(rect.x+230, rect.y, 100, EditorGUIUtility.singleLineHeight), "Quantity");
        EditorGUI.PropertyField(
            new Rect(rect.x+300, rect.y, 40, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("currentStack"),
            GUIContent.none);
    }

    void DrawHeaderItems(Rect rect)
    {
        string name = "Items";
        EditorGUI.LabelField(rect, name);
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("NPC");
        EditorGUILayout.EndHorizontal();

        
        nameProperty.stringValue = EditorGUILayout.TextField("Name :", nameProperty.stringValue);

        EditorGUILayout.Space();
        stateProperty.enumValueIndex = (int)(NPCState)EditorGUILayout.EnumPopup("NPCState state", (NPCState)Enum.GetValues(typeof(NPCState)).GetValue(stateProperty.enumValueIndex));


        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Dialogues");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Placer \"$*$\" pour séparer les phrases", MessageType.Info);

        GUIStyle areaStyle = GUI.skin.textArea;
        sentencesProperty.stringValue = EditorGUILayout.TextField("Dialogue", sentencesProperty.stringValue, areaStyle, GUILayout.Height(80));

        if(myObject.state != NPCState.NONE)
            altSentencesProperty.stringValue = EditorGUILayout.TextField("Alternative Dialogue", altSentencesProperty.stringValue, areaStyle, GUILayout.Height(80));

        EditorGUILayout.Space();


        

        if (myObject.state == NPCState.GIVER)
            itemList.DoLayoutList();
        else if (myObject.state == NPCState.ZONER)
            zoneList.DoLayoutList();

        audioList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

    }
}
