using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    
    public List<Item> itemsToBuy = new List<Item>();
    public List<Weapon> weaponsToBuy = new List<Weapon>();

    public GameObject prefab;

    public Transform itemsParent;
    private void Start()
    {
        foreach (var i in itemsToBuy)
        {
            var go = Instantiate(prefab, itemsParent);
            go.GetComponent<ItemShopInfo>().nameText=i.name;
        }
    }
}
