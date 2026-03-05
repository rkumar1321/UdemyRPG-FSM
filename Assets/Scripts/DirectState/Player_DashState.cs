using UnityEngine;

public class Player_DashState : EntityState {
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {
    }
    private float originalGravityScale;
    public override void Enter() {
        base.Enter();

        stateTimer = player.dashDuration;
        // Register original gravity
        originalGravityScale = rb.gravityScale;

        //reset gravityScale to 0 so dash work superb
        rb.gravityScale = 0;

        player.SetVelocity(player.dashSpeed * player.facingDir, 0);
    }

    public override void Update() {
        base.Update();
        CancelDashifNeeded();

        stateTimer -= Time.deltaTime;
        if (stateTimer < 0) {
            if (player.isOnGrounded){
                stateMachine.ChangeState(player.idleState);
            } else {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit() {
        base.Exit();
        // Not want to player move even a bit after dash so 
        player.SetVelocity(0, 0);
        // restore original gravityScale
        rb.gravityScale = originalGravityScale;
    }

    private void CancelDashifNeeded() {
        if (player.isWallAhead) {
            // On Ground while touching wall
            if (player.isOnGrounded) {
                stateMachine.ChangeState(player.idleState);
            } // On Air while touching wall
            else {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }
}
