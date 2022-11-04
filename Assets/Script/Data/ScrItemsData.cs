using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "ScriptableObjects/ItemsData", order = 1)]
[System.Serializable]
public class ScrItemsData : ScriptableObject
{
    public List<Item> itemsData;

    public bool ContainItem(string itemName, out Item thisItem) {
        thisItem = null;
        foreach(Item item in itemsData)
        {
            if(item.itemName == itemName)
            {
                thisItem = item;
                return true;
            }
        }
        return false;
    }
}