using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseData
{
    private Rigidbody2D _myRigidbody;
    public Animator _myAnimator;
    public Sprite _battleSprite;

    [Header ("General")]
    public string _myName;
    public Statistic _myStatistic;
    public int speed;
    
    [Header ("Health Status")]
    public int healthPoint;
    public ENITY_STATE entityState = ENITY_STATE.ALIVE;
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
