using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


[System.Serializable]
public class EnemyData : BaseData
{
    public EnemyData(string e_name, Sprite e_sprite, int e_maxPV, int e_pv, List<Attack> b_attcks) : base(e_name, e_sprite, e_maxPV, e_pv)
    {
        attacks = new List<Attack>(b_attcks);
    }

    public EnemyData(EnemyData baseEnemyData) : base(baseEnemyData.name, baseEnemyData.battleSprite, baseEnemyData.maxHealth, baseEnemyData.healthPoint)
    {
        attacks = new List<Attack>(baseEnemyData.attacks);
        _animatorCtrl = baseEnemyData._animatorCtrl;
    }

    // ANCIEN
    /*[Header("General Settings")]
    public ANIMAL animal;
    [Range(0,100)] public float spawnChance;*/

    [Header ("Combat Settings")]
    public List<Attack> attacks;

    [Range(0, 100)]
    public int menace = 50;

    public AnimatorController _animatorCtrl;

    // ANCIEN
    //public bool alwaysRun;
    //public float runAwaySpeed;

    /*public EnemyData(EnemyData origin)
    {
        animal = origin.animal;
        spawnChance = origin.spawnChance;
        alwaysRun = origin.alwaysRun;
        runAwaySpeed = origin.runAwaySpeed;
    }*/
}

[System.Serializable]
public struct Attack
{
    public string attName;

    // NOUVEAU
    public int damage;

    // ANCIEN
    /*public ATTACK_TYPE attType;
    [Range(0, 100)] public int attForce;
    [Range(0, 100)] public int attChance;
    public ATTACK_POWER attPower;
    [Range(0, 100)] public int powerChance;*/
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
    FRANK_O,
    SHINY_NICO,
    SHINY_ALEX,
    SHINY_THOMAS,
    SUPER_JESUS,
    JEROM,
}