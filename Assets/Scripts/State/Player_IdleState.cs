using UnityEngine;

public class Player_IdleState : Player_GroundedState {
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {
    }

    public override void Enter() {
        base.Enter();

        //  for stopping sliding after entering Idle State
        player.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update() {
        base.Update();

        if (player.moveInput != Vector2.zero) {
            stateMachine.ChangeState(player.moveState);
        }
    }
}