using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BrowserResources : EditorWindow
{
    private static List<string> allFolderName = new List<string>();
    private string newNameSet = "NewDataBase";
    private Vector2 scrollview;

    public static void InitBrowserWindow()
    {
        BrowserResources window = GetWindow<BrowserResources>();
        window.titleContent = new GUIContent("Folder Browser [Assets/Resources/ ... ]");
        window.maxSize = new Vector2(300f, 500f);
        window.minSize = window.maxSize;
        allFolderName.Clear();

        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Data");
        DirectoryInfo[] info = dir.GetDirectories();

        foreach(DirectoryInfo f in info)
        {
            allFolderName.Add(f.Name);
        }

        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Chose a configuration :");
        GUILayout.Space(20);

        scrollview = GUILayout.BeginScrollView(scrollview);

        foreach(string str in allFolderName)
        {
            if (GUILayout.Button(str)){
                DataManager.OpenDatabase("Data/" + str);
                this.Close();
            }
        }

        GUILayout.EndScrollView();

        GUILayout.Space(40);
        GUILayout.BeginHorizontal();
        newNameSet = GUILayout.TextField(newNameSet, GUILayout.Width(130));
        if(GUILayout.Button("New Set of Database"))
        {
            string path = AssetDatabase.CreateFolder("Assets/Resources/Data", newNameSet);
            if (path != "")
            {
                ScrPlayerData asset = ScriptableObject.CreateInstance<ScrPlayerData>();

                AssetDatabase.CreateAsset(asset, "Assets/Resources/Data/B");
                /*AssetDatabase.CreateAsset(ScrItemsData.CreateInstance("ItemsData"), path);
                AssetDatabase.CreateAsset(ScrListEnemy.CreateInstance("ListEnemy"), path);
                AssetDatabase.CreateAsset(ScrWeaponsData.CreateInstance("WeaponsData"), path);*/
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                //DataManager.OpenDatabase("Data/" + newNameSet);
                //this.Close();
            }
            else
                EditorUtility.DisplayDialog("Bite","Bite","ok");
        }
        GUILayout.EndHorizontal();
    }
}
