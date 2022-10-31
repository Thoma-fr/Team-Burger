using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataManager : EditorWindow
{
    public GUISkin customSkin;

    private string assetPath = "";
    private string pathToDisplay = "No Path Referenced";
    private string assetName = "No Asset Selected";

    private Vector2 enemyDataScrollPos;
    private Vector2 itemDataScrollPos;
    private Vector2 weaponDataScrollPos;

    private ScriptableObject dataSelected;

    private static ScrPlayerData scrPlayerData;
    private static ScrItemsData scrItemsData;
    private static ScrWeaponsData scrWeaponsData;
    private static ScrListEnemy scrListEnemy;

    [MenuItem("Toolbox/DataManager")]
    static void InitDataWindow()
    {
        DataManager window = GetWindow<DataManager>();
        window.titleContent = new GUIContent("DataManager");
        window.maxSize = new Vector2(1000f, 500f);
        window.minSize = window.maxSize;
        window.Show();
    }

    private void OnGUI()
    {
        GUI.skin = customSkin;


        GUILayout.BeginVertical();
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Space(10);

        GUILayout.BeginVertical(pathToDisplay, "window", GUILayout.Width(300), GUILayout.ExpandHeight(true));
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Open"))
        {
            BrowserResources.InitBrowserWindow();

        }
        GUILayout.Button("Delete");
        GUILayout.EndHorizontal();

        GUILayout.Button("Use This Database");
        GUILayout.Space(30);


        // PlayerData
        if (scrPlayerData)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("PlayerData");
            GUILayout.Button("Add", GUILayout.Width(75));
            GUILayout.EndHorizontal();

            GUILayout.Button(scrPlayerData.name, "BorderlessButton");
        }
        else
            GUILayout.Label("--- No PlayerData ---");

        GUILayout.Space(20);

        // EnemyData
        if (scrListEnemy)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("EnemyData");
            GUILayout.Button("Add", GUILayout.Width(75));
            GUILayout.EndHorizontal();

            enemyDataScrollPos = EditorGUILayout.BeginScrollView(enemyDataScrollPos);

            GUILayout.Button("BOUTON 0", "BorderlessButton");

            EditorGUILayout.EndScrollView();
        }
        else
            GUILayout.Label("--- No EnemyData ---");

        GUILayout.Space(20);

        // ItemData
        if (scrItemsData)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("ItemDataData");
            GUILayout.Button("Add", GUILayout.Width(75));
            GUILayout.EndHorizontal();

            itemDataScrollPos = EditorGUILayout.BeginScrollView(itemDataScrollPos);

            foreach (Item item in scrItemsData.itemsData)
            {
                if(GUILayout.Button(item.name, "BorderlessButton"))
                {
                    Debug.Log(item.name + " is Selected");
                }
            }
            EditorGUILayout.EndScrollView();
        }
        else
            GUILayout.Label("--- No ItemData ---");

        GUILayout.Space(20);

        // WeaponData
        if (scrWeaponsData)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("WeaponData");
            GUILayout.Button("Add", GUILayout.Width(75));
            GUILayout.EndHorizontal();

            weaponDataScrollPos = EditorGUILayout.BeginScrollView(weaponDataScrollPos);

            foreach (Weapon weapon in scrWeaponsData.weapons)
            {
                GUILayout.Button(weapon.name, "BorderlessButton");
            }
            EditorGUILayout.EndScrollView();
        }
        else
            GUILayout.Label("--- No WeaponData ---");

        GUILayout.EndVertical();
        GUILayout.Space(40);
        GUILayout.BeginVertical(assetName, "window", GUILayout.ExpandHeight(true));

        // _______________________________________________________ //
       
        GUILayout.EndVertical();
        GUILayout.Space(10);
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.EndVertical();
    }

    public static void OpenDatabase(string path)
    {
        scrPlayerData = null;
        scrItemsData = null;
        scrWeaponsData = null;
        scrListEnemy = null;
        
        ScriptableObject[] allScript = Resources.LoadAll<ScriptableObject>("Data");
        foreach (ScriptableObject obj in allScript)
        {
            try
            {
                scrPlayerData = (ScrPlayerData)obj;
                Debug.Log("ScrPlayerData imported successfuly");
            }
            catch { }

            try
            {
                scrItemsData = (ScrItemsData)obj;
                Debug.Log("ScrItemsData imported successfuly");
            }
            catch { }

            try
            {
                scrWeaponsData = (ScrWeaponsData)obj;
                Debug.Log("ScrWeaponsData imported successfuly");
            }
            catch { }

            try
            {
                scrListEnemy = (ScrListEnemy)obj;
                Debug.Log("ScrListEnemy imported successfuly");
            }
            catch { }
        }
    }

    private string CutAssetPath(string path, int numberCharMax)
    {
        string[] pathSplit = path.Split('/');
        int currentPathLenght = 0;
        string toReturn = "";

        for(int i = pathSplit.Length - 1; i != 0; i--)
        {
            currentPathLenght += pathSplit[i].Length;
            if (currentPathLenght < numberCharMax)
                toReturn = pathSplit[i] + "/" + toReturn;
            else
                break;
        }

        return "... " + toReturn;
    }
}
