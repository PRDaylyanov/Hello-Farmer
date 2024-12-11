using UnityEngine;
using UnityEngine.AI;

public class FarmerAI : MonoBehaviour
{
    public NavMeshAgent agent; // Reference to the NavMeshAgent
    public Transform player; // Reference to the player's Transform
    public Animator animator; // Reference to the Animator component

    public float chaseRange = 15f; // Distance to start chasing the player
    public float chaseDuration = 5f; // Time to chase the player
    public float roamRadius = 30f; // Radius for roaming

    private float chaseTimer = 0f;
    private Vector3 roamTarget;
    private bool isChasing = false;

    private enum State { Idle, Roaming, Chasing }
    private State currentState;

    void Start()
    {
        currentState = State.Idle;
        SetRandomRoamTarget();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdle();
                break;

            case State.Roaming:
                HandleRoaming();
                break;

            case State.Chasing:
                HandleChasing();
                break;
        }

        // Detect the player and switch to chasing state
        if (!isChasing && Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            isChasing = true;
            chaseTimer = 0f;
            currentState = State.Chasing;
            PlayAnimation("Run_Shooter");
        }
    }

    private void HandleIdle()
    {
        // Idle for a short duration, then start roaming
        if (!agent.hasPath)
        {
            Invoke(nameof(StartRoaming), Random.Range(2f, 4f)); // Idle for 2-4 seconds
        }
    }

    private void HandleRoaming()
    {
        if (!agent.hasPath || agent.remainingDistance < 1f)
        {
            SetRandomRoamTarget();
        }
    }

    private void HandleChasing()
    {
        if (isChasing)
        {
            agent.SetDestination(player.position);
            chaseTimer += Time.deltaTime;

            if (chaseTimer >= chaseDuration)
            {
                isChasing = false;
                currentState = State.Idle;
                PlayAnimation("Idle_Menu");
                SetRandomRoamTarget();
            }
        }
    }

    private void StartRoaming()
    {
        currentState = State.Roaming;
        PlayAnimation("Run_Front");
        SetRandomRoamTarget();
    }

    private void SetRandomRoamTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, roamRadius, NavMesh.AllAreas))
        {
            roamTarget = hit.position;
            agent.SetDestination(roamTarget);
        }
    }

    private void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }
}
