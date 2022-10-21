using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Shop))]
public class ShopTool : Editor
{
    

    public override void OnInspectorGUI()
    {
        Shop myShop = (Shop)target;
        base.OnInspectorGUI();
        if(GUILayout.Button("Add Item"))
        {
            myShop.AddItems();
        }
       // name = EditorGUILayout.EnumPopup(Object.a).ToString();
    }
    private enum Object
    {
        a,b,c
    }
}
