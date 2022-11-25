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
using System.Threading;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;

public class PlayerController : BaseController
{
	[SerializeField]private GameObject minimap;
	[HideInInspector] public bool isVise = false;
	private Animator animator;
	private SpriteRenderer sp;
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
	[SerializeField]private float distance;

	public TilemapCollider2D water;
	[SerializeField] private LayerMask interact;
	public NPCController npc;

	public GameObject bullet;

	private VisualEffect visualEffect;

	public GameObject canvasReticle;

    [Header("SFX")]
	private AudioSource audioSource;
	public AudioClip shootSFX;

	public static PlayerController playerInstance;
    private void Awake()
    {
		if(playerInstance==null)
		{
			playerInstance = this;
		}
        else
		{
			Destroy(this);
		}

		Instance = this;
		sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		visualEffect = GetComponent<VisualEffect>();
	}

    private void Start()
	{
		animator = GetComponent<Animator>();
		Cursor.visible=false;
		canvasReticle.SetActive(false);
		FPSvcam.gameObject.SetActive(false);

	}
	private void Update()
	{
		//vise();

        switch (playerMode)
		{
			case PLAYER_MODE.ADVENTURE_MODE:
                GetComponent<SpriteRenderer>().enabled = true;
                minimap.SetActive(true);
                canvasReticle.SetActive(false);
                target.GetComponent<SpriteRenderer>().enabled = true;
               
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
				minimap.SetActive(false);
                canvasReticle.SetActive(true);
				target.GetComponent<SpriteRenderer>().enabled = false;
                canvasReticle.transform.position = Input.mousePosition;
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
                    
                    //Shoot();
                }
					
				break;

			case PLAYER_MODE.DIALOGUE_MODE:
				if (Input.GetKeyDown("e"))
					npc.nextSentence();
				break;
			case PLAYER_MODE.COMBAT_MODE:
				GetComponent<SpriteRenderer>().enabled = false;
				canvasReticle.SetActive(false);
				break;
			default:
				break;
		}   
	}

    private void ToggleInventory()
    {
        throw new NotImplementedException();
    }

    public void vise(InputAction.CallbackContext context)
    {
		if(playerMode != PLAYER_MODE.COMBAT_MODE)
			if(context.started)
			{
				mainCam.transform.GetComponent<Volume>().enabled = true;
				if (!isVise)
				{
					isVise=true;
					playerMode = PLAYER_MODE.SHOOTING_MODE;
					//GameManager.instance.isShooting = true;
					//GameManager.instance.RotateWorld(GameManager.instance.mainCam);
                    FPSvcam.gameObject.SetActive(true);
				}
				else
				{
					isVise = false;
					playerMode = PLAYER_MODE.ADVENTURE_MODE;
					//GameManager.instance.isShooting = false;
                    //GameManager.instance.RotateWorld(GameManager.instance.mainCam);
                    FPSvcam.gameObject.SetActive(false);
				}
			}
        
	}
	private void FixedUpdate()
	{
        //rb.MovePosition(transform.position + direction.normalized * Time.fixedDeltaTime * speed);
		rb.velocity =direction.normalized * Time.fixedDeltaTime * speed;

        Debug.Log(rb.velocity.x);
        animator.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VelocityY", rb.velocity.y);
        if (rb.velocity.x < 0)
            sp.flipX = true;
        else
            sp.flipX = false;
    }

	public void Moveplayer(InputAction.CallbackContext context )
	{
		
        //direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if(playerMode==PLAYER_MODE.ADVENTURE_MODE)
		{
			direction = context.ReadValue<Vector2>();
       
					if (direction != Vector3.zero)
						visualEffect.SetBool("IsWalking", true);
					else
						visualEffect.SetBool("IsWalking", false);

					visualEffect.SetFloat("DirVel", direction.normalized.x*-1);
					visualEffect.SetFloat("Start", direction.normalized.x);
		}
        
	}

	public void Shoot(InputAction.CallbackContext context)
    {
        if (playerMode == PLAYER_MODE.SHOOTING_MODE)
            if (context.started)
			{
				canvasReticle.SetActive(false);
				mainCam.transform.GetComponent<Volume>().enabled = false;
				Debug.Log("oui");
				Vector3 Mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.z);
				Ray ray = mainCam.ScreenPointToRay(Mousepos);
				Debug.DrawRay(mainCam.transform.position, ray.direction * 1000, Color.red, 5f);
				RaycastHit gunhit;
				if (Physics.Raycast(mainCam.transform.position, ray.direction, out gunhit, range, mask))
				{
					GameObject go = Instantiate(bullet, mainCam.transform.position, Quaternion.identity);
					go.transform.LookAt(gunhit.point);
					audioSource.PlayOneShot(shootSFX);
					Debug.Log(gunhit.distance);
					float distanceMax = 40;
					float min = 1f;
					//Time.timeScale = Mathf.Lerp(1, 0.1f, 40 - gunhit.distance*Time.deltaTime);
					
                    DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.1f, gunhit.distance/40);
                }
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
		DIALOGUE_MODE,
		COMBAT_MODE
	}
}
