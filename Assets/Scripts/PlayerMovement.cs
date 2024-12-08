using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float jumpHeight = 1.2f;
    public float gravity = -9.81f;
    public float crouchHeight = 1f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float normalHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        normalHeight = controller.height;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Check if grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Adjust speed for sprinting and crouching
        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) // Sprint
        {
            speed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // Crouch
        {
            speed = crouchSpeed;
            controller.height = Mathf.Lerp(controller.height, crouchHeight, Time.deltaTime * 8f);
        }
        else
        {
            controller.height = Mathf.Lerp(controller.height, normalHeight, Time.deltaTime * 8f);
        }

        controller.Move(move * speed * Time.deltaTime);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}