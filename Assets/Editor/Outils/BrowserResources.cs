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
        if(GUILayout.Button("New Set of Database") && newNameSet != "")
        {
            if (!Directory.Exists(Application.dataPath + "/Resources/Data/" + newNameSet))
            {
                AssetDatabase.CreateFolder("Assets/Resources/Data", newNameSet);
                string path = "Assets/Resources/Data/" + newNameSet;
                AssetDatabase.SaveAssets();
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ScrPlayerData>(), path + "/playerData.asset");
                AssetDatabase.CreateAsset(ScrItemsData.CreateInstance<ScrItemsData>(), path + "/itemData.asset");
                AssetDatabase.CreateAsset(ScrListEnemy.CreateInstance<ScrListEnemy>(), path + "/enemyData.asset");
                AssetDatabase.CreateAsset(ScrWeaponsData.CreateInstance<ScrWeaponsData>(), path + "/weaponData.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                this.Close();
            }
            else
                EditorUtility.DisplayDialog("File already exists", "Please enter a different configuration name.", "Ok");
        }
        GUILayout.EndHorizontal();
    }
}
