using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine.VFX;

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

	private VisualEffect visualEffect;

    private void Awake()
    {
		col = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
		visualEffect = GetComponent<VisualEffect>();
	}

    private void Start()
	{
		
		FPSvcam.gameObject.SetActive(false);
		/*GetComponent<CinemachineImpulseSource>().GenerateImpulse(2);
		Debug.Log("Tremble connard");*/
		//pi = GetComponent<PlayerInput>();

	}
	private void Update()
	{
		vise(direction);
		
		switch (playerMode)
		{
			case PLAYER_MODE.ADVENTURE_MODE:
				direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

				Vector3 mouseScreen = Input.mousePosition;
				mouseScreen.z = -mainCam.transform.position.z;

				mousepos = mainCam.ScreenToWorldPoint(mouseScreen);


				Vector3 camCirection = (mousepos - transform.position).normalized;
				direction.z = 0;

				target.transform.rotation = Quaternion.LookRotation(Vector3.forward, -camCirection);
				target.transform.position = (transform.position + (camCirection * distance));

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
			GameManager.instance.isShooting = true;
			FPSvcam.gameObject.SetActive(true);

		}
		else if (Input.GetKeyUp(KeyCode.Mouse1))
		{
			playerMode = PLAYER_MODE.ADVENTURE_MODE;
			GameManager.instance.isShooting = false;
			FPSvcam.gameObject.SetActive(false);
		}
		
	}
	private void FixedUpdate()
	{
		Move();
		
	}

	protected override void Move()
	{
		rb.MovePosition(transform.position + direction.normalized * Time.fixedDeltaTime * speed);
		if (direction != Vector3.zero)
			visualEffect.SetBool("IsWalking", true);
		else
			visualEffect.SetBool("IsWalking", false);

		visualEffect.SetFloat("DirVel", direction.normalized.x*-1);
		visualEffect.SetFloat("Start", direction.normalized.x);
	}

	private void Shoot()
	{
		Debug.Log("oui");
        Vector3 Mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.z);
		Ray ray = mainCam.ScreenPointToRay(Mousepos);
		Debug.DrawRay(mainCam.transform.position, ray.direction*1000,Color.red,5f);
		RaycastHit gunhit;
		if (Physics.Raycast(mainCam.transform.position, ray.direction,out gunhit, range, mask))
			{
            GameObject go = Instantiate(bullet, mainCam.transform.position,Quaternion.identity);
			go.transform.LookAt(gunhit.point);
			}
			
    }
    //gerer les mask (entity)
    //GameObject go = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, -0.3f), FPSvcam.transform.rotation);
    private void camshake()
	{
		FPSvcam.transform.DOShakeRotation(0.2f, 90, 10, 90, true);
	}
	enum PLAYER_MODE
	{
		ADVENTURE_MODE,
		SHOOTING_MODE,
	}
}
