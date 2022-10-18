using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToolBoxWindow : EditorWindow
{
    public GUISkin customSkin;

    private string assetPath = "No Path Referenced";
    private string pathToDisplay = "";
    private string assetName = "No Asset Selected";

    [MenuItem("Toolbox/Data")]
    static void InitDataWindow()
    {
        ToolBoxWindow window = GetWindow<ToolBoxWindow>();
        window.titleContent = new GUIContent("Data");
        window.Show();
    }

        Vector2 enemyDataScrollPos;
        Vector2 itemDataScrollPos;
        Vector2 weaponDataScrollPos;


    private void OnGUI()
    {
        GUI.skin = customSkin;


        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginVertical(assetPath, "window", GUILayout.Width(300), GUILayout.ExpandHeight(true));
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Open"))
        {
            assetPath = EditorUtility.OpenFolderPanel("Open data folder", "", "");
        }
        if (GUILayout.Button("Delete"))
        {
            EditorUtility.DisplayDialogComplex("WARNING : This action can't be undo", "Petit pas", "Yes", "No", "Cancel");
        }
        GUILayout.EndHorizontal();
        GUILayout.Button("New Set of Database");
        GUILayout.Button("Use This Database");
        GUILayout.Space(30);

        GUILayout.BeginHorizontal();
        GUILayout.Label("PlayerData");
        GUILayout.Button("Add", GUILayout.Width(75));
        GUILayout.EndHorizontal();
        // PlayerData
        GUILayout.Button("BOUTON 0", "BorderlessButton");
        
        GUILayout.Space(20);
        // EnemyData
        GUILayout.BeginHorizontal();
        GUILayout.Label("EnemyData");
        GUILayout.Button("Add", GUILayout.Width(75));
        GUILayout.EndHorizontal();
        enemyDataScrollPos = EditorGUILayout.BeginScrollView(enemyDataScrollPos, "window", GUILayout.MaxHeight(100));
        GUILayout.Button("BOUTON 0", "BorderlessButton");
        GUILayout.Button("BOUTON 1", "BorderlessButton");
        GUILayout.Button("BOUTON 2", "BorderlessButton");
        GUILayout.Button("BOUTON 3", "BorderlessButton");
        GUILayout.Button("BOUTON 4", "BorderlessButton");
        EditorGUILayout.EndScrollView();

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Label("ItemDataData");
        GUILayout.Button("Add", GUILayout.Width(75));
        GUILayout.EndHorizontal();
        // ItemData
        itemDataScrollPos = EditorGUILayout.BeginScrollView(itemDataScrollPos, "window", GUILayout.MaxHeight(100));
        GUILayout.Button("BOUTON 0", "BorderlessButton");
        GUILayout.Button("BOUTON 1", "BorderlessButton");
        GUILayout.Button("BOUTON 2", "BorderlessButton");
        GUILayout.Button("BOUTON 3", "BorderlessButton");
        GUILayout.Button("BOUTON 4", "BorderlessButton");
        EditorGUILayout.EndScrollView();

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Label("WeaponData");
        GUILayout.Button("Add", GUILayout.Width(75));
        GUILayout.EndHorizontal();
        // WeaponData
        weaponDataScrollPos = EditorGUILayout.BeginScrollView(weaponDataScrollPos, "window", GUILayout.MaxHeight(100));
        GUILayout.Button("BOUTON 0", "BorderlessButton");
        GUILayout.Button("BOUTON 1", "BorderlessButton");
        GUILayout.Button("BOUTON 2", "BorderlessButton");
        GUILayout.Button("BOUTON 3", "BorderlessButton");
        GUILayout.Button("BOUTON 4", "BorderlessButton");
        EditorGUILayout.EndScrollView();

        GUILayout.EndVertical();
        GUILayout.Space(40);
        GUILayout.BeginVertical(assetName, "window", GUILayout.ExpandHeight(true));

        GUILayout.Label("TEST");
       
        GUILayout.EndVertical();
        GUILayout.Space(10);
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.EndVertical();
    }
}
