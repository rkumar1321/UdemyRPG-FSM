using UnityEngine;

public class Player_FallState : Player_AirState {
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {
    }


    public override void Update() {
        base.Update();

        // Detect ground and if true : Enter idle State
        if (player.isOnGrounded) {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.isWallAhead) {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}