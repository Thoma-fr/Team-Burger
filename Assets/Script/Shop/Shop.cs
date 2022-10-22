using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel.Design;

public class Shop : MonoBehaviour
{
    public GameObject prefab;
    public Transform itemsParent;

    public GameObject shopPanel;
    private List<GameObject> buttons = new List<GameObject>();
    public List<Item> requestedItems = new List<Item>();
    public List<Item> itemTosell= new List<Item>();

    public ScrItemsData GetDefaultItemsData;
    public void AddItems()
    {
        //Debug.Log(GetDefaultItemsData.itemsData.Find(x => x.ItemName == requestedItems[0].ItemName).ItemName);
        foreach (Item item in requestedItems)
        {
            if (GameManager.instance.GetDefaultItemsData.itemsData.Find(x => x.itemName == item.itemName) != null)
            {
                string st = GameManager.instance.GetDefaultItemsData.itemsData.Find(x => x.itemName == item.itemName).itemName;

                if (st == item.itemName)
                {
                    Debug.Log("<color=green>the item :" + item.itemName + "</color> has been added to the Shop");
                    itemTosell.Add(item);

                }

            }
            else
            {
                Debug.LogError("the item : " + item.itemName + " has not been added to the Shop");
            }

        }
    }
    public void OnOpen()
    {
        shopPanel.SetActive(true);
        foreach (var i in GameManager.instance.GetDefaultItemsData.itemsData)
        {
            GameObject go = Instantiate(prefab, itemsParent);
            buttons.Add(go);
            go.GetComponent<ItemShopInfo>().nameText.GetComponent<TextMeshProUGUI>().text = i.itemName;
            go.GetComponent<ItemShopInfo>().priceText.GetComponent<TextMeshProUGUI>().text = i.price.ToString();

        }
    }
    public void OnClose()
    {
        shopPanel.SetActive(false);
        foreach (var i in buttons)
        {
            Destroy(i);
        }
        buttons.Clear();
    }
}
