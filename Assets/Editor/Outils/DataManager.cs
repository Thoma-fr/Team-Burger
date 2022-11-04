using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataManager : EditorWindow
{
    public GUISkin customSkin;

    private static string configName = "No Config Referenced";
    private string assetName = "No Asset Selected";

    private Vector2 enemyDataScrollPos;
    private Vector2 itemDataScrollPos;
    private Vector2 weaponDataScrollPos;
    private Vector2 playerDataScrollPos;

    private PlayerData dataSelected;
    private Item itemSelected;
    private Weapon weaponSelected;
    private EnemyData enemySelected;

    private static ScrPlayerData scrPlayerData;
    private static ScrItemsData scrItemsData;
    private static ScrWeaponsData scrWeaponsData;
    private static ScrListEnemy scrListEnemy;

    [MenuItem("Toolbox/DataManager")]
    static void InitDataWindow()
    {
        DataManager window = GetWindow<DataManager>();
        window.titleContent = new GUIContent("DataManager");
        window.maxSize = new Vector2(800f, 500f);
        window.minSize = window.maxSize;
        window.Show();
    }

    private void OnGUI()
    {
        GUI.skin = customSkin;

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(configName, "window", GUILayout.Width(200), GUILayout.ExpandHeight(true));

        // ---------------------------------------- Options ---------------------------------------- //
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Open"))
        {
            BrowserResources.InitBrowserWindow();

        }
        GUILayout.Button("Delete");
        GUILayout.EndHorizontal();

        GUILayout.Button("Use This Database");
        GUILayout.Space(30);


        // ---------------------------------------- PlayerData ---------------------------------------- //
        if (scrPlayerData)
        {
            GUILayout.Label("PlayerData");
            if(GUILayout.Button(scrPlayerData.name, "BorderlessButton"))
            {
                assetName = "PlayerData : " + scrPlayerData.name;
                dataSelected = scrPlayerData.playerData;
                itemSelected = null;
                weaponSelected = null;
                enemySelected = null;
            }
        }
        else
            GUILayout.Label("--- No PlayerData ---");

        GUILayout.Space(20);

        // ---------------------------------------- EnemyData ---------------------------------------- //
        if (scrListEnemy)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("EnemyData");
            GUILayout.Button("Add", GUILayout.Width(75));
            GUILayout.EndHorizontal();

            enemyDataScrollPos = EditorGUILayout.BeginScrollView(enemyDataScrollPos);

            foreach (EnemyData enemy in scrListEnemy.allEnemiesData)
            {
                if (GUILayout.Button(enemy.name, "BorderlessButton"))
                {
                    assetName = "Enemy : " + enemy.name;
                    dataSelected = null;
                    itemSelected = null;
                    weaponSelected = null;
                    enemySelected = enemy;
                }
            }

            EditorGUILayout.EndScrollView();
        }
        else
            GUILayout.Label("--- No EnemyData ---");

        GUILayout.Space(20);

        // ---------------------------------------- ItemData ---------------------------------------- //
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
                    assetName = "Item : " + item.name;
                    dataSelected = null;
                    itemSelected = item;
                    weaponSelected = null;
                    enemySelected = null;
                }
            }
            EditorGUILayout.EndScrollView();
        }
        else
            GUILayout.Label("--- No ItemData ---");

        GUILayout.Space(20);

        // ---------------------------------------- WeaponData ---------------------------------------- //
        if (scrWeaponsData)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("WeaponData");
            GUILayout.Button("Add", GUILayout.Width(75));
            GUILayout.EndHorizontal();

            weaponDataScrollPos = EditorGUILayout.BeginScrollView(weaponDataScrollPos);

            foreach (Weapon weapon in scrWeaponsData.weapons)
            {
                if(GUILayout.Button(weapon.name, "BorderlessButton"))
                {
                    assetName = "Weapon : " + weapon.name;
                    dataSelected = null;
                    itemSelected = null;
                    weaponSelected = weapon;
                    enemySelected = null;
                }
            }
            EditorGUILayout.EndScrollView();
        }
        else
            GUILayout.Label("--- No WeaponData ---");

        GUILayout.EndVertical();

        GUILayout.BeginVertical(assetName, "window", GUILayout.ExpandHeight(true));

        DisplayAsset();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    private void DisplayAsset()
    {
        if (dataSelected != null)
        {
            GUILayout.Space(10);
            // ---------------------------------------- Inventory ---------------------------------------- //
            GUILayout.BeginHorizontal();
            GUILayout.Label("Inventory");
            GUILayout.Button("Add Item");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("window", GUILayout.Height(100));

            // ---------------------------------------- Debug ---------------------------------------- //
            playerDataScrollPos = EditorGUILayout.BeginScrollView(playerDataScrollPos, "window", GUILayout.Width(150));
            foreach (Item item in dataSelected.inventory)
            {
                if (GUILayout.Button(item.name, "BorderlessButton"))
                {
                    itemSelected = item;
                }
            }
            EditorGUILayout.EndScrollView();

            if(itemSelected != null)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("Name");
                itemSelected.name = GUILayout.TextField(itemSelected.name);
                GUILayout.EndVertical();

                GUILayout.BeginVertical();
                GUILayout.Label("Number max stack");
                itemSelected.maxStackable = EditorGUILayout.IntField(itemSelected.maxStackable);
                GUILayout.EndVertical();

                GUILayout.Label("Description");
                itemSelected.description = GUILayout.TextField(itemSelected.description);
            }
            else
                GUILayout.Label("No Selection");
            // ---------------------------------------- Debug ---------------------------------------- //

            GUILayout.EndHorizontal();

            // ---------------------------------------- Weapon ---------------------------------------- //
            GUILayout.BeginHorizontal();
            GUILayout.Label("Weapon");
            GUILayout.Label("Weapon In Hand");
            GUILayout.EndHorizontal();
        }else if(enemySelected != null)
        {

        }
        else if(weaponSelected != null)
        {

        }else if(itemSelected != null)
        {
            GUILayout.Label("Name");
            GUILayout.Label("Number max stack");
            GUILayout.Label("Description");
        }
    }


    public static void OpenDatabase(string path, string name)
    {
        scrPlayerData = null;
        scrItemsData = null;
        scrWeaponsData = null;
        scrListEnemy = null;
        
        ScriptableObject[] allScript = Resources.LoadAll<ScriptableObject>(path);
        foreach (ScriptableObject obj in allScript)
        {
            if(obj is ScrPlayerData playerData)
            {
                scrPlayerData = playerData;
            }
            else if (obj is ScrItemsData itemData)
            {
                scrItemsData = itemData;
            }
            else if (obj is ScrWeaponsData weaponData)
            {
                scrWeaponsData = weaponData;
            }
            else if (obj is ScrListEnemy enemyData)
            {
                scrListEnemy = enemyData;
            }
        }

        configName = name;
    }
}
