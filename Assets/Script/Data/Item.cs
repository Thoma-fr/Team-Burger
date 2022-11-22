using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : Object
{
	public int currentStack;
	public string itemName;
	public int maxStackable;

	public Item(string b_name, int b_maxStackable)
	{
		itemName = b_name;
		maxStackable = b_maxStackable;
	}

	public void UseItem()
	{
		Debug.Log("Item Used");
	}
}
