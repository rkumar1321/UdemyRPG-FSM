using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public PlayerInputActions playerInputActions { get; private set; }
    private StateMachine stateMachine;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }

    [Header("Attack Details")]
    public float atkTimer { get; private set; } = 0.1f;
    public float attackRepulseMovement { get; private set; } = 3f;
    public Vector2[] atkRepulse;
    public float comboResetTime { get; private set; } = 2f;
    private Coroutine queueCoroutine;
    

    [Header("Movement Details")]
    public float moveSpeed { get; private set; } = 8f;
    public float jumpSpeed { get; private set; } = 12f;
    public Vector2 moveInput { get; private set; }
    
    private bool isFacingRight = true;
    public float dashDuration { get; private set; } = 0.25f;
    public float dashSpeed { get; private set; } = 20f;

    public float inAirSpeedMultiplier { get; private set; } = 0.7f;
    public float wallSlideSlowMultiplier { get; private set; } = 0.7f;
    public Vector2 wallJumpMultiplier { get; private set; } = new Vector2(6, 12);
    

    [Header("Collision Detection")]
    public int facingDir { get; private set; } = 1;
    [SerializeField] private float groundCheckingDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    public bool isOnGrounded { get; private set; }
    public bool isWallAhead { get; private set; }

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        playerInputActions = new PlayerInputActions();

        idleState = new Player_IdleState(this, stateMachine, "Idle");
        moveState = new Player_MoveState(this, stateMachine, "Move");
        jumpState = new Player_JumpState(this, stateMachine, "Jump/Fall");
        fallState = new Player_FallState(this, stateMachine, "Jump/Fall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "Jump/Fall");
        dashState = new Player_DashState(this, stateMachine, "Dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "BasicAttack");
    }

    private void OnEnable() {
        playerInputActions.Enable();

        playerInputActions.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInputActions.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable() {
        playerInputActions.Disable();
    }

    private void Start() {
        stateMachine.Intialize(idleState);
    }

    private void Update() {
        HandleCollisionDetection();

        stateMachine.UpdateActiveState();
    }

    public void EnterAttackStateWithDelay() {
        if(queueCoroutine != null)
            StopCoroutine(queueCoroutine);

        queueCoroutine = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo() {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    public void SetVelocity(float xVelocity, float yVelocity) {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);

        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity) {
        if(xVelocity > 0 && isFacingRight == false) {
            Flip();
        } else if(xVelocity < 0 && isFacingRight) {
            Flip();
        }
    }

    private void HandleCollisionDetection() {
        // For ground Check
        isOnGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckingDistance, whatIsGround);

        // For wall check
        isWallAhead = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    public void CallAnimationTriggerEvents() {
        stateMachine.currentState.CallAnimationEventStateChange();
    }

    private void Flip() {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
        facingDir = facingDir * (-1);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckingDistance, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0, 0));
    }
}
