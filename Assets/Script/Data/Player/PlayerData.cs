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
    public Weapon weaponInHand;

    public int money;

    public void RemoveItemFromInventory(Item item, int number = 1)
    {
        Item itemTemp = inventory.Find(x => x.name == item.name);
        if (itemTemp != null)
        {
            int itemId = inventory.IndexOf(itemTemp);

            if (inventory[itemId].currentStack - number <= 0)
                inventory.Remove(itemTemp);
            else
                inventory[itemId].currentStack = inventory[itemId].currentStack - number;
        }
    }

    public void AddItemInInventory(Item item, int number = 1)
    {
        Item itemTemp = inventory.Find(x => x.name == item.name);
        if (itemTemp != null)
        {
            int itemId = inventory.IndexOf(itemTemp);

            inventory[itemId].currentStack = Mathf.Clamp(inventory[itemId].currentStack + number, 1, inventory[itemId].maxStackable);
        }
        else
        {
            if (GameManager.instance.GetDefaultItemsData.ContainItem(item.name, out Item info))
            {
                Item itemToAdd = new Item(GameManager.instance.GetDefaultItemsData.itemsData.Find(x => x.name == info.name));
                inventory.Add(itemToAdd);
                inventory[inventory.IndexOf(itemToAdd)].currentStack = Mathf.Clamp(number, 1, itemToAdd.maxStackable);
            }
        }
    }
}
