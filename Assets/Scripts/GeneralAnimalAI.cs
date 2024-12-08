using UnityEngine;
using UnityEngine.AI;

public class GeneralAnimalAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float chaseRange = 10f;
    public float roamRange = 20f;
    public float idleTime = 3f;

    private Vector3 roamCenter;
    private float idleTimer;
    private bool isIdling;
    private enum State { Roaming, Idling, Chasing }
    private State currentState;

    void Start()
    {
        // Initialize
        roamCenter = transform.position;
        SetRandomRoamTarget();
        currentState = State.Roaming;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Roaming:
                HandleRoaming();
                break;

            case State.Idling:
                HandleIdling();
                break;

            case State.Chasing:
                HandleChasing();
                break;
        }

        // Check if the player is within chase range
        if (Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            currentState = State.Chasing;
        }
        else if (currentState == State.Chasing)
        {
            currentState = State.Roaming;
            SetRandomRoamTarget();
        }
    }

    void HandleRoaming()
    {
        if (!agent.hasPath || agent.remainingDistance < 1f)
        {
            StartIdling();
        }
    }

    void HandleIdling()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleTime)
        {
            idleTimer = 0f;
            isIdling = false;
            currentState = State.Roaming;
            SetRandomRoamTarget();
        }
    }

    void HandleChasing()
    {
        agent.SetDestination(player.position);
    }

    void StartIdling()
    {
        isIdling = true;
        idleTimer = 0f;
        currentState = State.Idling;
        agent.ResetPath();
    }

    void SetRandomRoamTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRange;
        randomDirection += roamCenter;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, roamRange, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void SetNewRoamCenter(Vector3 newCenter)
    {
        roamCenter = newCenter;
    }
}