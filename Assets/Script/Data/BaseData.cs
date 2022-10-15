using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
    [Header ("Base")]
    public string name;
    public Sprite m_battleSprite;

    // ANCIEN
    /*public int speed;
    public int attack;
    public int defense;
    public int agility;
    public Sprite m_adventureSprite;*/
   
    [Header ("Health Status")]
    public int maxHealth;

    [HideInInspector] public int healthPoint;

    // ANCIEN
    /*public ENITY_STATE entityState = ENITY_STATE.ALIVE;*/
}

public enum ENITY_STATE
{
    ALIVE,
    DEAD,
    POISONED,
    ASLEEP,
    TIRED,
    INJURED
}
