using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected EnemyData dataEntity;

    protected string name { get; set; }
    protected Sprite sprite { get; set; }
    protected Collider2D col { get; set; }
    protected Rigidbody2D rb { get; set; }

    public int health;
    public int speed;
    public Statistic statistic;

    protected abstract void Move();
}

public struct Statistic
{
    int attack;
    int defense;
    
    public Statistic(int att, int def)
    {
        attack = att;
        defense = def;
    }
}

//enum RELATION
//{
//    ENEMY,
//    FRIENDLY,
//    NEUTRAL
//}