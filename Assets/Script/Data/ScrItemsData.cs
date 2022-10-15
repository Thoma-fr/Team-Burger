using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "ScriptableObjects/ItemsData", order = 1)]
[System.Serializable]
public class ScrItemsData : ScriptableObject
{
    public List<Item> itemsData;
}