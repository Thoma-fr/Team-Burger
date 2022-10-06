using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class PlayerController : BaseController
{
	[Header("Shooting Setting")]
	[SerializeField] private float range;
	[SerializeField] private LayerMask mask;

	[Header ("Debug")]
	[SerializeField] private PLAYER_MODE playerMode = PLAYER_MODE.ADVENTURE_MODE;
	[SerializeField] private float speed;

	private Vector3 direction;
	private PlayerInput pi;

	public Camera cam;
    public CinemachineVirtualCamera FPSvcam;

    public GameObject emptyprefab;

	private GameObject empty;
    private void Start()
	{
		col = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
        FPSvcam.gameObject.SetActive(false);
        //pi = GetComponent<PlayerInput>();

    }
	//Vector2 inputmove;
 //   public void ONinputMove(InputAction.CallbackContext context)
 //   {
 //       inputmove = context.ReadValue<Vector2>();
 //   }

    private void Update()
	{
		vise(direction);
        
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
	public void vise( Vector3 direction)
	{
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerMode = PLAYER_MODE.SHOOTING_MODE;
            FPSvcam.gameObject.SetActive(true);
           // empty = Instantiate(emptyprefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            FPSvcam.gameObject.transform.rotation = Quaternion.Euler(direction);

        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            playerMode = PLAYER_MODE.ADVENTURE_MODE;
            FPSvcam.gameObject.SetActive(false);
            //Destroy(empty);
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
