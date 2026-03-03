using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private StateMachine stateMachine;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }


    public Vector2 moveInput { get; private set; }

    private void Awake() {
        stateMachine = new StateMachine();
        playerInputActions = new PlayerInputActions();

        idleState = new Player_IdleState(this, stateMachine, "IdleState");
        moveState = new Player_MoveState(this, stateMachine, "MoveState");
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
        stateMachine.UpdateActiveState();
    }
}
