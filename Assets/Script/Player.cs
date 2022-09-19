using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
	public LayerMask mask;
    
	private void Start()
	{
		sprite = GetComponent<Sprite>();
		col = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Shoot();
		}
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

	private void Shoot()
	{
		Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
		RaycastHit2D rc = Physics2D.Raycast(transform.position, dir, 10.0f, mask);
		Debug.DrawRay(transform.position, dir * 10.0f, Color.red, 2.0f);
		Debug.Log(rc.transform.name);
        if (rc.collider != null)
        {
			
        }
    }

}
