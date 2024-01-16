using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;
    private float ySpeed;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDirection = transform.TransformDirection(direction) * speed;

        // Apply gravity
        ApplyGravity();

        // Jumping
        if (characterController.isGrounded)
        {
            ySpeed = -0.5f; // Reset vertical speed when grounded

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
            }
        }

        // Apply movement
        characterController.Move((moveDirection + new Vector3(0, ySpeed, 0)) * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            ySpeed = -0.5f; // A small downward force to ensure being grounded
        }
    }
}
