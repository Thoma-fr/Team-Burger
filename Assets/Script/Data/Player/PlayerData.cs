using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : BaseData
{
    public Dictionary<string, int> inventory;
    public List<Weapon> weapons;
}
