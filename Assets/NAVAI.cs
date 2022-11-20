using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


enum AIState { idle, flee,move, attack }
public class NAVAI : MonoBehaviour
{
    

    [SerializeField] private AIState mysate= AIState.idle;
    NavMeshAgent agent;
    public Transform target;
    public GameObject player;
    private Rigidbody rb;
    public SpriteRenderer sp;
    public Animator anim;
    public bool hasMove;
    public float range;
    private void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        Debug.Log(agent.velocity.x);



        anim.SetFloat("Velocity", Mathf.Abs(agent.velocity.x));

        if (agent.velocity.x < 0)
            sp.flipX = true;
        else
            sp.flipX = false;

        switch (mysate)
        {
            case AIState.idle:
                if(hasMove && Mathf.Abs(agent.velocity.x)<0.1f)
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
                    mysate= AIState.idle;
                }

                break;
            case AIState.flee:
                break;
            case AIState.attack:
                agent.SetDestination(new Vector3(target.position.x, target.position.y, 0f));
                break;
            default:
                break;
        }
        
        
    }
    IEnumerator waitowalk()
    {
        hasMove=false;
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
}
