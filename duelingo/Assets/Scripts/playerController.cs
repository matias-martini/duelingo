using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0f;
    public float sprintSpeed = 10f;
    public float jumpForce = 8f;
    private float ySpeed;

    private CharacterController characterController;
    private Animator animator;
    private bool isSprinting = false;
    private bool drawWeapon = false;
    private bool sheathWeapon = true;
    public GameObject Sword, Axe, Spear;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()


    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                SwordAttack();
            }
        }

        ApplyGravity();
        // Sprint input


        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift) && vertical>0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDirection = transform.TransformDirection(direction) * (isSprinting ? sprintSpeed : speed);
        print(isSprinting);

        if (Input.GetButtonDown("Jump"))
        {
            if (characterController.isGrounded)
            {
                
                ySpeed = jumpForce;
                animator.SetTrigger("Jump");
                animator.SetBool("IsJumping", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (sheathWeapon == true && drawWeapon == false)
            {
                sheathWeapon = false;
                drawWeapon = true;
                animator.SetTrigger("drawWeapon");
                animator.SetBool("weaponOn", drawWeapon);

            }
            else if (sheathWeapon == false && drawWeapon == true)
            {
                sheathWeapon = true;
                drawWeapon = false;
                animator.SetTrigger("sheathWeapon");
                animator.SetBool("weaponOn", false);
            }
        }


        // Apply movement
        characterController.Move((moveDirection + new Vector3(0, ySpeed, 0)) * Time.deltaTime);

        // Update Animator parameters
        UpdateAnimatorParameters(horizontal * speed, vertical * speed);
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
            animator.SetBool("IsJumping", false);
        }
    }


    void UpdateAnimatorParameters(float movementMagnitude, float verticalInput)

    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Update Animator parameters
        animator.SetFloat("XSpeed", movementMagnitude);
        animator.SetFloat("YSpeed", verticalInput);
        animator.SetBool("IsSprinting", isSprinting);
    }
    public void SwordAttack()
    {
        isAttacking = true;
        CanAttack = false;
        animator.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking=false;
    }
}
