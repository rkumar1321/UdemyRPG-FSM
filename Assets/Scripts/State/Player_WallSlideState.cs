public class Player_WallSlideState : EntityState {
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {
    }


    public override void Update() {
        base.Update();

        HandleWallSlide();

        // If jump: Enter wallJumpState
        if (player.playerInputActions.Player.Jump.WasPerformedThisFrame()) {
            stateMachine.ChangeState(player.wallJumpState);
        }


        // if wall is not ahead of player: enter fall state
        if (player.isWallAhead == false) {
            stateMachine.ChangeState(player.fallState);
        }

        // if player is on ground : enter idle state
        if (player.isOnGrounded) {
            stateMachine.ChangeState(player.idleState);
        }
    }

    // Handle Wall slide logic
    private void HandleWallSlide() {
        if (player.moveInput.y < 0) {
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        } else {
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideSlowMultiplier);
        }
    }
}
