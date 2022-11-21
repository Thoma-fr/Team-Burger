using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

enum AIState { idle, flee,move, attack,Traped }
enum AItype { passiv,agressive,friendly}
public class NAVAI : MonoBehaviour
{

    [SerializeField] private AItype myType;
    [SerializeField] private AIState mysate = AIState.idle;
    private NavMeshAgent agent;
    public Transform target;
    //public GameObject player;
    private Rigidbody rb;
    public SpriteRenderer sp;
    public Animator anim;
    private bool hasMove;


    [Range(0, 20)]
    public float range;

    [Range(0, 20)]
    public float sight;

    //audio
    [Header("sfx")]
    private AudioSource audioSource;
    public AudioClip pain;
    public AudioClip pain2;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
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
        //Debug.Log(agent.velocity.x);



        anim.SetFloat("Velocity", Mathf.Abs(agent.velocity.x));

        if (agent.velocity.x < 0)
            sp.flipX = true;
        else
            sp.flipX = false;

        switch (mysate)
        {
            case AIState.idle:
                if (hasMove && Mathf.Abs(agent.velocity.x) < 0.1f)
                    StartCoroutine(waitowalk());
                break;
            case AIState.move:
                Vector3 point;
                if (RandomPoint(transform.position, range, out point))
                {
                    if (!hasMove)
                    {
                        agent.SetDestination(point);
                        hasMove = true;
                    }
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    mysate = AIState.idle;
                }

                break;
            case AIState.flee:
                break;
            case AIState.attack:
                agent.SetDestination(new Vector3(target.position.x, target.position.y, 0f));
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
        yield return new WaitForSeconds(Random.Range(1f, 10f));
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

    }
}
