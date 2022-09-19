using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyData data;

    // Start is called before the first frame update
    void Start()
    {
        data._myBaseData._adventureSprite = GetComponent<Sprite>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    protected override void Move()
    {
    }
}
