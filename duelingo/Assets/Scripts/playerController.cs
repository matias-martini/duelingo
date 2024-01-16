using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 8f;
    private float ySpeed;
    public float rotationSpeed = 10f;

    private CharacterController characterController;
    private Animator animator;
    private bool isSprinting = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Sprint input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;

        }

        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDirection = transform.TransformDirection(direction) * (isSprinting ? sprintSpeed : speed);

        // Apply gravity
        ApplyGravity();

        // Jumping
        if (characterController.isGrounded)
        {
            ySpeed = -0.5f; // Reset vertical speed when grounded

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        // Apply movement
        characterController.Move((moveDirection + new Vector3(0, ySpeed, 0)) * Time.deltaTime);
        RotateTowardsDirection(moveDirection);
        // Update Animator parameters
        UpdateAnimatorParameters(direction.magnitude);
        print(moveDirection);
    }
    void RotateTowardsDirection(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
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

    void UpdateAnimatorParameters(float movementMagnitude)
    {
        // Update Animator parameters
        animator.SetFloat("Speed", movementMagnitude);
        animator.SetBool("IsSprinting", isSprinting && characterController.isGrounded);
        animator.SetBool("IsJumping", !characterController.isGrounded);
    }

    void Jump()
    {
        ySpeed = jumpForce;
        animator.SetTrigger("Jump");
    }
}