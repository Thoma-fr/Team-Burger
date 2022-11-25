using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel.Design;
using Unity.VisualScripting;

public class Shop : MonoBehaviour
{
    [Header("reference")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private GameObject shopPanel;

    public string itemname;
    public string description;
    public int price;
    Item itemAdded;


    [Header("Item a ajouter dans le magasin")]
    [SerializeField] private List<Item> requestedItems = new List<Item>();
    [Header("Item a vendre dans le magasin")]
    [SerializeField] private List<Item> itemTosell= new List<Item>();

    private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private ScrItemsData GetDefaultItemsData;
    public void AddItems()//si y'a une erreur de null référence il faut metre en play et arrèter 
    {
        //itemAdded.name= itemname;
        //itemAdded.description= description;
        //itemAdded.price= price;

        //requestedItems.Add(itemAdded);
        foreach (Item item in requestedItems)
        {
            if (GameManager.instance.GetDefaultItemsData.itemsData.Find(x => x.name == item.name) != null)
            {
                string st = GameManager.instance.GetDefaultItemsData.itemsData.Find(x => x.name == item.name).name;

                if (st == item.name)//hhoooo c'est bo un debug qui fait de la couleur 
                {
                    Debug.Log("<color=green>the item :" + item.name + "</color> has been added to the Shop");
                    itemTosell.Add(item);// hop hop hop ont ajoute ça a la liste des items a vendre

                }
            }
            else //si le nom de l'item existe pas, ça fait une erreur
            {
                Debug.LogError("<color=red>the item : " + item.name + "</color> has not been added to the Shop");
            }// un Log.Error car ça fait menacant bhouuuuu en plus y'a du rouge bbooooooo

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
