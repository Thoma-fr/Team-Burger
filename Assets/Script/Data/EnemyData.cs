using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/EnemyData", order = 1)]
[System.Serializable]
public class EnemyData : ScriptableObject
{
    public BaseData _myBaseData;
    
    [Header ("Combat Settings")]
    public bool alwaysRun;
    public float runAwaySpeed;
    public Attack[] _myAttacks;
}

[System.Serializable]
public struct Attack
{
    public string attName;
    public ATTACK_TYPE attType;
    public ATTACK_POWER attPower;
}

[System.Serializable]
public enum ATTACK_TYPE 
{
    NONE,
    MELLEE,
    RANGE
}

[System.Serializable]
public enum ATTACK_POWER
{
    NONE,
    POISON,
    BONE_BREAKER
}