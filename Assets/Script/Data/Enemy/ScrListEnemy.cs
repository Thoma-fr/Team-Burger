using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "ScriptableObjects/EnemiesData", order = 1)]
[System.Serializable]
public class ScrListEnemy : ScriptableObject
{
    public List<EnemyData> allEnemiesData;
}