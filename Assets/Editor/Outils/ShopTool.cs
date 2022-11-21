using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Shop))]
public class ShopTool : Editor
{
   //fonction qui remplace l'affichage normale de l'�diteur
    public override void OnInspectorGUI()
    {
        Shop myShop = (Shop)target;
        base.OnInspectorGUI();//affiche l'�diteur de base 

        if(GUILayout.Button("Add Item"))//fais spawn un bouton qui d�clanche la fonction AddItems
        {
            myShop.AddItems();
        }
       
    }
    
}
