using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsData", menuName = "ScriptableObjects/WeaponsData", order = 1)]
[System.Serializable]
public class ScrWeaponsData : ScriptableObject
{
    public List<Weapon> weapons;
}