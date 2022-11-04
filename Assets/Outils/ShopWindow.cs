using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using System.Linq;
using UnityEngine.UIElements;

public class ShopWindow : EditorWindow
{

    public GUISkin customSkin;
    // Add menu named "My Window" to the Window menu
    [MenuItem("Toolbox/Shop")]
    static void ShowMyEditor()
    {
        // Get existing open window or if none, make a new one:
        ShopWindow window = (ShopWindow)EditorWindow.GetWindow(typeof(ShopWindow));
        window.Show();
    }

    public void CreateGUI()
    {
        GUI.skin = customSkin;

        rootVisualElement.Add(new Label("hello"));
        var allObjectGuids = AssetDatabase.FindAssets("t:Sprite");
        var allObjects = new List<Sprite>();

        foreach (var guid in allObjectGuids)
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        GUILayout.Label("oui");
        GUILayout.BeginVertical();
        EditorGUILayout.LabelField("non");
        GUILayout.Space(10);

        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUILayout.EndVertical();
    }
}
