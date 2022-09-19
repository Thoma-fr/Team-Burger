using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
    protected Rigidbody2D _myRigidbody { get; set; }
    protected Collider2D col { get; set; }

    [Header ("General")]
    public string _myName;
    public int speed;
    public Statistic _myStatistic;  // class en abstract - non visible dans l'éditeur
    public Animator _myAnimator;
    public Sprite _battleSprite;
    public Sprite _adventureSprite;
   
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
