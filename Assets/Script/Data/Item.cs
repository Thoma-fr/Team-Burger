using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : Object
{
	public int currentStack;
	public int maxStackable;

	public Item(string b_name, int b_maxStackable)
	{
		name = b_name;
		maxStackable = b_maxStackable;
	}

	public Item(Item clone)
	{
		currentStack = clone.currentStack;
		maxStackable = clone.maxStackable;
		name = clone.name;
		description = clone.description;
		price = clone.price;
	}
}
