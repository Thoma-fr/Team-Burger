using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
[System.Serializable]
public class PlayerData : ScriptableObject
{
    public BaseData m_baseData;
}
