using UnityEngine;

public class Player_WallJumpState : EntityState {
    public Player_WallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {
    }

    public override void Enter() {
        base.Enter();


        // here player.wallJumpMultiplier.x is for pushing player far away from wall
        // While facingDir change the dir of push as left or right
        player.SetVelocity(-player.wallJumpMultiplier.x * player.facingDir, player.wallJumpMultiplier.y);
    }

    public override void Update() {
        base.Update();

        if(rb.linearVelocity.y < 0) {
            stateMachine.ChangeState(player.fallState);
        }

        if (player.isWallAhead) {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}