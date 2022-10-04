using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyData : BaseData
{
    [Header("General Settings")]
    public ANIMAL animal;
    [Range(0,100)] public float spawnChance;

    [Header ("Combat Settings")]
    public bool alwaysRun;
    public float runAwaySpeed;
    public Attack[] m_Attacks;
}

[System.Serializable]
public struct Attack
{
    public string attName;
    public ATTACK_TYPE attType;
    public ATTACK_POWER attPower;
    [Range (0,100)] public float powerChance;
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

public enum ANIMAL
{
    DEER,
    RABBIT,
    BOAR,
    PARIS_PIGEON,
    RAKON,
    BEEVER,
    OURS,
    FRANK_OOOOOOOOOOOOOOOOOOOOOOOOO,
    SHINY_NICO,
    SHINY_ALEX,
    SHINY_THOMAS,
    SUPER_JESUS,
    JEROM,
}