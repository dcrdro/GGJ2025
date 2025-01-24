using Cysharp.Threading.Tasks;
using SceneManagement;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        public enum InteractState
        {
            Idle,
            Running,
            TakeItem,
            Interact,
            UseItem,
            Teleport,
        }
        
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        [Header("Movement Settings")] public float moveSpeed = 5f;
        public float jumpForce = 10f;

        [Header("Ground Check")] 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        
        [SerializeField] private Transform view;

        [SerializeField] private TouchProbe lelfProbe;
        [SerializeField] private TouchProbe rightProbe;

        [Header("Debug")] 
        [SerializeField] private float force; 
        [SerializeField] private InteractState state = InteractState.Idle; 
        
        

        private Rigidbody rb;
        private Animator animator;
        private bool isGrounded;
        

        private void Start()
        {
            rb = GetComponentInChildren<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            HandleMovement();
            HandleJump();
            UpdateAnimations();
        }

        private void HandleMovement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector3 velocity = rb.linearVelocity;
            force = horizontalInput;
            if (horizontalInput < 0f)
            {
                if (lelfProbe.IsTouching)
                {
                    force = 0f;
                }
            }
            else if (horizontalInput > 0f)
            {
                if (rightProbe.IsTouching)
                {
                    force = 0f;
                }
            }
            velocity.x = force * moveSpeed;
            velocity.z = 0;
            rb.linearVelocity = velocity;

            if (horizontalInput != 0)
            {
                view.transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
            }
        }

        private void HandleJump()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

            if (!isGrounded)
                return;
            
            if (Input.GetButtonDown("Jump"))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }
        }

        private void UpdateAnimations()
        {
            animator.SetBool(IsMoving, Mathf.Abs(rb.linearVelocity.x) > 0.001f);
            animator.SetBool(IsJumping, !isGrounded);
        }

        public void TakeDamage()
        {
            animator.SetTrigger("Die");
            _ = Restart(); 
        }

        private async UniTask Restart()
        {
            await UniTask.Delay(1000);
            SceneLoader.ReloadScene();
        }
    }
}