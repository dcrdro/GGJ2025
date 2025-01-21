using UnityEngine;

public class Player: MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 velocity = rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        velocity.z = 0; 
        rb.linearVelocity = velocity;
        
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
        }
    }

    void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    void UpdateAnimations()
    {
        animator.SetBool("isMoving", rb.linearVelocity.x > 0.1f);
        animator.SetBool("isJumping", !isGrounded);
    }
}
