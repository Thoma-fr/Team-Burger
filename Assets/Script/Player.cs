using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private void Start()
    {
        sprite = GetComponent<Sprite>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    protected override void Move()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.MovePosition(transform.position + dir.normalized * Time.fixedDeltaTime * speed); 
    }

}
