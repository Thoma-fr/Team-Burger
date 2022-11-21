using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine.VFX;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using System;

public class PlayerController : BaseController
{
	public static PlayerController Instance { get; private set; }

	[Header("Shooting Setting")]
	[SerializeField] private float range;
	[SerializeField] private LayerMask mask;

	[Header ("Debug")]
	public PLAYER_MODE playerMode = PLAYER_MODE.ADVENTURE_MODE;
	[SerializeField] private float speed;

	private Vector3 direction;
	private PlayerInput pi;


	public CinemachineVirtualCamera FPSvcam;
	public Camera mainCam;
	public GameObject target;
	private Vector3 mousepos;
	public float distance;

	public TilemapCollider2D water;
	[SerializeField] private LayerMask interact;
	public NPCController npc;

	public GameObject bullet;

	private VisualEffect visualEffect;

	public GameObject canvasReticle;

    [Header("SFX")]
	private AudioSource audioSource;
	public AudioClip shootSFX;
    private void Awake()
    {
		Instance = this;
		col = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		visualEffect = GetComponent<VisualEffect>();
	}

    private void Start()
	{
		Cursor.visible=false;
		canvasReticle.SetActive(false);
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
                canvasReticle.SetActive(false);
                target.GetComponent<SpriteRenderer>().enabled = true;
                direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                mainCam.transform.GetComponent<Volume>().enabled = false;
                Vector3 mouseScreen = Input.mousePosition;
				mouseScreen.z = -mainCam.transform.position.z;

				mousepos = mainCam.ScreenToWorldPoint(mouseScreen);


				Vector3 camDirection = (mousepos - transform.position).normalized;
				direction.z = 0;

				target.transform.rotation = Quaternion.LookRotation(Vector3.forward, -camDirection);

				target.transform.position = (transform.position + (camDirection * distance));

				if (Input.GetKeyDown("e"))
					Interact(camDirection);

				if (Input.GetKeyDown("i"))
					ToggleInventory();

				break;

			case PLAYER_MODE.SHOOTING_MODE:
                canvasReticle.SetActive(true);
				target.GetComponent<SpriteRenderer>().enabled = false;
                canvasReticle.transform.position = Input.mousePosition;
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
                    
                    Shoot();
                }
					
				break;

			case PLAYER_MODE.DIALOGUE_MODE:
				if (Input.GetKeyDown("e"))
					npc.nextSentence();
				break;
			default:
				break;
		}   
	}

    private void ToggleInventory()
    {
        throw new NotImplementedException();
    }

    public void vise( Vector3 direction)
	{
        mainCam.transform.GetComponent<Volume>().enabled = true;
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
        canvasReticle.SetActive(false);
        mainCam.transform.GetComponent<Volume>().enabled = false;
        Debug.Log("oui");
        Vector3 Mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.z);
		Ray ray = mainCam.ScreenPointToRay(Mousepos);
		Debug.DrawRay(mainCam.transform.position, ray.direction*1000,Color.red,5f);
		RaycastHit gunhit;
		if (Physics.Raycast(mainCam.transform.position, ray.direction,out gunhit, range, mask))
		{
			GameObject go = Instantiate(bullet, mainCam.transform.position,Quaternion.identity);
			go.transform.LookAt(gunhit.point);
			audioSource.PlayOneShot(shootSFX);
		}
			
    }

	public void CanSwin()
	{
		water.GetComponent<TilemapCollider2D>().enabled = false;
	}

	public void Interact(Vector3 camCirection)
	{
		Vector3 interactPos = transform.position + (camCirection);

		Debug.DrawLine(transform.position, interactPos, Color.red, 0.5f);

		Collider2D col = Physics2D.OverlapCircle(interactPos, 0.3f, interact);
		if (col != null)
		{
			col.GetComponent<Interactable>()?.Interact();
		}

	}

	private void camshake()
	{
		FPSvcam.transform.DOShakeRotation(0.2f, 90, 10, 90, true);
	}
	public enum PLAYER_MODE
	{
		ADVENTURE_MODE,
		SHOOTING_MODE,
		DIALOGUE_MODE
	}
}
