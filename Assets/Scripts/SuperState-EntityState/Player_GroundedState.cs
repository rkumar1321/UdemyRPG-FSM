using UnityEngine;

public class Player_GroundedState : EntityState {
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {
    }


    public override void Update() {
        base.Update();

        // If space key were pressed, enter in JumpState
        if (playerInputActions.Player.Jump.WasPerformedThisFrame()) {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
