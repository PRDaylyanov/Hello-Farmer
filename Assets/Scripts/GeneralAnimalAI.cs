using UnityEngine;

public class GeneralAnimalAI : MonoBehaviour
{
    [Header("Global Farm Settings")]
    public Vector3 globalFarmAreaCenter; // Center of the global farm area
    public Vector3 globalFarmAreaSize;   // Size of the global farm area

    [Header("Roaming Settings")]
    public float roamAreaChangeDelay = 10f; // Time before the roam area changes
    public float roamDelay = 3f;           // Time between movement decisions
    public float idleTimeMin = 2f;         // Minimum idle time
    public float idleTimeMax = 5f;         // Maximum idle time
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;       // Speed of rotation towards the movement direction

    private Vector3 roamAreaCenter; // Current roaming area's center
    private Vector3 roamAreaSize;   // Current roaming area's size
    private Vector3 roamTarget;     // Current target position

    private bool isRoaming = false;
    private bool isIdle = false;

    private void Start()
    {
        ChangeRoamArea(); // Set initial roaming area
        InvokeRepeating(nameof(ChangeRoamArea), roamAreaChangeDelay, roamAreaChangeDelay);
        SetIdleState(); // Start in idle state
    }

    private void Update()
    {
        if (isRoaming)
        {
            MoveToTarget();
        }
    }

    private void ChangeRoamArea()
    {
        if (isIdle || isRoaming) return; // Only change area if the animal is stationary

        roamAreaCenter = new Vector3(
            Random.Range(globalFarmAreaCenter.x - globalFarmAreaSize.x / 2, globalFarmAreaCenter.x + globalFarmAreaSize.x / 2),
            globalFarmAreaCenter.y,
            Random.Range(globalFarmAreaCenter.z - globalFarmAreaSize.z / 2, globalFarmAreaCenter.z + globalFarmAreaSize.z / 2)
        );

        roamAreaSize = new Vector3(
            Random.Range(5f, 20f), // Randomize the roaming area's width
            0f,
            Random.Range(5f, 20f)  // Randomize the roaming area's depth
        );

        Debug.Log($"{gameObject.name} changed roaming area to Center: {roamAreaCenter}, Size: {roamAreaSize}");
    }

    private void SetRandomRoamTarget()
    {
        if (isIdle) return;

        roamTarget = new Vector3(
            Random.Range(roamAreaCenter.x - roamAreaSize.x / 2, roamAreaCenter.x + roamAreaSize.x / 2),
            transform.position.y,
            Random.Range(roamAreaCenter.z - roamAreaSize.z / 2, roamAreaCenter.z + roamAreaSize.z / 2)
        );

        isRoaming = true;
    }

    private void MoveToTarget()
    {
        Vector3 direction = (roamTarget - transform.position).normalized;

        // Rotate smoothly toward the target
        if (direction.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, roamTarget, moveSpeed * Time.deltaTime);

        // Stop roaming if the target is reached
        if (Vector3.Distance(transform.position, roamTarget) < 0.1f)
        {
            isRoaming = false;
            SetIdleState();
        }
    }

    private void SetIdleState()
    {
        isIdle = true;
        float idleDuration = Random.Range(idleTimeMin, idleTimeMax);
        Debug.Log($"{gameObject.name} is idling for {idleDuration} seconds.");
        Invoke(nameof(EndIdleState), idleDuration);
    }

    private void EndIdleState()
    {
        isIdle = false;
        SetRandomRoamTarget();
    }

    private void OnDrawGizmos()
    {
        // Draw the global farm area (developer view only)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(globalFarmAreaCenter, globalFarmAreaSize);

        // Draw the current roaming area
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(roamAreaCenter, roamAreaSize);

        // Draw the current target
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(roamTarget, 0.3f);
    }
}
