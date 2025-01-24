using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using SceneManagement;
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
        
        private static readonly int Interact = Animator.StringToHash("Interact");
        private static readonly int TakeItem = Animator.StringToHash("TakeItem");
        private static readonly int UseItem = Animator.StringToHash("UseItem");
        private static readonly int Teleport = Animator.StringToHash("Teleport");
        
        
        [Header("Movement Settings")] public float moveSpeed = 5f;
        public float jumpForce = 10f;

        [Header("Ground Check")] 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        
        [SerializeField] private Transform view;

        [SerializeField] private TouchProbe lelfProbe;
        [SerializeField] private TouchProbe rightProbe;
        [SerializeField] private InteractProbe interactProbe;

        [Header("Debug")] 
        [SerializeField] private float force; 
        [SerializeField] private InteractState state = InteractState.Idle;
        private Coroutine _interactCor;
        private Coroutine _jumpCor;

        private Rigidbody rb;
        private Animator animator;
        private bool isGrounded;
        

        private void Start()
        {
            rb = GetComponentInChildren<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            interactProbe.OnInteract += HandleInteractState;
        }

        private void OnDestroy()
        {
            interactProbe.OnInteract -= HandleInteractState;
        }
        
        private void Update()
        {
            if (state == InteractState.Idle || state == InteractState.Running)
            {
                HandleMovement();
                HandleJump();
                state = rb.linearVelocity.sqrMagnitude > 0.001f ? InteractState.Running : InteractState.Idle;
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
            UpdateAnimations();
        }

        private void HandleInteractState(InteractState interactionState, float duration)
        {
            if (_interactCor != null)
                return;
            
            _interactCor = StartCoroutine(InteractCoroutine(interactionState, duration));
        }

        private IEnumerator InteractCoroutine(InteractState interactionState, float duration)
        {
            state = interactionState;
            switch (interactionState)
            {
                case InteractState.Interact:
                    animator.SetTrigger(Interact);
                    break;
                
                case InteractState.TakeItem:
                    animator.SetTrigger(TakeItem);
                    break;
                
                case InteractState.UseItem:
                    animator.SetTrigger(UseItem);
                    break;
                
                case InteractState.Teleport:
                    animator.SetTrigger(Teleport);
                    break;
            }
            
            yield return new WaitForSeconds(duration);
            _interactCor = null;
            state = InteractState.Idle;
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

            // if (_jumpCor == null)
            // {
            //     _jumpCor = StartCoroutine(JumpCor());
            // }
            
            if (Input.GetButtonDown("Jump"))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }
        }

        // private IEnumerator JumpCor()
        // {
        //     
        // }

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