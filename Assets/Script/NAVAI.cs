using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine.VFX;

enum AIState { idle, flee,move, attack,Traped }

public class NAVAI : MonoBehaviour
{
    public bool isdead;
    public GameObject hitcam;
    public enum AItype { passiv, agressive, friendly }
    public GameObject camFight;
    public AItype myType;
    [SerializeField] private AIState mysate = AIState.idle;
    private NavMeshAgent agent;
    public Transform target;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Animator anim;
    private bool hasMove;
    private float targetDistance;

    public float range;
    public float sight;

    [SerializeField] private float maxIdleTime;


    [Header("nav parameter")]
    public float speed;
    public float acceleration;

    private GameObject player;
    //audio
    
    [Header("sfx")]
    private AudioSource audioSource;
    public AudioClip pain;
    public AudioClip pain2;
    public AudioClip zaworld;
    public AudioClip killSFX;
    private VisualEffect visualEffect;
    public List<AudioClip> naturalsound;

    private Vector3 dir;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        if (speed == 0)
        {
            speed = 3.5f;
            acceleration = 8;
        }
        agent.speed = speed;
        agent.acceleration = acceleration;
        player = PlayerController.Instance.gameObject;
        visualEffect = GetComponent<VisualEffect>();


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        if(myType==AItype.agressive)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sight);
            
        }
    }
    private void Update()
    {
        if(anim!=null)
            anim.SetFloat("Velocity", Mathf.Abs(agent.velocity.x));
        if (agent.velocity.x != 0 && agent.velocity.y != 0)
        {
            visualEffect.SetFloat("DirVel", -dir.x);
            visualEffect.SetFloat("Start", (0));
            visualEffect.SetBool("IsWalking", true);
        }
        else
            visualEffect.SetBool("IsWalking", false);
        FlipSpriteX();
        switch (mysate)
        {
            case AIState.idle:
                if (hasMove && Mathf.Abs(agent.velocity.x) < 0.1f)
                    StartCoroutine(waitowalk());
                break;
            case AIState.move:
                Vector3 point;
                int a = Random.Range(0, 5);
                if (a == 2 && PlayerController.playerInstance.playerMode == PlayerController.PLAYER_MODE.SHOOTING_MODE)
                {
                    if (!hasMove)
                    {
                        Debug.Log("ho mon dieu il fonce droit sur nous");
                        agent.SetDestination(Camera.main.transform.position);
                        hasMove = true;
                        audioSource.PlayOneShot(naturalsound[Random.Range(0,naturalsound.Count)]);
                    }
                    mysate = AIState.idle;
                }
                else
                {
                    if (RandomPoint(transform.position, range, out point))
                    {
                        if (!hasMove)
                        {
                            agent.SetDestination(point);
                            dir = point - transform.localPosition;
                            hasMove = true;
                        }
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                        mysate = AIState.idle;
                    }
                    if (a == 3 && myType == AItype.passiv && PlayerController.playerInstance.playerMode == PlayerController.PLAYER_MODE.SHOOTING_MODE)
                        anim.SetTrigger("Dance");
                    if (myType == AItype.agressive)
                        SearchForTarget();
                }
                break;
            case AIState.flee:
                break;
            case AIState.attack:
                if (target == null)
                {
                    mysate = AIState.move;
                    break;
                }
                agent.SetDestination(new Vector3(target.position.x, target.position.y, 0f));
                targetDistance = Vector3.Distance(transform.position, target.position);
                if (targetDistance < 2f)
                {
                    if (anim != null)
                        anim.SetTrigger("Attacking");
                    kill(target.gameObject);


                }
                break;
            case AIState.Traped:
                break;
            default:
                break;
        }


    }
    IEnumerator waitowalk()
    {
        hasMove = false;
        yield return new WaitForSeconds(Random.Range(1f, maxIdleTime));
        mysate = AIState.move;
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.CompareTag("Trap") || collision.transform.CompareTag("Bullet"))
        {
            StopAllCoroutines();
            agent.Stop();
            anim.SetBool("Pain", true);
            mysate = AIState.Traped;
            audioSource.PlayOneShot(pain);
            audioSource.PlayOneShot(pain2);
        }
        if (collision.transform.CompareTag("Bullet"))
        {
            //Debug.Log("hit");
            Time.timeScale = 1f;
            //collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Instantiate(hitcam, collision.contacts[0].point, transform.rotation);
            if (camFight != null)
            {
                camFight.SetActive(true);
                camFight.transform.parent = null;
            }
            Destroy(collision.gameObject);
            GameManager.instance.OnBattleActivation(GetComponent<EnemyController>());
            GameManager.instance.RotateWorld(GameManager.instance.mainCam);
        }

    }
    private void SearchForTarget()
    {
        RaycastHit hit;
        Ray ray;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sight);
        foreach (var hited in hitColliders)
        {
            Debug.Log(hited.transform.name);
            
            NAVAI hitednav;
            if (hited.TryGetComponent<NAVAI>(out hitednav))
            {
                if (hitednav.myType == NAVAI.AItype.passiv)
                {
                    target = hited.transform;
                    mysate = AIState.attack;
                    return;
                }
            }  
        }
    }
    public void kill(GameObject tokill)
    {
        if (!tokill.GetComponent<NAVAI>().isdead)
        {
            target = null;
            tokill.GetComponent<NAVAI>().isdead = true;
            GameManager.instance.faceTheCam.Remove(tokill);
            Destroy(tokill, 0.5f);
            audioSource.PlayOneShot(killSFX);
            mysate = AIState.move;
            tokill.GetComponent<NAVAI>().die();
        }

    }
    public void die()
    {
        isdead = true;
        GameManager.instance.faceTheCam.Remove(gameObject);
        Destroy(camFight);
        Destroy(gameObject);
        Debug.Log("die");
            if (GameManager.instance.combatSystem.enemyController.gameObject == gameObject)
            {
            if (PlayerController.playerInstance.playerMode == PlayerController.PLAYER_MODE.COMBAT_MODE)
                PlayerController.playerInstance.Unvise();
                PlayerController.playerInstance.playerMode = PlayerController.PLAYER_MODE.ADVENTURE_MODE;
                GameManager.instance.combatSystem.state = CombatManagerInGame.BATTLE_STATE.END;
            }
                
    }
    public void FlipSpriteX()
    {
        if(myType==AItype.agressive)
            if (agent.velocity.x < 0)
                sp.flipX = false;
            else
                sp.flipX = true;
        else
            if (agent.velocity.x < 0)
                sp.flipX = true;
            else
                sp.flipX = false;


    }
}
