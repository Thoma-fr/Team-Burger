using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
	[Header("Shooting Setting")]
	[SerializeField] private float range;
	[SerializeField] private LayerMask mask;

	[Header ("Debug")]
	[SerializeField] private PLAYER_MODE playerMode = PLAYER_MODE.ADVENTURE_MODE;
	[SerializeField] private float speed;

	private Vector3 direction;


	private void Start()
	{
		col = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
        switch (playerMode)
        {
            case PLAYER_MODE.ADVENTURE_MODE:
				direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
				break;

            case PLAYER_MODE.SHOOTING_MODE:
				if (Input.GetKeyDown(KeyCode.Mouse0))
					Shoot();
                break;

            default:
                break;
        }
	}

	private void FixedUpdate()
	{
		Move();
	}

	protected override void Move()
	{
		rb.MovePosition(transform.position + direction.normalized * Time.fixedDeltaTime * speed); 
	}

	private void Shoot()
	{
		RaycastHit info;
        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, out info, range, mask) && info.transform.GetComponent<Renderer>().isVisible)
        {
			Debug.Log(info.transform.name);
			Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), info.transform.position, Color.green, 2.0f);
			// appèle l'interface : IShootable
        }
		else
			Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * range, Color.red, 2.0f);
    }

	enum PLAYER_MODE
    {
		ADVENTURE_MODE,
		SHOOTING_MODE,
    }
}
