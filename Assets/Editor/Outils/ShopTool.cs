using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Shop))]
public class ShopTool : Editor
{
   //fonction qui remplace l'affichage normale de l'éditeur
    public override void OnInspectorGUI()
    {
        Shop myShop = (Shop)target;
        base.OnInspectorGUI();//affiche l'éditeur de base 
        
        myShop.itemname = EditorGUILayout.TextField(myShop.itemname);
        myShop.description = EditorGUILayout.TextArea(myShop.description);
        myShop.price = EditorGUILayout.IntField(myShop.price);


        if (GUILayout.Button("Add Item"))//fais spawn un bouton qui déclanche la fonction AddItems
        {
            myShop.AddItems();
        }
       
    }
    
}
