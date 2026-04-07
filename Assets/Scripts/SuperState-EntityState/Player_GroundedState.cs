using UnityEngine;

public class Player_GroundedState : EntityState {
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {
    }


    public override void Update() {
        base.Update();


        if(player.moveInput.y < 0 && player.isOnGrounded == false) {
            stateMachine.ChangeState(player.fallState);
        }


        // If space key were pressed, enter in JumpState
        if (playerInputActions.Player.Jump.WasPerformedThisFrame()) {
            stateMachine.ChangeState(player.jumpState);
        }

        // If left mouse button were pressed, enter in BasicAttackState
        if (playerInputActions.Player.Attack.WasPerformedThisFrame()) {
            stateMachine.ChangeState(player.basicAttackState);;
        }
    }
}
