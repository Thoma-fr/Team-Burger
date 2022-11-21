using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleItem : MonoBehaviour, Interactable
{
    public string itemName;
    public int nbStack;

    private Item itemSelf;

    public void Start()
    {
        if (GameManager.instance.GetDefaultItemsData.ContainItem(itemName, out Item item))
        {
            itemSelf = item;
            itemSelf.currentStack = nbStack;
        }
    }
    public void Interact()
	{
		GameManager.instance.GetPlayerData.AddItemInInventory(itemSelf, itemSelf.currentStack);
		Destroy(this.transform.gameObject);
	}
}
