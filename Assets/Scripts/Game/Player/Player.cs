using System;
using System.Collections;
using SceneController;
using UnityEngine;

namespace Game.Player
{
	public class Player : Singleton<Player>
	{
		public enum State
		{
			Idle,
			Running,
			TakeItem,
			[Obsolete]
			Interact,
			UseItem,
			TeleportIn,
			TeleportOut,
			Damage,
			LookAtMirror,
		}

		public static readonly int IsMoving = Animator.StringToHash("isMoving");
		public static readonly int IsJumping = Animator.StringToHash("isJumping");

		public static readonly int Interact = Animator.StringToHash("Interact");
		public static readonly int TakeItem = Animator.StringToHash("TakeItem");
		public static readonly int UseItem = Animator.StringToHash("UseItem");
		public static readonly int Die = Animator.StringToHash("Die");
		public static readonly int JumpBlend = Animator.StringToHash("JumpBlend");


		[Header("Movement Settings")] public float moveSpeed = 5f;
		public float jumpForce = 10f;

		public IObservableVar<State> CurrentState => _state;
		public event Action OnJump;
		public event Action OnLand;
		public bool IsGrounded => isGrounded;

		[Header("Ground Check")] [SerializeField]
		private Transform groundCheck;

		[SerializeField] private float groundCheckRadius = 0.2f;
		[SerializeField] private LayerMask groundLayer;

		[SerializeField] private Transform view;

		[SerializeField] private TouchProbe lelfProbe;
		[SerializeField] private TouchProbe rightProbe;
		[SerializeField] private InteractProbe interactProbe;
		[SerializeField] private PlayerDissolveEffect dissolveEffect;

		[Header("Debug")] [SerializeField] private float force;
		[SerializeField] private ObservableVar<State> _state;
		private Coroutine _interactCor;
		private Coroutine _jumpCor;

		private Rigidbody rb;
		[SerializeField] private Animator animator;
		[SerializeField] private Animator shadow;
		private bool isGrounded, wasGrounded;

		protected override void OnAwake()
		{
			_state = new ObservableVar<State>(State.Idle);
		}

		private void Start()
		{
			rb = GetComponentInChildren<Rigidbody>();
			animator = GetComponentInChildren<Animator>();
			interactProbe.OnInteract += ProceedState;
			//Appear();
		}

		private void OnDestroy()
		{
			interactProbe.OnInteract -= ProceedState;
		}

		private void Update()
		{
			if (_state.Value == State.Idle || _state.Value == State.Running)
			{
				HandleMovement();
				HandleJump();
				_state.Value = rb.linearVelocity.sqrMagnitude > 0.001f ? State.Running : State.Idle;
			}
			else
			{
				rb.linearVelocity = Vector3.zero;
			}

			UpdateAnimations();
		}

		private void ProceedState(State interactionState, float duration)
		{
			if (_interactCor != null)
				return;

			_interactCor = StartCoroutine(InteractCoroutine(interactionState, duration));
		}

		private IEnumerator InteractCoroutine(State interactionState, float duration)
		{
			_state.Set(interactionState);
			switch (interactionState)
			{
				case State.Interact:
					animator.SetTrigger(Interact);
					shadow.SetTrigger(Interact);
					break;

				case State.TakeItem:
                    AudioManager.PlayOneShot(FMODEvents.Instance.grabItem);

                    animator.SetTrigger(TakeItem);
					shadow.SetTrigger(TakeItem);
					break;

				case State.UseItem:
                    AudioManager.PlayOneShot(FMODEvents.Instance.grabItem);

                    animator.SetTrigger(UseItem);
					shadow.SetTrigger(UseItem);
					break;
				
				case State.Damage:
					animator.SetTrigger(Die);
					shadow.SetTrigger(Die);
					break;

				case State.TeleportIn:
					//view.transform.forward = Vector3.right;
					dissolveEffect.RunDisappear();
					//animator.SetTrigger(Teleport);
					break;

				case State.TeleportOut:
					//view.transform.forward = Vector3.right;
					dissolveEffect.RunAppear();
					//animator.SetTrigger(Teleport);
					break;
				
				case State.LookAtMirror:
					view.transform.forward = Vector3.left;
					yield return new WaitForSeconds(2.5f);
					animator.SetTrigger(TakeItem);
					shadow.SetTrigger(TakeItem);
					yield return new WaitForSeconds(1f);
					view.transform.right = Vector3.right;
					break;
			}

			yield return new WaitForSeconds(duration);
			_interactCor = null;
			_state.Set(State.Idle);
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
				MoveTutorial.isPlayerMoved = true;
				view.transform.right = horizontalInput > 0f 
					? Vector3.right : Vector3.left; 
			}
		}

		private void HandleJump()
		{
			isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

			if (isGrounded && !wasGrounded)
			{
				OnLand?.Invoke();
			}

			wasGrounded = isGrounded;

			if (!isGrounded)
				return;

			// if (_jumpCor == null)
			// {
			//     _jumpCor = StartCoroutine(JumpCor());
			// }

			if (Input.GetButtonDown("Jump"))
			{
				rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
				OnJump?.Invoke();
			}
		}

		// private IEnumerator JumpCor()
		// {
		//     
		// }

		private void UpdateAnimations()
		{
			var isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.001f;
			var jf = rb.linearVelocity.y;
			animator.SetFloat(JumpBlend, jf);
			animator.SetBool(IsMoving, isMoving);
			animator.SetBool(IsJumping, !isGrounded);
			
			shadow.SetBool(IsMoving, isMoving);
			shadow.SetBool(IsJumping, !isGrounded);
		}
		
		public void Disappear()
		{
			dissolveEffect.Set(0f);
			ProceedState(State.TeleportIn, 1f);
		}

		public void Appear()
		{
			dissolveEffect.Set(1f);
			ProceedState(State.TeleportOut, 1f);
		}

		public void TakeDamage(float duration)
		{
			AudioManager.PlayOneShot(FMODEvents.Instance.death);
			ProceedState(State.Damage, duration);
		}
	}
}