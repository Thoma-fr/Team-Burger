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
    public List<Item> items = new List<Item>();
    public bool real;
    public void AddItems()
    {
        foreach (Item item in items)
        {
            if (GameManager.instance.GetDefaultItemsData.itemsData.Find(x => x.name == item.name)!=null)
            {
                Debug.Log("Item already exists");
                real = true;
            }
            else
            {
                real = false;
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
            go.GetComponent<ItemShopInfo>().nameText.GetComponent<TextMeshProUGUI>().text = i.name;
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
