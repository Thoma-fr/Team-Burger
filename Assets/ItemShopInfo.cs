using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemShopInfo : MonoBehaviour
{

    private  GameObject goTextName;
    public string nameText;
    public int price;
    private GameObject goTextPrice;
    public string priceText;
    public Sprite sprite;

    private void Awake()
    {
        nameText = goTextName.GetComponent<TextMeshPro>().text;
        priceText = goTextPrice.GetComponent<TextMeshPro>().text;
    }
}
