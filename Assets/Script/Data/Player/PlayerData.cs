using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : BaseData
{
    public PlayerData(string p_name, Sprite p_sprite, int p_maxPV, int p_pv, List<Item> p_inventory, List<Weapon> p_weapon) : base(p_name, p_sprite, p_maxPV, p_pv)
    {
        inventory = new List<Item>(p_inventory);
        weapons = new List<Weapon>(p_weapon);
    }

    public PlayerData(PlayerData basePlayerData) : base(basePlayerData.name, basePlayerData.battleSprite, basePlayerData.maxHealth, basePlayerData.healthPoint)
    {
        inventory = new List<Item>(basePlayerData.inventory);
        weapons = new List<Weapon>(basePlayerData.weapons);
    }

    public List<Item> inventory = new List<Item>();
    public List<Weapon> weapons = new List<Weapon>();

    public int money;

    /*public void RemoveItemFromInventory(string itemName, int number = 1)
    {
        if (inventory.TryGetValue(itemName, out int currentItemNumber))
        {
            if (currentItemNumber - number <= 0)
                inventory.Remove(itemName);
            else
                inventory[itemName] = currentItemNumber - number;
        }
    }

    public void AddItemInInventory(string itemName, int number = 1)
    {
        if (inventory.TryGetValue(itemName, out int currentItemNumber))
        {
            inventory["itemName"] = currentItemNumber + number;
        }
        else
        {
            if (GameManager.instance.GetDefaultItemsData.ContainItem(itemName, out Item info))
            {
                inventory.Add(itemName, number);
            }
        }
    }*/
}
