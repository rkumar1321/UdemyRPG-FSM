using UnityEngine;

public abstract class EntityState {

    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator animator;
    protected Rigidbody2D rb;
    protected PlayerInputActions playerInputActions;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;


        animator = player.animator;
        rb = player.rb;
        playerInputActions = player.playerInputActions;
    }


    public virtual void Enter() {
        animator.SetBool(animBoolName, true);
    }

    public virtual void Update() {
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    public virtual void Exit() {
        animator.SetBool(animBoolName, false);
    }
}