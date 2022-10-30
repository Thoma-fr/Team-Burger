using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.VisualScripting;

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


    public CinemachineVirtualCamera FPSvcam;
    public Camera mainCam;
    public GameObject target;
    private Vector3 mousepos;
    public float distance;

    public GameObject bullet;
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



        if(playerMode== PLAYER_MODE.ADVENTURE_MODE)
        {
            Vector3 mouseScreen = Input.mousePosition;
            mouseScreen.z = -mainCam.transform.position.z;

            mousepos = mainCam.ScreenToWorldPoint(mouseScreen);

        
            Vector3 camCirection = (mousepos - transform.position).normalized;
            direction.z = 0;

            target.transform.rotation = Quaternion.LookRotation(Vector3.forward, -camCirection);
            target.transform.position = (transform.position + (camCirection * distance));

            //FPSvcam.transform.Rotate(new Vector3(0, 0, -target.transform.rotation.z));
        }
       


    }
	public void vise( Vector3 direction)
	{
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerMode = PLAYER_MODE.SHOOTING_MODE;
            GameManager.instance.isShooting = true;
            FPSvcam.gameObject.SetActive(true);
           // empty = Instantiate(emptyprefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            //FPSvcam.gameObject.transform.rotation = Quaternion.Euler(direction);

        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            playerMode = PLAYER_MODE.ADVENTURE_MODE;
            GameManager.instance.isShooting = false;
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
        GameObject go = Instantiate(bullet,new Vector3(transform.position.x, transform.position.y, -0.3f), FPSvcam.transform.rotation);
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
