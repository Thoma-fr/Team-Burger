using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
    public BaseData(string b_name, Sprite b_sprite, int b_maxPV, int b_PV)
    {
        name = b_name;
        battleSprite = b_sprite;
        maxHealth = b_maxPV;
        healthPoint = b_PV;
    }

    [Header ("Base")]
    public string name;
    public Sprite battleSprite;

    // ANCIEN
    /*public int speed;
    public int attack;
    public int defense;
    public int agility;
    public Sprite m_adventureSprite;*/
   
    [Header ("Health Status")]
    public int maxHealth;

    public int healthPoint;

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
