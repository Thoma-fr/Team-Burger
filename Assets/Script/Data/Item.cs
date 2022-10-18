using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : Object
{
    public Item(string b_name, int b_maxStackable)
    {
        name = b_name;
        maxStackable = b_maxStackable;
    }

    public string name;
    public int maxStackable;

    public void UseItem()
    {
        Debug.Log("Item Used");
    }
}
